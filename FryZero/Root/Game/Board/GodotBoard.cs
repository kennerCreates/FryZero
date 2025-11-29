using System.Drawing;
using System.Numerics;
using Godot;
using Color = Godot.Color;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.Root.Game.Board;

[Tool]

[GlobalClass]

public partial class GodotBoard : Node2D
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
    private void UpdateLightSquareColor()
    {
        if (_lightSquares == null) return;
        _lightSquares.Modulate = _lightSquareColor;
    }

    [Export] public Texture2D LightSquareTexture
    {
        get => _lightSquareTexture;
        set
        {
            _lightSquareTexture = value;
            UpdateLightSquareTexture();
        }
    }
    private Texture2D _lightSquareTexture;
    private void UpdateLightSquareTexture()
    {
        if (_lightSquares == null) return;
        _lightSquares.Texture = _lightSquareTexture;
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

    private void UpdateDarkSquareColor()
    {
        if (_darkSquares == null) return;
        _darkSquares.Modulate = _darkSquareColor;
    }
    [Export] public Texture2D DarkSquareTexture
    {
        get => _darkSquareTexture;
        set
        {
            _darkSquareTexture = value;
            UpdateDarkSquareTexture();
        }
    }
    private Texture2D _darkSquareTexture;
    private void UpdateDarkSquareTexture()
    {
        if (_darkSquares == null) return;
        _darkSquares.Texture = _darkSquareTexture;
    }

    private Sprite2D _lightSquares;
    private Sprite2D _darkSquares;

    private void UpdateSquares()
    {
        if (_lightSquares == null)
        {
            CreateLightSquares();
        }
        if (_darkSquares == null)
        {
            CreateDarkSquares();
        }
        UpdateLightSquareColor();
        UpdateDarkSquareColor();
        UpdateLightSquareTexture();
        UpdateDarkSquareTexture();
        UpdateSquareScale();
    }

    private void CreateDarkSquares()
    {
        _darkSquares = new Sprite2D();
        _darkSquares.Texture = _darkSquareTexture;
        AddChild(_darkSquares);
    }

    private void CreateLightSquares()
    {
        _lightSquares = new Sprite2D();
        _lightSquares.Texture = _lightSquareTexture;
        AddChild(_lightSquares);
    }

    private int _squareSize;
    private void UpdateScreenSize()
    {
        var viewportSize = GetViewport().GetVisibleRect().Size;
        _squareSize = (int)viewportSize.Y / 9;
    }

    private void UpdateSquareScale()
    {
        UpdateScreenSize();
        _lightSquares.Scale = new Vector2(_squareSize, _squareSize);
        _darkSquares.Scale = new Vector2(_squareSize, _squareSize);
    }

    private void EditorOnReady()
    {
        GetViewport().Connect("size_changed", Callable.From(UpdateSquareScale));
        UpdateSquares();
    }

    private void GameOnReady()
    {

    }

    public override void _EnterTree()
    {
        CreateLightSquares();
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
