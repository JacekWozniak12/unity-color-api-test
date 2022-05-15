using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Threading.Tasks;

public class ApiConnector : MonoBehaviour
{
    public static ApiConnector Instance;
    const string API_ADDRESS = "http://colormind.io/api/";

    public List<Color> CurrentColors = new List<Color>(5);
    public UnityEvent<Color[]> ColorReady = new UnityEvent<Color[]>();

    void Awake() => Instance = this;

    public async void RequestColorScheme() => await SendRequestColorScheme();
    public async void RequestColorScheme(Dictionary<int, Color> dictionary)
    {
        string part = "{\"input\":[";

        for (int i = 0; i < 5; i++)
        {
            if (dictionary.TryGetValue(i, out Color color))
            {
                byte[] byteColor = ColorRangeConverter.ColorToRGB255_Byte(color.r, color.g, color.b);
                part += $"[{byteColor[0]}, {byteColor[1]}, {byteColor[2]}]";
            }
            else part += "\"N\"";
            if (i < 4) part += ",";
        }

        part += "],\"model\":\"default\"}";

        await SendRequestColorScheme(part);
    }

    UnityWebRequest SetColorSchemeRequest(string request, byte[] dataBytes)
    {
        UnityWebRequest colorSchemeInfoRequest = UnityWebRequest.Get(request);
        colorSchemeInfoRequest.uploadHandler = new UploadHandlerRaw(dataBytes);
        colorSchemeInfoRequest.downloadHandler = new DownloadHandlerBuffer();
        colorSchemeInfoRequest.SetRequestHeader("accept", " text/plain");
        colorSchemeInfoRequest.SetRequestHeader("application", "x-www-form-urlencoded");
        return colorSchemeInfoRequest;
    }

    async Task SendRequestColorScheme(string data = "{\"model\" : \"default\"}")
    {
        string requestAddress = API_ADDRESS;
        var dataBytes = Encoding.UTF8.GetBytes(data);
        UnityWebRequest colorSchemeInfoRequest = SetColorSchemeRequest(requestAddress, dataBytes);

        UnityWebRequest.Result result = await colorSchemeInfoRequest.SendWebRequest();
        Popup.Instance.RequestHideLoading();

        if (CheckIfConnectionOrProtocolError(result))
        {
            Popup.Instance.RequestErrorMessage(colorSchemeInfoRequest.error);
            return;
        }

        try
        {
            CurrentColors = GetColorsFromRequest(colorSchemeInfoRequest);
            ColorReady?.Invoke(CurrentColors.ToArray());
        }
        catch (JsonException e)
        {
            Popup.Instance.RequestErrorMessage(e.Message + "\n " + colorSchemeInfoRequest.downloadHandler.text);
        }
    }

    bool CheckIfConnectionOrProtocolError(UnityWebRequest.Result result)
    {
        return result == UnityWebRequest.Result.ConnectionError || result == UnityWebRequest.Result.ProtocolError;
    }

    List<Color> GetColorsFromRequest(UnityWebRequest colorSchemeInfoRequest)
    {
        JObject t = JsonConvert.DeserializeObject<JObject>(colorSchemeInfoRequest.downloadHandler.text);
        List<int>[] colors = t.GetValue("result").ToObject<List<int>[]>();
        List<Color> temp = CreateListOfColorsFromIntArray(colors);
        return temp;
    }

    List<Color> CreateListOfColorsFromIntArray(List<int>[] colors)
    {
        List<Color> temp = new List<Color>();

        foreach (List<int> l in colors)
        {
            Color c = new Color(l[0], l[1], l[2]);

            string message = string.Format(
                "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
                (byte)(c.r), (byte)(c.g), (byte)(c.b), c);

            temp.Add(c);
        }

        return temp;
    }
}




