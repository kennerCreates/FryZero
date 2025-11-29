using System.Drawing;
using System.Numerics;
using Godot;
using Color = Godot.Color;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.Root.Game.Board;

[Tool]

[GlobalClass]

public partial class GodotBoard : Control
{
    [Export] public Color LightSquareColor
    {
        get => _lightSquareColor;
        set
        {
            _lightSquareColor = value;
            UpdateLightSquareColor();
        }
    }
    private Color _lightSquareColor = Colors.White;
    private ColorRect _lightSquareRect = new();

    private void UpdateLightSquareColor()
    {
        _lightSquareRect.Color = _lightSquareColor;
    }

    [Export] public Color DarkSquareColor
    {
        get => _darkSquareColor;
        set
        {
            _darkSquareColor = value;
            UpdateDarkSquareColor();
        }
    }
    private Color _darkSquareColor = Colors.Black;
    private ColorRect _darkSquareRect = new();

    private void UpdateDarkSquareColor()
    {
        _darkSquareRect.Color = _darkSquareColor;
    }

    private int _squareSize;
    private void UpdateScreenSize()
    {
        Vector2 viewportSize = GetViewportRect().Size;
        _squareSize = (int)viewportSize.Y / 9;
        Size = new Vector2(_squareSize * 8, _squareSize * 8);
    }

    private void EditorOnReady()
    {
        UpdateScreenSize();
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
