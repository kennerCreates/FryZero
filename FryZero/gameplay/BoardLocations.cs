using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.gameplay;

public static class BoardLocations
{

    public static File GetFileFromLocation(int location, PieceOptions options) => 
        (File)(location + CenterBoardLocation(options));

    public static Vector2 GetLocationFromSquare(PieceOptions options) =>
        new(GetLocationFromFile(options), GetLocationFromRank(options));

    private static int GetLocationFromRank(PieceOptions options) => 
        ((int)options.PieceRank * options.SquareSize - CenterBoardLocation(options)) * -1;

    public static int GetLocationFromFile(PieceOptions options) => 
        (int)options.PieceFile * options.SquareSize - CenterBoardLocation(options);
        
    public static int CenterBoardLocation(PieceOptions options) => 
        options.SquareSize * 3 + options.SquareSize / 2;
}