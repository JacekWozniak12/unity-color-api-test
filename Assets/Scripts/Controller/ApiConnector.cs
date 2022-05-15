using System;
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
    const string API_ADDRESS = "http://colormind.io/api/";
    public List<Color> CurrentColors = new List<Color>(5);
    public static ApiConnector Instance;

    void Awake() => Instance = this;

    /// <summary>
    /// Tries to connect with server specified in API_ADDRESS
    /// </summary>
    public UnityEvent<Color[]> ColorReady = new UnityEvent<Color[]>();

    public async void RequestColorScheme()
    {
        await SendRequestColorScheme();
    }

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

    public async void RequestColorScheme(Color color, int index)
    {
        string part = "{\"input\":[";
        byte[] byteColor = ColorRangeConverter.ColorToRGB255_Byte(color.r, color.g, color.b);

        for (int i = 0; i < 5; i++)
        {
            if (i != index) part += "\"N\"";
            else part += $"[{byteColor[0]}, {byteColor[1]}, {byteColor[2]}]";
            if (i < 4) part += ",";
        }

        part += "],\"model\":\"default\"}";
        await SendRequestColorScheme(part);
    }

    UnityWebRequest SetColorSchemeRequest(string request, byte[] dataBytes)
    {
        UnityWebRequest colorSchemeInfoRequest = new UnityWebRequest(request, "GET");
        colorSchemeInfoRequest.uploadHandler = new UploadHandlerRaw(dataBytes);
        colorSchemeInfoRequest.downloadHandler = new DownloadHandlerBuffer();
        colorSchemeInfoRequest.SetRequestHeader("Content-Type", "application/json");
        colorSchemeInfoRequest.SetRequestHeader("Accept", " text/plain");
        return colorSchemeInfoRequest;
    }

    async Task SendRequestColorScheme(string data = "{\"model\" : \"default\"}")
    {
        string requestAddress = API_ADDRESS;
        var dataBytes = Encoding.UTF8.GetBytes(data);

        UnityWebRequest colorSchemeInfoRequest = SetColorSchemeRequest(requestAddress, dataBytes);
        UnityWebRequest.Result result = await colorSchemeInfoRequest.SendWebRequest();

        if (
            result == UnityWebRequest.Result.ConnectionError
            ||
            result == UnityWebRequest.Result.ProtocolError)
        {
            Popup.Instance.RequestErrorMessage(colorSchemeInfoRequest.error);
        }
        else
        {
            try
            {
                JObject t = JsonConvert.DeserializeObject<JObject>(colorSchemeInfoRequest.downloadHandler.text);
                List<int>[] colors = t.GetValue("result").ToObject<List<int>[]>();
                List<Color> temp = CreateListOfColorsFromIntArray(colors);
                CurrentColors = temp;
                ColorReady?.Invoke(temp.ToArray());
            }
            catch (JsonException e)
            {
                Popup.Instance.RequestErrorMessage(e.Message);
            }
        }

    }

    private List<Color> CreateListOfColorsFromIntArray(List<int>[] colors)
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




