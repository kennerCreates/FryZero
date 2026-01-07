using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;

[GlobalClass]
public partial class GameThemeData : Resource
{
    [Export] public Color LightColor { get; set; } = Colors.White;

    [Export] public Color LightHighlightColor { get; set; } = Colors.Yellow;

    [Export] public Color LightShadowColor { get; set; } = Colors.Magenta;

    [Export] public Color LightAccentColor { get; set; }= Colors.Red;

    [Export] public Color DarkColor { get; set; }= Colors.Black;

    [Export] public Color DarkHighlightColor { get; set; }= Colors.Cyan;

    [Export] public Color DarkShadowColor { get; set; }= Colors.Blue;

    [Export] public Color DarkAccentColor { get; set; }= Colors.Green;


}
