using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using FryZeroGodot.GodotInterface.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.Statics.UI.GameTheme;

public static class ThemeExtensions
{
    public static void UpdateAllThemeColors(this ShaderMaterial shader, GameThemeData themeData)
    {
        foreach (var color in Enum.GetValues<ThemeColor>()) shader.SetShaderColor(color, themeData);
    }
    private static void SetShaderColor(this ShaderMaterial shaderMaterial, ThemeColor color, GameThemeData themeData) =>
        shaderMaterial.SetShaderParameter(color.GetParameterString(), color.GetThemeColor(themeData));

    private static string GetParameterString(this ThemeColor color) =>
        color switch
        {
            ThemeColor.Light => "light_color",
            ThemeColor.LightHighlight => "light_highlight_color",
            ThemeColor.LightShadow => "light_shadow_color",
            ThemeColor.LightAccent => "light_accent_color",
            ThemeColor.Dark => "dark_color",
            ThemeColor.DarkHighlight => "dark_highlight_color",
            ThemeColor.DarkShadow => "dark_shadow_color",
            ThemeColor.DarkAccent => "dark_accent_color",
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

    private static Color GetThemeColor(this ThemeColor color, GameThemeData themeData) =>
        color switch
        {
            ThemeColor.Light => themeData.LightColor,
            ThemeColor.LightHighlight => themeData.LightHighlightColor,
            ThemeColor.LightShadow => themeData.LightShadowColor,
            ThemeColor.LightAccent => themeData.LightAccentColor,
            ThemeColor.Dark => themeData.DarkColor,
            ThemeColor.DarkHighlight => themeData.DarkHighlightColor,
            ThemeColor.DarkShadow => themeData.DarkShadowColor,
            ThemeColor.DarkAccent => themeData.DarkAccentColor,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

    public static Color GetModulateColor(this ThemeColor color) =>
        color switch
        {
            ThemeColor.Light => Colors.White,
            ThemeColor.LightHighlight => Colors.Yellow,
            ThemeColor.LightShadow => Colors.Magenta,
            ThemeColor.LightAccent => Colors.Red,
            ThemeColor.Dark => Colors.Black,
            ThemeColor.DarkHighlight => Colors.Cyan,
            ThemeColor.DarkShadow => Colors.Blue,
            ThemeColor.DarkAccent => Colors.Green,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
}
