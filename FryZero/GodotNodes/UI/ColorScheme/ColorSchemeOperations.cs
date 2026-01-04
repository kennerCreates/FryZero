using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;

public static class ColorSchemeOperations
{
    public static Color ModulateToShaderColor(ThemeColor color) =>
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
            _ => Colors.White
        };

    public static Color ModulateToThemeColor(this GodotColorScheme scheme, ThemeColor color) =>
        color switch
        {
            ThemeColor.Light => scheme.LightColor,
            ThemeColor.LightHighlight => scheme.LightHighlightColor,
            ThemeColor.LightShadow => scheme.LightShadowColor,
            ThemeColor.LightAccent => scheme.LightAccentColor,
            ThemeColor.Dark => scheme.DarkColor,
            ThemeColor.DarkHighlight => scheme.DarkHighlightColor,
            ThemeColor.DarkShadow => scheme.DarkShadowColor,
            ThemeColor.DarkAccent => scheme.DarkAccentColor,
            _ => Colors.White
        };

}
