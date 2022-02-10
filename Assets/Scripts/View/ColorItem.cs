using UnityEngine;
using UnityEngine.UI;

public class ColorItem : MonoBehaviour
{
    public int Index;
    public Color Color;

    Image image;
    Button button;

    void Start()
    {
        image = GetComponent<Image>();
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(RequestColorChanger);
    }

    /// <summary>
    /// Takes color from given Color[] array using preset index in component
    /// </summary>
    public void SetColor(Color[] colors)
    {
        Color = ColorRangeConverter.ColorFromRGB255_Color(
            colors[Index].r,
            colors[Index].g,
            colors[Index].b
            );
        image.color = Color;
    }

    /// <summary>
    /// Enables ColorItem to use the colorChanger
    /// </summary>
    public void RequestColorChanger() => Popup.Instance.RequestColorChanger(this);
}
