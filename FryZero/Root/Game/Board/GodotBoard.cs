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
    private ColorRect _lightSquareRect;

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
    private ColorRect _darkSquareRect;

    private void UpdateDarkSquareColor()
    {
        _darkSquareRect.Color = _darkSquareColor;
    }


    private void EditorOnReady()
    {

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
