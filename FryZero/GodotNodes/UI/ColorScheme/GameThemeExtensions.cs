using Godot;

namespace FryZeroGodot.GodotNodes.UI.ColorScheme;

public static class GameThemeExtensions
{
    public static void UpdateShader(ShaderMaterial material, GameThemeData data)
    {
        material.SetShaderParameter("light_color", data.LightColor);
        material.SetShaderParameter("light_highlight_color", data.LightHighlightColor);
        material.SetShaderParameter("light_shadow_color", data.LightShadowColor);
        material.SetShaderParameter("light_accent_color", data.LightAccentColor);
        material.SetShaderParameter("dark_color", data.DarkColor);
        material.SetShaderParameter("dark_highlight_color", data.DarkHighlightColor);
        material.SetShaderParameter("dark_shadow_color", data.DarkShadowColor);
        material.SetShaderParameter("dark_accent_color", data.DarkAccentColor);
    }
}
