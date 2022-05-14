using UnityEngine;
using UnityEngine.UI;

public class ColorItem : MonoBehaviour
{
    public int Index;
    public Color Color
    {
        get => _color;
        private set
        {
            _color = value;
            image.color = value;
        }
    }

    private Color _color;

    public bool Dirty
    {
        get; private set;
    }

    Image image;
    Button button;
    Toggle toggle;

    void Start()
    {
        image = GetComponent<Image>();
        Color = Color.black;
        image.color = Color;
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(RequestColorChanger);
        toggle = transform.parent.GetComponentInChildren<Toggle>();
        toggle.onValueChanged.AddListener(SetDirty);
    }

    /// <summary>
    /// Takes color from given Color[] array using preset index in component
    /// </summary>
    public void SetColorFromRGB255Array(Color[] colors)
    {
        Color = ColorRangeConverter.ColorFromRGB255_Color(
            colors[Index].r,
            colors[Index].g,
            colors[Index].b,
            1
            );

        image.color = Color;
    }

    /// <summary>
    /// Takes color
    /// </summary>
    public void SetColor(Color color) => Color = color;
    public void SetDirty(bool setDirty) => Dirty = setDirty;

    /// <summary>
    /// Enables ColorItem to use the colorChanger
    /// </summary>
    public void RequestColorChanger() => 
        Popup.Instance.RequestColorChanger(this);
}
