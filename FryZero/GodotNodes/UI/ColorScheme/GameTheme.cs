using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;
public static class GameTheme
{
    private static GameThemeData _themeData;

    private static ShaderMaterial _themedMaterial;

    private static void UpdateShader()
    {
        // foreach (var color in Enum.GetValues<ThemeColor>())
        // {
        //     SetShaderColor(color);
        // }
        //GetThemeMaterial().SetShaderParameter(GetParameterString(ThemeColor.Light), GetThemeData().LightColor);
        // GetThemeMaterial().SetShaderParameter("light_highlight_color", GetThemeData().LightHighlightColor);
        // GetThemeMaterial().SetShaderParameter("light_shadow_color", GetThemeData().LightShadowColor);
        // GetThemeMaterial().SetShaderParameter("light_accent_color", GetThemeData().LightAccentColor);
        // GetThemeMaterial().SetShaderParameter("dark_color", GetThemeData().DarkColor);
        // GetThemeMaterial().SetShaderParameter("dark_highlight_color", GetThemeData().DarkHighlightColor);
        // GetThemeMaterial().SetShaderParameter("dark_shadow_color", GetThemeData().DarkShadowColor);
        // GetThemeMaterial().SetShaderParameter("dark_accent_color", GetThemeData().DarkAccentColor);
    }

    private static string GetParameterString(ThemeColor color) => color.ToString();

    private static void SetShaderColor(ThemeColor color) =>
        GetThemeMaterial().SetShaderParameter(GetParameterString(color), GetThemeColor(color));

    public static ShaderMaterial GetThemeMaterial()
    {
        if (_themedMaterial is not null) return _themedMaterial;
        var material = new ShaderMaterial();
        material.Shader = GD.Load<Shader>("res://GodotNodes/Visuals/HueShift.gdshader");
        UpdateShader();
        _themedMaterial = material;
        return _themedMaterial;
    }

    private static GameThemeData GetThemeData()
    {
        if (_themeData is not null) return _themeData;
        var themeData = ResourceLoader.Load<GameThemeData>("res://assets/UI/CurrentColorTheme.tres");
        _themeData = themeData;
        return _themeData;
    }

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
            _ => Colors.White
        };


}
