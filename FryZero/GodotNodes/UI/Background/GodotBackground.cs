using FryZeroGodot.Config.Enums;
using Godot;
using Color = Godot.Color;

namespace FryZeroGodot.GodotNodes.UI.Background;

[GlobalClass]

public partial class GodotBackground : Node2D
{
    private ColorRect _backgroundRect;
    private Color _backgroundColor;
    private Sprite2D _backgroundSprite;
    private Texture2D _backgroundTexture;
    private Color _patternColor;

    [Export]
    public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            if (_backgroundRect != null) UpdateColor();
        }
    }
    private void CreateBackgroundRect()
    {
        _backgroundRect = new ColorRect();
        _backgroundRect.Size = new Vector2(4000, 4000);
        _backgroundRect.Position = new Vector2(-2000, -2000);
        AddChild(_backgroundRect);
    }
    private void UpdateColor()
    {
        _backgroundRect.Color = _backgroundColor;
    }

    [Export]
    public Texture2D BackgroundTexture
    {
        get => _backgroundTexture;
        set
        {
            _backgroundTexture = value;
            if (_backgroundSprite != null) UpdateSprite();
        }
    }
    [Export]
    public Color PatternColor
    {
        get => _patternColor;
        set
        {
            _patternColor = value;
            if (_backgroundSprite != null) UpdateSprite();
        }
    }
    private void CreateBackgroundSprite()
    {
        _backgroundSprite = new Sprite2D();
        AddChild(_backgroundSprite);
        UpdateSprite();
    }
    private void UpdateSprite()
    {
        _backgroundSprite.Texture = _backgroundTexture;
        _backgroundSprite.Scale = new Vector2(5, 5);
        _backgroundSprite.RegionEnabled = true;
        _backgroundSprite.RegionRect = new Rect2(0, 0, 800, 800);
        _backgroundSprite.TextureRepeat = TextureRepeatEnum.Enabled;
        _backgroundSprite.Modulate = PatternColor;
    }
    private void EditorOnReady()
    {
        UpdateColor();

    }
    private void GameOnReady()
    {
        _backgroundRect.MouseFilter = Control.MouseFilterEnum.Ignore;
    }
    public override void _EnterTree()
    {
        CreateBackgroundRect();
        CreateBackgroundSprite();
    }

    public override void _Ready()
    {
        if (Engine.IsEditorHint())
        {
            EditorOnReady();
        }
        else
        {
            EditorOnReady();
            GameOnReady();
        }
    }

}
