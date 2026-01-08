using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.Background;

[GlobalClass]

public partial class GodotBackground : Node2D
{
    private ColorRect _backgroundRect;

    private Sprite2D _backgroundSprite;

    private static ColorRect GetBackgroundRect(ColorRect background)
    {
        background ??= new ColorRect
        {
            Size = new Vector2(4000, 4000),
            Position = new Vector2(-2000, -2000),
            Material = GameTheme.GetThemeMaterial(),
            MouseFilter = Control.MouseFilterEnum.Ignore
        };
        background.Color = GameTheme.GetBackgroundColor();
        return background;
    }

    private static Sprite2D GetBackgroundSprite(Sprite2D sprite)
    {
        sprite ??= new Sprite2D
        {
            RegionEnabled = true,
            RegionRect = new Rect2(0, 0, 2000, 2000),
            TextureRepeat = TextureRepeatEnum.Enabled,
            Material = GameTheme.GetThemeMaterial()
        };
        sprite.Texture = GameTheme.GetThemeData().BackgroundTexture;
        sprite.Scale = new Vector2(GameTheme.GetPatternScale(), GameTheme.GetPatternScale());
        sprite.SelfModulate = GameTheme.GetPatternColor();
        return sprite;
    }

    public override void _Ready()
    {

        AddChild(GetBackgroundRect(_backgroundRect));
        AddChild(GetBackgroundSprite(_backgroundSprite));
    }

}
