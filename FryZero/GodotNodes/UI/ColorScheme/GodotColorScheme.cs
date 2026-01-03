using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;

[GlobalClass]

public partial class GodotColorScheme : Node2D
{
    [Signal]
    public delegate void ColorSchemeInitializedEventHandler();

    public bool IsInitialized { get; private set; }

    [Export] public Color LightColor { get; set; } = Colors.White;

    [Export] public Color LightHighlightColor { get; set; } = Colors.Yellow;

    [Export] public Color LightShadowColor { get; set; } = Colors.Magenta;

    [Export] public Color LightAccentColor { get; set; }= Colors.Red;

    [Export] public Color DarkColor { get; set; }= Colors.Black;

    [Export] public Color DarkHighlightColor { get; set; }= Colors.Cyan;

    [Export] public Color DarkShadowColor { get; set; }= Colors.Blue;

    [Export] public Color DarkAccentColor { get; set; }= Colors.Green;


    private ShaderMaterial _material;

    private void CreateHueShiftShader()
    {
        _material = new ShaderMaterial();
        _material.Shader = GD.Load<Shader>("res://GodotNodes/Visuals/HueShift.gdshader");
        UpdateShader();
    }

    private void UpdateShader()
    {
        _material.SetShaderParameter("light_color", LightColor);
        _material.SetShaderParameter("light_highlight_color", LightHighlightColor);
        _material.SetShaderParameter("light_shadow_color", LightShadowColor);
        _material.SetShaderParameter("light_accent_color", LightAccentColor);
        _material.SetShaderParameter("dark_color", DarkColor);
        _material.SetShaderParameter("dark_highlight_color", DarkHighlightColor);
        _material.SetShaderParameter("dark_shadow_color", DarkShadowColor);
        _material.SetShaderParameter("dark_accent_color", DarkAccentColor);
    }

    public new ShaderMaterial GetMaterial()
    {
        return _material;
    }


    public override void _Ready()
    {
        CreateHueShiftShader();
        IsInitialized = true;
        EmitSignal(SignalName.ColorSchemeInitialized);
    }

}
