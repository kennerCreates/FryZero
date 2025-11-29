using Godot;

namespace FryZeroGodot.Root.Game.Board;

[Tool]

[GlobalClass]

public partial class GodotSquare : ColorRect
{
    [Export]
    public Color LightSquareColor
    {
        get => _lightSquareColor;
        set
        {
            _lightSquareColor = value;
            UpdateSquareColor();
        }
    }
    private Color _lightSquareColor;

    [Export] public Color DarkSquareColor
    {
        get => _darkSquareColor;
        set
        {
            _darkSquareColor = value;
            UpdateSquareColor();
        }
    }
    private Color _darkSquareColor;

    [Export] public bool IsLightSquare
    {
        get => _isLightSquare;
        set
        {
            _isLightSquare = value;
            UpdateSquareColor();
        }
    }
    private bool _isLightSquare;

    private void EditorOnReady()
    {
        UpdateSquareColor();
    }

    private void GameOnReady()
    {

    }

    private void UpdateSquareColor()
    {
        Color = IsLightSquare ? LightSquareColor : DarkSquareColor;
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
