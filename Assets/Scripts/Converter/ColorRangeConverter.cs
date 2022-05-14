using System.Drawing;
using System;
using UnityEngine;

/// <summary>
/// Unity enforces the (0, 1) range in its Color object, so for conversion from and to (0, 1)
/// use this class
/// </summary>
public static class ColorRangeConverter
{
    public static Color ColorFromRGB255_Color(float r, float g, float b, float a)
    {
        float red = r / 255;
        float green = g / 255;
        float blue = b / 255;

        return new Color(red, green, blue, a);
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
    public static string[] ColorToRGB255_StringArray(Color color) =>
        ColorToRGB255_StringArray(color.r, color.g, color.b);

    /// <summary>
    /// Returns specified string from array of red, green and blue.
    /// </summary>
    public static string ColorToRGB255_StringArrayPart(Color color, int part) =>
        ColorToRGB255_StringArrayPart(color.r, color.g, color.b, part);

    public static string ColorToRGB255_String(Color color) =>
        ColorToRGB255_String(color.r, color.g, color.b);

    /// <summary>
    /// Returns string with params of red, green and blue.
    /// </summary>
    public static string ColorToRGB255_String(float r, float g, float b)
    {
        string red = (r * 255).ToString();
        string green = (g * 255).ToString();
        string blue = (b * 255).ToString();

        return $"RGB({red}, {green}, {blue})";
    }

    /// <summary>
    /// Returns string array with params of red, green and blue.
    /// </summary>
    public static string[] ColorToRGB255_StringArray(float r, float g, float b)
    {
        string red = (r * 255).ToString();
        string green = (g * 255).ToString();
        string blue = (b * 255).ToString();

        return new string[] { red, green, blue };
    }

    /// <summary>
    /// Returns specified string param from array of red, green and blue.
    /// </summary>
    public static string ColorToRGB255_StringArrayPart(float r, float g, float b, int part)
    {
        string red = (r * 255).ToString();
        string green = (g * 255).ToString();
        string blue = (b * 255).ToString();

        return new string[] { red, green, blue }[part];
    }
}
