using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotNodes.NodeModels;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.GodotNodes.Gameplay.Board;

[GlobalClass]

public partial class GodotBoard : RootNode
{
    [Export] public int SquareSize { get; set; } = 64;
    [Export] public Texture2D LightSquareTexture { get; set; }

    [Export] public ThemeColor LightSquareColor { get; set; }

    [Export] public Texture2D DarkSquareTexture { get; set; }

    [Export] public ThemeColor DarkSquareColor { get; set; }

    private Sprite2D _lightSquares;
    private Sprite2D _darkSquares;
    private void UpdateSquares()
    {
        UpdateLightSquares();
        UpdateDarkSquares();
        SetSquareScale();
    }
    private void UpdateDarkSquares()
    {
        _darkSquares ??= DarkSquareTexture.AddSprite2DAsChild(this);
        _darkSquares.Modulate = GameTheme.GetThemeColor(DarkSquareColor);
    }

    private void UpdateLightSquares()
    {
        _lightSquares ??= LightSquareTexture.AddSprite2DAsChild(this);
        _lightSquares.Modulate = GameTheme.GetThemeColor(LightSquareColor);

    }

    private void SetSquareScale()
    {
        _lightSquares.Scale = new Vector2(SquareSize, SquareSize);
        _darkSquares.Scale = new Vector2(SquareSize, SquareSize);
    }

    protected override void OnReady()
    {
        UpdateSquares();
        GetViewport().Connect("size_changed", Callable.From(SetSquareScale));
    }


}
