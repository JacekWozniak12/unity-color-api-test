using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ColorChanger : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField]
    Image image;

    [SerializeField]
    Button generate;

    [SerializeField]
    Button close;

    ColorItem requestingItem;

    [Header("Inputs")]
    [SerializeField]
    TMP_InputField red;

    [SerializeField]
    TMP_InputField green;

    [SerializeField]
    TMP_InputField blue;

    void Start()
    {
        generate.onClick.AddListener(Generate);
        close.onClick.AddListener(Close);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl))
            if(requestingItem != null) CopyIntoClipboard();
    }

    public void Connect(ColorItem colorItem)
    {
        requestingItem = colorItem;
        string[] colorParts = ColorRangeConverter.ColorToRGB255_StringArray(colorItem.Color);

        red.text = colorParts[0];
        green.text = colorParts[1];
        blue.text = colorParts[2];

        red.onEndEdit.AddListener(UpdateColor);
        green.onEndEdit.AddListener(UpdateColor);
        blue.onEndEdit.AddListener(UpdateColor);

        image.color = ColorRangeConverter.ColorFromRGB255_Color(red.text, green.text, blue.text);
    }

    void UpdateColor(string txt)
    {
        try
        {
            if (float.TryParse(red.text, out float r)) r = Mathf.Clamp(r, 0, 255);
            if (float.TryParse(green.text, out float g)) g = Mathf.Clamp(g, 0, 255);
            if (float.TryParse(blue.text, out float b)) b = Mathf.Clamp(b, 0, 255);
            Color color = ColorRangeConverter.ColorFromRGB255_Color(r, g, b);

            image.color = color;
            requestingItem.SetColor(color);
        }
        catch (Exception e) { }
    }

    void Generate()
    {
        ApiConnector.Instance.RequestColorScheme(requestingItem.Color, requestingItem.Index);
    }

    void Close()
    {
        UpdateColor("");
        Clean();
        Popup.Instance.RequestHide(this.gameObject);
    }

    void Clean()
    {
        requestingItem = null;
    }
    
    void CopyIntoClipboard()
    {
        GUIUtility.systemCopyBuffer = ColorRangeConverter.ColorToRGB255_String(requestingItem.Color);
    }
}