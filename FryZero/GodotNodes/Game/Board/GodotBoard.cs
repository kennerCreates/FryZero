using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.GodotNodes.Game.Board;

[GlobalClass]

public partial class GodotBoard : Node2D
{
    [Export] public int SquareSize { get; set; } = 64;
    [Export] public Texture2D LightSquareTexture { get; set; }

    [Export] public ThemeColor LightSquareColor { get; set; }

    [Export] public Texture2D DarkSquareTexture { get; set; }

    [Export] public ThemeColor DarkSquareColor { get; set; }

    public Sprite2D LightSquares;
    public Sprite2D DarkSquares;
    public GodotColorScheme ColorScheme;
    private void UpdateSquares()
    {
        UpdateLightSquares();
        UpdateDarkSquares();
        SetSquareScale();
    }
    private void UpdateDarkSquares()
    {
        DarkSquares ??= CreateDarkSquares();
        var modulated = ColorScheme.ModulateToThemeColor(DarkSquareColor);
        DarkSquares.Modulate = modulated;
    }

    private Sprite2D CreateDarkSquares()
    {
        var darkSquares = new Sprite2D();
        AddChild(darkSquares);
        darkSquares.Texture = DarkSquareTexture;
        return darkSquares;
    }
    private void UpdateLightSquares()
    {
        LightSquares ??= CreateLightSquares();
        LightSquares.Modulate = ColorScheme.ModulateToThemeColor(LightSquareColor);
    }
    private Sprite2D CreateLightSquares()
    {
        var lightSquares = new Sprite2D();
        AddChild(lightSquares);
        lightSquares.Texture = LightSquareTexture;
        return lightSquares;
    }
    private void SetSquareScale()
    {
        LightSquares.Scale = new Vector2(SquareSize, SquareSize);
        DarkSquares.Scale = new Vector2(SquareSize, SquareSize);
    }

    public override void _EnterTree()
    {
        ColorScheme = GetParent<GodotColorScheme>();
        if (ColorScheme.IsInitialized)
        {
            OnColorSchemeReady();
        }
        else
        {
            ColorScheme.ColorSchemeInitialized += OnColorSchemeReady;
        }
    }

    private void OnColorSchemeReady()
    {
        UpdateSquares();
        GetViewport().Connect("size_changed", Callable.From(SetSquareScale));
    }


}
