using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;

[Tool]
[GlobalClass]

public partial class GodotColorScheme : Node2D
{


    [Export] public Color LightColor = Colors.White;

    [Export] public Color LightHighlightColor = Colors.Yellow;

    [Export] public Color LightShadowColor = Colors.Magenta;

    [Export] public Color LightAccentColor = Colors.Red;

    [Export] public Color DarkColor = Colors.Black;

    [Export] public Color DarkHighlightColor = Colors.Cyan;

    [Export] public Color DarkShadowColor = Colors.Blue;

    [Export] public Color DarkAccentColor = Colors.Green;


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



    public override void _Ready()
    {
        CreateHueShiftShader();
    }

}
