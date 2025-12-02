using System;
using Godot;

namespace FryZeroGodot.Root.UI.Background;

[Tool]

[GlobalClass ]

public partial class GodotBackground : Node2D
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
        if (_backgroundRect != null) UpdateColor();
    }

    private void CreateBackgroundRect()
    {
        _backgroundRect = new ColorRect();
        var sizeX = Math.Max(ViewportSize.X, 288);
        var sizeY = Math.Max(ViewportSize.Y, 512);
        _backgroundRect.Size = new Vector2(sizeX, sizeY);
        AddChild(_backgroundRect);
    }

    private void UpdateColor()
    {
        _backgroundRect.Color = _backgroundColor;
    }

    private void UpdateScreenSize()
    {
        _backgroundRect.Size = ViewportSize;
        _backgroundRect.Position = _backgroundRect.Size / -2;
    }

    private Vector2 ViewportSize => GetViewport().GetVisibleRect().Size;

    private void EditorOnReady()
    {
        GetViewport().Connect("size_changed", Callable.From(UpdateScreenSize));
        UpdateScreenSize();
        UpdateColor();
    }

    private void GameOnReady()
    {

    }

    public override void _EnterTree()
    {
        CreateBackgroundRect();
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
