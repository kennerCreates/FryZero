using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotNodes.NodeModels;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.Background;

[GlobalClass]

public partial class GodotBackground : RootNode
{
    private ColorRect _backgroundRect;

    private Sprite2D _backgroundSprite;

    [Export] public ThemeColor BackgroundColor { get; set; }

    [Export] public Texture2D BackgroundTexture { get; set; }

    [Export] public ThemeColor PatternColor { get; set; }

    private void CreateBackgroundRect()
    {
        _backgroundRect = new ColorRect();
        _backgroundRect.Size = new Vector2(4000, 4000);
        _backgroundRect.Position = new Vector2(-2000, -2000);
        AddChild(_backgroundRect);
    }
    private void UpdateColor()
    {
        _backgroundRect.Color = GameTheme.GetThemeColor(BackgroundColor);
    }

    private void CreateBackgroundSprite()
    {
        _backgroundSprite = BackgroundTexture.AddSprite2DAsChild(this);
        UpdateSprite();
    }
    private void UpdateSprite()
    {
        _backgroundSprite.Texture = BackgroundTexture;
        _backgroundSprite.Scale = new Vector2(5, 5);
        _backgroundSprite.RegionEnabled = true;
        _backgroundSprite.RegionRect = new Rect2(0, 0, 800, 800);
        _backgroundSprite.TextureRepeat = TextureRepeatEnum.Enabled;
        _backgroundSprite.Modulate = GameTheme.GetThemeColor(PatternColor);
    }

    protected override void OnReady()
    {

        CreateBackgroundRect();
        CreateBackgroundSprite();
        UpdateColor();
        _backgroundRect.MouseFilter = Control.MouseFilterEnum.Ignore;
    }

}
