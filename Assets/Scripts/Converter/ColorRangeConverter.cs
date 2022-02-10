using UnityEngine;

/// <summary>
/// Unity messes up the RGB model enforcing the (0, 1) range in its 
/// Color component, so I have to make this not really clean converter.
/// TODO: check if there is not better solutions for that.
/// </summary>
public static class ColorRangeConverter
{
    public static Color ColorFromRGB255_Color(float r, float g, float b)
    {
        float red = r / 255;
        float green = g / 255;
        float blue = b / 255;

        return new Color(red, green, blue);
    }

    public static byte[] ColorToRGB255_Byte(float r, float g, float b)
    {
        byte red = (byte)(r * 255);
        byte green = (byte)(g * 255);
        byte blue = (byte)(b * 255);

        return new byte[] { red, green, blue };
    }

    /// <summary>
    /// Returns string array with params of red, green and blue.
    /// </summary>
    public static string[] ColorToRGB255_String(Color color)
        => ColorToRGB255_String(color.r, color.g, color.b);

    /// <summary>
    /// Returns specified string from array of red, green and blue.
    /// </summary>
    public static string ColorToRGB255_String(Color color, int part)
        => ColorToRGB255_String(color.r, color.g, color.b, part);

    /// <summary>
    /// Returns string array with params of red, green and blue.
    /// </summary>
    public static string[] ColorToRGB255_String(float r, float g, float b)
    {
        string red = (r * 255).ToString();
        string green = (g * 255).ToString();
        string blue = (b * 255).ToString();

        return new string[] { red, green, blue };
    }

    /// <summary>
    /// Returns specified string param from array of red, green and blue.
    /// </summary>
    public static string ColorToRGB255_String(float r, float g, float b, int part)
    {
        string red = (r * 255).ToString();
        string green = (g * 255).ToString();
        string blue = (b * 255).ToString();

        return new string[] { red, green, blue }[part];
    }
}
