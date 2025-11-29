using Godot;

namespace FryZeroGodot.Root.UI.Background;

[Tool]

[GlobalClass ]

public partial class Background : Control
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


    private void EditorOnReady()
    {
        UpdateBackgroundRectangle();
    }

    private void GameOnReady()
    {

    }

    private void UpdateBackgroundRectangle()
    {
        if (_backgroundRect == null)
        {
            CreateBackgroundRectangle();
        }
        SetBackgroundRectColor();
        SetAnchorsToFullScreen();
    }

    private void CreateBackgroundRectangle()
    {
        _backgroundRect = new ColorRect();
        AddChild(_backgroundRect);
    }

    private void SetAnchorsToFullScreen()
    {
        _backgroundRect.AnchorLeft = 0;
        _backgroundRect.AnchorRight = 1;
        _backgroundRect.AnchorTop = 0;
        _backgroundRect.AnchorBottom = 1;
    }
    private void SetBackgroundRectColor()
    {
        _backgroundRect.Color = BackgroundColor;
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
