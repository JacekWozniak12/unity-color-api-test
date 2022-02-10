using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class ApiConnector : MonoBehaviour
{
    const string API_ADDRESS = "http://colormind.io/api/";

    public void GetColors()
    {
        StartCoroutine(RequestColorScheme());
    }

    IEnumerator RequestColorScheme()
    {
        string request = API_ADDRESS;
        Debug.Log(request);

        string data = "{\"model\" : \"default\"}";

        var dataBytes = Encoding.UTF8.GetBytes(data);

        using (UnityWebRequest colorSchemeInfoRequest = new UnityWebRequest(request, "GET"))
        {
            colorSchemeInfoRequest.uploadHandler = new UploadHandlerRaw(dataBytes);
            colorSchemeInfoRequest.downloadHandler = new DownloadHandlerBuffer();
            colorSchemeInfoRequest.SetRequestHeader("Content-Type", "application/json");
            colorSchemeInfoRequest.SetRequestHeader("Accept", " text/plain");
            yield return colorSchemeInfoRequest.SendWebRequest();

            if (
                colorSchemeInfoRequest.result == UnityWebRequest.Result.ConnectionError
                ||
                colorSchemeInfoRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(colorSchemeInfoRequest.error);
            }
            else
                Debug.Log(colorSchemeInfoRequest.downloadHandler.text);

            JObject t = JsonConvert.DeserializeObject<JObject>(colorSchemeInfoRequest.downloadHandler.text);
            List<int>[] colors = t.GetValue("result").ToObject<List<int>[]>();

            foreach (List<int> l in colors)
            {
                Color c = new Color(l[0], l[1], l[2]);

                string message = string.Format(
                    "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
                    (byte)(c.r * 255f),
                    (byte)(c.g * 255f),
                    (byte)(c.b * 255f),
                    c);

                Debug.Log(message);
            }
        }
    }
}



