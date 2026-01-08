using FryZeroGodot.GodotNodes.NodeModels;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.GodotNodes.Gameplay.Board;

[GlobalClass]

public partial class GodotBoard : RootNode
{
    private static Sprite2D _lightSquares;
    private static Sprite2D _darkSquares;

    private static Sprite2D GetDarkSquares()
    {
        _darkSquares ??= new Sprite2D
        {
            Material = GameTheme.GetThemeMaterial()
        };
        _darkSquares.Texture = GameTheme.GetDarkSquareTexture();
        _darkSquares.SelfModulate = GameTheme.GetDarkSquareColor();
        var squareSize = GameTheme.GetSquareSize();
        _darkSquares.Scale = new Vector2(squareSize, squareSize);
        return _darkSquares;
    }

    private static Sprite2D GetLightSquares()
    {
        _lightSquares ??= new Sprite2D
        {
            Material = GameTheme.GetThemeMaterial()
        };
        _lightSquares.Texture = GameTheme.GetLightSquareTexture();
        _lightSquares.SelfModulate = GameTheme.GetLightSquareColor();
        var squareSize = GameTheme.GetSquareSize();
        _lightSquares.Scale = new Vector2(squareSize, squareSize);
        return _lightSquares;
    }

    private void SetSquareScale()
    {
        var squareSize = GameTheme.GetSquareSize();
        _lightSquares.Scale = new Vector2(squareSize, squareSize);
        _darkSquares.Scale = new Vector2(squareSize, squareSize);
    }

    protected override void OnReady()
    {
        AddChild(GetLightSquares());
        AddChild(GetDarkSquares());
        //GetViewport().Connect("size_changed", Callable.From(SetSquareScale));
    }


}
