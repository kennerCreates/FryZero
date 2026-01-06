using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.GodotNodes.Game.Board;

[GlobalClass]

public partial class GodotBoard : BaseNode
{
    [Export] public int SquareSize { get; set; } = 64;
    [Export] public Texture2D LightSquareTexture { get; set; }

    [Export] public ThemeColor LightSquareColor { get; set; }

    [Export] public Texture2D DarkSquareTexture { get; set; }

    [Export] public ThemeColor DarkSquareColor { get; set; }

    public Sprite2D LightSquares;
    public Sprite2D DarkSquares;
    private void UpdateSquares()
    {
        UpdateLightSquares();
        UpdateDarkSquares();
        SetSquareScale();
    }
    private void UpdateDarkSquares()
    {
        DarkSquares ??= DarkSquareTexture.AddSprite2DAsChild(this);
        var modulated = ColorScheme.ModulateToThemeColor(DarkSquareColor);
        DarkSquares.Modulate = modulated;
    }

    private void UpdateLightSquares()
    {
        LightSquares ??= LightSquareTexture.AddSprite2DAsChild(this);
        LightSquares.Modulate = ColorScheme.ModulateToThemeColor(LightSquareColor);
    }

    private void SetSquareScale()
    {
        LightSquares.Scale = new Vector2(SquareSize, SquareSize);
        DarkSquares.Scale = new Vector2(SquareSize, SquareSize);
    }

    protected override void OnReady()
    {
        UpdateSquares();
        GetViewport().Connect("size_changed", Callable.From(SetSquareScale));
    }


}
