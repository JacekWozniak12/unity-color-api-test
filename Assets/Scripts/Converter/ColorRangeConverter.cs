using UnityEngine;

public static class ColorRangeConverter
{
    public static Color ColorFromRGB255(float r, float g, float b)
    {
        float red = r / 255;
        float green = g / 255;
        float blue = b / 255;

        return new Color(red, green, blue);
    }

    public static byte[] ColorToRGB255(float r, float g, float b)
    {
        byte red = (byte)(r * 255);
        byte green = (byte)(g * 255);
        byte blue = (byte)(b * 255);

        return new byte[] { red, green, blue };
    }
}
