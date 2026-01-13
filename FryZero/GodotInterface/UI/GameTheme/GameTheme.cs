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

    public Color GetBackgroundColor() => _themeData.BackgroundColor.GetModulateColor();
    public Color GetPatternColor() => _themeData.BackgroundPatternColor.GetModulateColor();
    public Texture2D GetBackgroundTexture() => _themeData.BackgroundTexture;
    public int GetPatternScale() => _themeData.BackgroundPatternScale;
    public Color GetDarkSquareColor() => _themeData.DarkSquareColor.GetModulateColor();
    public Color GetLightSquareColor() => _themeData.LightSquareColor.GetModulateColor();
    public Texture2D GetLightSquareTexture() => _themeData.LightSquareTexture;
    public Texture2D GetDarkSquareTexture() => _themeData.DarkSquareTexture;
    public int GetPieceDelay() => _themeData.PieceMovementDelay;
    public Texture2D GetPieceAtlasTexture() => _themeData.PieceStyle.GetPieceTextureFromStyle();
    public int GetPieceSize() => _themeData.PieceSize;
    public int GetSquareSize() => _themeData.SquareSize;
    public ShaderMaterial GetThemeMaterial() => _themedMaterial;

}
