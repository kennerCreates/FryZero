using Godot;

namespace FryZeroGodot.GodotNodes.Game;

public static class ColorScheme
{
    public static ShaderMaterial CreateHueShiftShader()
    {
        var material = new ShaderMaterial();
        material.Shader = GD.Load<Shader>("res://GodotNodes/Visuals/HueShift.gdshader");
        return material;
    }
}
