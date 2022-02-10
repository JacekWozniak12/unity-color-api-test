using UnityEngine;
using UnityEngine.UI;

public class ColorItem : MonoBehaviour
{
    public int Index;
    public Color Color;

    Image image;
    
    void Start()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// Takes color from given Color[] array using preset index in component
    /// </summary>
    public void SetColor(Color[] colors)
    {
        Color = ColorRangeConverter.ColorFromRGB255(
            colors[Index].r, 
            colors[Index].g, 
            colors[Index].b
            );
        image.color = Color;
    }
}
