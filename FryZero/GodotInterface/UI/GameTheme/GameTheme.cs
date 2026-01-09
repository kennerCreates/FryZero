using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.UI.ColorScheme;
using FryZeroGodot.Statics.UI.GameTheme;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.GameTheme;
public partial class GameTheme : Node2D
{
    public static GameTheme Instance { get; private set; }

    private GameThemeData _themeData = ResourceLoader.Load<GameThemeData>("res://assets/UI/CurrentColors.tres");

    private ShaderMaterial _themedMaterial = GD.Load<ShaderMaterial>("res://Shaders/HueShiftMaterial.tres");

    public override void _Ready()
    {
        Instance = this;
        _themedMaterial.UpdateAllThemeColors(_themeData);
    }

    // private static ShaderMaterial GetThemeMaterial( ShaderMaterial shaderMaterial)
    // {
    //     shaderMaterial ??= new ShaderMaterial
    //     {
    //         Shader = GD.Load<Shader>("res://Shaders/HueShift.gdshader")
    //     };
    //     return shaderMaterial;
    // }
    //
    // private static GameThemeData GetThemeData( GameThemeData themeData)
    // {
    //     themeData ??= ResourceLoader.Load<GameThemeData>("res://assets/UI/CurrentColors.tres");
    //     return themeData;
    // }

    private static Texture2D GetPieceTextureFromStyle(PieceStyle style) =>
        style switch
        {
            PieceStyle.Little => GD.Load<Texture2D>("res://assets/Pieces/LittlePieces.png"),
            PieceStyle.Tiny => GD.Load<Texture2D>("res://assets/Pieces/LittlePieces.png"),
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };

    public Color GetBackgroundColor() => _themeData.BackgroundColor.GetModulateColor();
    public Color GetPatternColor() => _themeData.BackgroundPatternColor.GetModulateColor();
    public Texture2D GetBackgroundTexture() => _themeData.BackgroundTexture;
    public int GetPatternScale() => _themeData.BackgroundPatternScale;
    public Color GetDarkSquareColor() => _themeData.DarkSquareColor.GetModulateColor();
    public Color GetLightSquareColor() => _themeData.LightSquareColor.GetModulateColor();
    public Texture2D GetLightSquareTexture() => _themeData.LightSquareTexture;
    public Texture2D GetDarkSquareTexture() => _themeData.DarkSquareTexture;
    public int GetPieceDelay() => _themeData.PieceMovementDelay;
    public Texture2D GetPieceAtlasTexture() => GetPieceTextureFromStyle(_themeData.PieceStyle);
    public int GetPieceSize() => _themeData.PieceSize;
    public int GetSquareSize() => _themeData.SquareSize;
    public ShaderMaterial GetThemeMaterial() => _themedMaterial;

}
