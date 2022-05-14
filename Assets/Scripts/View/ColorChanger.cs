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
    Button copyToClipboard, close;

    [Header("Inputs")]
    [SerializeField]
    TMP_InputField red, green, blue;

    ColorItem requestingItem;

    void Start()
    {
        copyToClipboard.onClick.AddListener(CopyIntoClipboard);
        close.onClick.AddListener(Close);
    }

    void Update()
    {
        if (CtrlAndC_Check()) if (requestingItem != null) CopyIntoClipboard();
    }

    private static bool CtrlAndC_Check() => Input.GetKeyDown(KeyCode.C) && Input.GetKey(KeyCode.LeftControl);

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

        image.color = colorItem.Color;
    }

    void CopyIntoClipboard() => GUIUtility.systemCopyBuffer = ColorRangeConverter.ColorToRGB255_String(requestingItem.Color);
    void UpdateColor(string txt)
    {
        try
        {
            if (float.TryParse(red.text, out float r)) r = Mathf.Clamp(r, 0, 255);
            if (float.TryParse(green.text, out float g)) g = Mathf.Clamp(g, 0, 255);
            if (float.TryParse(blue.text, out float b)) b = Mathf.Clamp(b, 0, 255);

            red.text = r.ToString();
            green.text = g.ToString();
            blue.text = b.ToString();

            Color color = ColorRangeConverter.ColorFromRGB255_Color(
                r, g, b, 1);

            image.color = color;
            requestingItem.SetColor(color);
        }
        catch (Exception e)
        {
            Popup.Instance.RequestErrorMessage(e.Message);
        }
    }

    void Close()
    {
        UpdateColor("");
        Clean();
        Popup.Instance.RequestHide(this.gameObject);
    }

    void Clean() => requestingItem = null;
}