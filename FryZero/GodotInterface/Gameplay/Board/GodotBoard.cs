using FryZeroGodot.GodotInterface.UI.GameTheme;
using Godot;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.GodotInterface.Gameplay.Board;

[GlobalClass]

public partial class GodotBoard : Node2D
{
    private Sprite2D _lightSquares;
    private Sprite2D _darkSquares;

    private Sprite2D GetDarkSquares()
    {
        _darkSquares ??= new Sprite2D
        {
            Material = GameTheme.Instance.GetThemeMaterial()
        };
        _darkSquares.Texture = GameTheme.Instance.GetDarkSquareTexture();
        _darkSquares.SelfModulate = GameTheme.Instance.GetDarkSquareColor();
        var squareSize = GameTheme.Instance.GetSquareSize();
        _darkSquares.Scale = new Vector2(squareSize, squareSize);
        return _darkSquares;
    }

    private Sprite2D GetLightSquares()
    {
        _lightSquares ??= new Sprite2D
        {
            Material = GameTheme.Instance.GetThemeMaterial()
        };
        _lightSquares.Texture = GameTheme.Instance.GetLightSquareTexture();
        _lightSquares.SelfModulate = GameTheme.Instance.GetLightSquareColor();
        var squareSize = GameTheme.Instance.GetSquareSize();
        _lightSquares.Scale = new Vector2(squareSize, squareSize);
        return _lightSquares;
    }

    private void SetSquareScale()
    {
        var squareSize = GameTheme.Instance.GetSquareSize();
        _lightSquares.Scale = new Vector2(squareSize, squareSize);
        _darkSquares.Scale = new Vector2(squareSize, squareSize);
    }

    public override void _Ready()
    {
        AddChild(GetLightSquares());
        AddChild(GetDarkSquares());
        //GetViewport().Connect("size_changed", Callable.From(SetSquareScale));
    }


}
