using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using FryZeroGodot.GodotInterface.UI.GameTheme;
using Godot;
using GameThemeData = FryZeroGodot.GodotInterface.UI.GameTheme.GameThemeData;

namespace FryZeroGodot.Statics.UI.GameTheme;

public static class ThemeExtensions
{
    public static void UpdateAllThemeColors(this ShaderMaterial shader, GameThemeData themeData)
    {
        foreach (var color in Enum.GetValues<ThemeColor>()) shader.SetShaderColor(color, themeData);
    }
    private static void SetShaderColor(this ShaderMaterial shader, ThemeColor color, GameThemeData themeData) =>
        shader.SetShaderParameter(color.GetParameterString(), color.GetThemeColor(themeData));

    public static string GetParameterString(this ThemeColor color) =>
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

    public static Color GetThemeColor(this ThemeColor color, IGameThemeData themeData) =>
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

    //TODO: GetPieceTextureFromStyle is currently untested
    public static Texture2D GetPieceTextureFromStyle(this PieceStyle style) =>
        style switch
        {
            PieceStyle.Little => GD.Load<Texture2D>("res://assets/Pieces/LittleStyle.png"),
            PieceStyle.Tiny => GD.Load<Texture2D>("res://assets/Pieces/TinyStyle.png"),
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };
}
