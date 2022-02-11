using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [Header("Parts")]
    [SerializeField]
    Image image;

    [SerializeField]
    Button generate;

    [SerializeField]
    Button cancel;

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
        cancel.onClick.AddListener(Cancel);
    }

    public void Connect(ColorItem colorItem)
    {
        requestingItem = colorItem;
        string[] colorParts = ColorRangeConverter.ColorToRGB255_String(colorItem.Color);
        
        red.text = colorParts[0];
        green.text = colorParts[1];
        blue.text = colorParts[2];
        
        red.onEndEdit.AddListener(UpdateColor);
        green.onEndEdit.AddListener(UpdateColor);
        blue.onEndEdit.AddListener(UpdateColor);
        UpdateColor("");
    }

    void UpdateColor(string txt)
    {
        image.color = ColorRangeConverter.ColorFromRGB255_Color(red.text, green.text, blue.text);
        requestingItem.SetColor(image.color, true);
    }

    void Generate()
    {
        ApiConnector.Instance.RequestColorScheme(requestingItem.Color, requestingItem.Index);
    }

    void Cancel()
    {
        Clean();
        Popup.Instance.RequestHide(this.gameObject);
    }

    void Clean()
    {
        requestingItem = null;
    }
}