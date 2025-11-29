using Godot;

namespace FryZeroGodot.Root.UI.Background;

[Tool]

[GlobalClass ]

public partial class GodotBackground : Control
{
    [Export] public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            UpdateBackgroundRectangle();
        }
    }

    private Color _backgroundColor = Colors.White;

    private ColorRect _backgroundRect;

    private void UpdateBackgroundRectangle()
    {
        if (_backgroundRect == null)
        {
            CreateBlankBackground();
        }
        SetBackgroundColor(_backgroundRect, _backgroundColor);
        SetAnchorsToFullScreen(_backgroundRect);
    }

    private void CreateBlankBackground()
    {
        _backgroundRect = new ColorRect();
        AddChild(_backgroundRect);
    }

    private void SetAnchorsToFullScreen(ColorRect rect)
    {
        rect.AnchorLeft = 0;
        rect.AnchorRight = 1;
        rect.AnchorTop = 0;
        rect.AnchorBottom = 1;
    }
    private void SetBackgroundColor(ColorRect rect, Color color)
    {
        rect.Color = color;
    }

    private void EditorOnReady()
    {
        UpdateBackgroundRectangle();
    }

    private void GameOnReady()
    {

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
