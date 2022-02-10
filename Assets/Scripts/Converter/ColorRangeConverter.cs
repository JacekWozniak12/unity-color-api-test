using UnityEngine;

public static class ColorRangeConverter
{
    public static Color ColorFromRGB255(float r, float g, float b)
    {
        Debug.Log(r);
        float red = r/255;
        float green = g/255;
        float blue = b/255;

        return new Color(red, green, blue); 
    }
}
