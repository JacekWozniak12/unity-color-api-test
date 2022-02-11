using System;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine.Events;

public class ApiConnector : MonoBehaviour
{
    const string API_ADDRESS = "http://colormind.io/api/";

    public List<Color> CurrentColors = new List<Color>(5);
    public static ApiConnector Instance;

    void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Tries to connect with server specified in API_ADDRESS
    /// </summary>
    public UnityEvent<Color[]> ColorReady = new UnityEvent<Color[]>();

    public void RequestColorScheme()
    {
        StartCoroutine(SendRequestColorScheme());
    }

    public void RequestColorScheme(Dictionary<int, Color> dictionary)
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
        Debug.Log(part);
        StartCoroutine(SendRequestColorScheme(part));
    }

    public void RequestColorScheme(Color color, int index)
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
        Debug.Log(part);
        StartCoroutine(SendRequestColorScheme(part));
    }

    IEnumerator SendRequestColorScheme(string data = "{\"model\" : \"default\"}")
    {
        string request = API_ADDRESS;
        Debug.Log(data);
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
            List<Color> temp = new List<Color>();

            foreach (List<int> l in colors)
            {
                Color c = new Color(l[0], l[1], l[2]);

                string message = string.Format(
                    "<color=#{0:X2}{1:X2}{2:X2}>{3}</color>",
                    (byte)(c.r),
                    (byte)(c.g),
                    (byte)(c.b),
                    c);

                Debug.Log(message);

                temp.Add(c);
            }

            CurrentColors = temp;
            ColorReady?.Invoke(temp.ToArray());
        }
    }
}



