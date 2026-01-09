using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Enums.Visuals;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.ColorScheme;

[GlobalClass]
public partial class GameThemeData : Resource
{
    [ExportCategory("Colors")]
    [Export] public Color LightColor { get; set; } = Colors.White;
    [Export] public Color LightHighlightColor { get; set; } = Colors.Yellow;
    [Export] public Color LightShadowColor { get; set; } = Colors.Magenta;
    [Export] public Color LightAccentColor { get; set; }= Colors.Red;
    [Export] public Color DarkColor { get; set; }= Colors.Black;
    [Export] public Color DarkHighlightColor { get; set; }= Colors.Cyan;
    [Export] public Color DarkShadowColor { get; set; }= Colors.Blue;
    [Export] public Color DarkAccentColor { get; set; }= Colors.Green;


    [ExportCategory("Background")]
    [Export] public Texture2D BackgroundTexture { get; set; }
    [Export] public ThemeColor BackgroundColor { get; set; }
    [Export] public ThemeColor BackgroundPatternColor { get; set; }
    [Export] public int BackgroundPatternScale { get; set; } = 1;


    [ExportCategory("Board")]
    [Export] public int SquareSize { get; set; } = 64;
    [Export] public Texture2D LightSquareTexture { get; set; }
    [Export] public ThemeColor LightSquareColor { get; set; }
    [Export] public Texture2D DarkSquareTexture { get; set; }
    [Export] public ThemeColor DarkSquareColor { get; set; }

    [ExportCategory("Pieces")]
    [Export] public int PieceMovementDelay { get; set; } = 10;
    [Export] public PieceStyle PieceStyle { get; set; }
    [Export] public Texture2D PieceAtlasTexture { get; set; }
    [Export] public int PieceSize{ get; set; } = 32;

}
