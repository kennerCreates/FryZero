using Godot;

namespace FryZeroGodot.Root.UI.Background;

[Tool]

[GlobalClass ]

public partial class GodotBackground : Control
{
    private ColorRect _backgroundRect;
    private Color _backgroundColor = Colors.White;
    [Export] public Color BackgroundColor
    {
        get => GetColor();
        set => SetColor(value);
    }
    private Color GetColor()
    {
        return _backgroundColor;
    }
    private void SetColor(Color value)
    {
        _backgroundColor = value;
    }

    private void CreateBackgroundRect()
    {
        _backgroundRect = new ColorRect();
        AddChild(_backgroundRect);
    }

    private void UpdateColor()
    {
        _backgroundRect.Color = _backgroundColor;
    }
    private void SetAnchorsToFullScreen()
    {
        _backgroundRect.AnchorLeft = 0;
        _backgroundRect.AnchorRight = 1;
        _backgroundRect.AnchorTop = 0;
        _backgroundRect.AnchorBottom = 1;
    }
    private  void SetPosition()
    {
       SetPosition(new Vector2(ViewportSize.X * -0.5f, ViewportSize.Y * -0.5f));
       AnchorsPreset = (int)LayoutPreset.FullRect;
       AnchorLeft = 0;
       AnchorRight = 1;
       AnchorTop = 0;
       AnchorBottom = 1;
    }
    private Vector2 ViewportSize => GetViewportRect().Size;

    private void EditorOnReady()
    {
        if (_backgroundRect == null)
        {
            CreateBackgroundRect();
        }
        UpdateColor();
        SetAnchorsToFullScreen();


    }

    private void GameOnReady()
    {

    }

    public override void _EnterTree()
    {
        SetPosition();
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
