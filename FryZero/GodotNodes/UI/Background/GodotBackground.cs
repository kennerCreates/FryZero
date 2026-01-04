using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;
using Color = Godot.Color;

namespace FryZeroGodot.GodotNodes.UI.Background;

[GlobalClass]

public partial class GodotBackground : Node2D
{
    private ColorRect _backgroundRect;

    private Sprite2D _backgroundSprite;

    [Export] public ThemeColor BackgroundColor { get; set; }
    private void CreateBackgroundRect()
    {
        _backgroundRect = new ColorRect();
        _backgroundRect.Size = new Vector2(4000, 4000);
        _backgroundRect.Position = new Vector2(-2000, -2000);
        AddChild(_backgroundRect);
    }
    private void UpdateColor()
    {
        _backgroundRect.Color = ColorScheme.ModulateToThemeColor(BackgroundColor);
    }

    [Export] public Texture2D BackgroundTexture { get; set; }

    [Export] public ThemeColor PatternColor { get; set; }
    private void CreateBackgroundSprite()
    {
        _backgroundSprite = new Sprite2D();
        AddChild(_backgroundSprite);
        UpdateSprite();
    }
    private void UpdateSprite()
    {
        _backgroundSprite.Texture = BackgroundTexture;
        _backgroundSprite.Scale = new Vector2(5, 5);
        _backgroundSprite.RegionEnabled = true;
        _backgroundSprite.RegionRect = new Rect2(0, 0, 800, 800);
        _backgroundSprite.TextureRepeat = TextureRepeatEnum.Enabled;
        _backgroundSprite.Modulate = ColorScheme.ModulateToThemeColor(PatternColor);
    }

    public GodotColorScheme ColorScheme;
    public override void _EnterTree()
    {

        ColorScheme = GetParent<GodotColorScheme>();
        if (ColorScheme.IsInitialized)
        {
            OnColorSchemeReady();
        }
        else
        {
            ColorScheme.ColorSchemeInitialized += OnColorSchemeReady;
        }
    }

    private void OnColorSchemeReady()
    {
        CreateBackgroundRect();
        CreateBackgroundSprite();
        UpdateColor();
        _backgroundRect.MouseFilter = Control.MouseFilterEnum.Ignore;
    }

}
