using Godot;

namespace FryZeroGodot.GodotInterface.UI.GameTheme;

public interface IGameThemeData
{
    Color LightColor { get; set; }
    Color LightHighlightColor { get; set; }
    Color LightShadowColor { get; set; }
    Color LightAccentColor { get; set; }
    Color DarkColor { get; set; }
    Color DarkHighlightColor { get; set; }
    Color DarkShadowColor  { get; set; }
    Color DarkAccentColor { get; set; }
}
