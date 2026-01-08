using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;
public partial class GameTheme : Node2D
{
    private static GameThemeData _themeData;

    private static ShaderMaterial _themedMaterial;

    public override void _Ready()
    {
        UpdateAllThemeColors();
    }

    private static void UpdateAllThemeColors()
    {
        foreach (var color in Enum.GetValues<ThemeColor>()) SetShaderColor(color);
    }

    private static void SetShaderColor(ThemeColor color) =>
        GetThemeMaterial().SetShaderParameter(GetParameterString(color), GetThemeColor(color));

    private static string GetParameterString(ThemeColor color) =>
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

    public static Color GetThemeColor(ThemeColor color) =>
        color switch
        {
            ThemeColor.Light => GetThemeData().LightColor,
            ThemeColor.LightHighlight => GetThemeData().LightHighlightColor,
            ThemeColor.LightShadow => GetThemeData().LightShadowColor,
            ThemeColor.LightAccent => GetThemeData().LightAccentColor,
            ThemeColor.Dark => GetThemeData().DarkColor,
            ThemeColor.DarkHighlight => GetThemeData().DarkHighlightColor,
            ThemeColor.DarkShadow => GetThemeData().DarkShadowColor,
            ThemeColor.DarkAccent => GetThemeData().DarkAccentColor,
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };

    private static Color GetModulateColor(ThemeColor color) =>
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

    public static ShaderMaterial GetThemeMaterial()
    {
        _themedMaterial ??= new ShaderMaterial
        {
            Shader = GD.Load<Shader>("res://GodotNodes/Visuals/HueShift.gdshader")
        };
        return _themedMaterial;
    }

    public static GameThemeData GetThemeData()
    {
        _themeData ??= ResourceLoader.Load<GameThemeData>("res://assets/UI/CurrentColorTheme.tres");
        return _themeData;
    }

    public static Color GetBackgroundColor() => GetModulateColor(GetThemeData().BackgroundColor);
    public static Color GetPatternColor() => GetModulateColor(GetThemeData().BackgroundPatternColor);
    public static int GetPatternScale() => GetThemeData().BackgroundPatternScale;
    public static Color GetDarkSquareColor() => GetModulateColor(GetThemeData().DarkSquareColor);
    public static Color GetLightSquareColor() => GetModulateColor(GetThemeData().LightSquareColor);
    public static Texture2D GetLightSquareTexture() => GetThemeData().LightSquareTexture;
    public static Texture2D GetDarkSquareTexture() => GetThemeData().DarkSquareTexture;
    public static int GetPieceDelay() => GetThemeData().PieceMovementDelay;
    public static PieceStyle GetPieceStyle() => GetThemeData().PieceStyle;
    public static Texture2D GetPieceAtlasTexture() => GetThemeData().PieceAtlasTexture;
    public static int GetPieceSize() => GetThemeData().PieceSize;
    public static int GetSquareSize() => GetThemeData().SquareSize;

}
