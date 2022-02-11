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
            if (_color != value)
            {
                _color = value;
                image.color = value;
            }
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
        button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(RequestColorChanger);
        toggle = transform.parent.GetComponentInChildren<Toggle>();
        toggle.onValueChanged.AddListener(SetDirty);
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
    /// Takes color
    /// </summary>
    public void SetColor(Color color, bool setDirty = false)
    {
        Color = ColorRangeConverter.ColorFromRGB255_Color(
            color.r,
            color.g,
            color.b
            );
        image.color = Color;
        Dirty = setDirty;
    }

    public void SetDirty(bool setDirty) => Dirty = setDirty;

    /// <summary>
    /// Enables ColorItem to use the colorChanger
    /// </summary>
    public void RequestColorChanger() => Popup.Instance.RequestColorChanger(this);
}
