using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;

public static class ColorSchemeOperations
{
    public static Color ModulateToThemeColor(ThemeColors color) =>
        color switch
        {
            ThemeColors.Light => Colors.White,
            ThemeColors.LightHighlight => Colors.Yellow,
            ThemeColors.LightShadow => Colors.Magenta,
            ThemeColors.LightAccent => Colors.Red,
            ThemeColors.Dark => Colors.Black,
            ThemeColors.DarkHighlight => Colors.Cyan,
            ThemeColors.DarkShadow => Colors.Blue,
            ThemeColors.DarkAccent => Colors.Green,
            _ => Colors.White
        };

}
