using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Pieces;
using FryZeroGodot.Config.Structs;
using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.gameplay;

public static class BoardLocations
{
    public static Vector2 GetClosestSquareLocation(PieceAttributes attributes, Vector2 location) =>
        new (GetClosestAxis(location.X, attributes), GetClosestAxis(location.Y, attributes));

    public static float GetClosestAxis(float locationAxis, PieceAttributes attributes) => 5f;

    public static bool DecimalXAxisGreaterThanFifty(PieceAttributes attributes, Vector2 location) =>
        GetDecimalVector(attributes, location).X > 0.5f;
    public static bool DecimalYAxisGreaterThanFifty(PieceAttributes attributes, Vector2 location) =>
        GetDecimalVector(attributes, location).Y > 0.5f;
    public static Vector2 GetDecimalVector(PieceAttributes attributes, Vector2 location) =>
        new(location.X % attributes.SquareSize, location.Y % attributes.SquareSize);
    
    public static Square GetSquareFromLocation(PieceAttributes attributes, Vector2 location) =>
        new(GetFileFromLocation(attributes, location.X), GetRankFromLocation(attributes, location.Y));
    public static Rank GetRankFromLocation(PieceAttributes attributes, float locationY) =>
        (Rank)((locationY + CenterBoardLocation(attributes))/attributes.SquareSize);
    
    public static File GetFileFromLocation(PieceAttributes attributes, float locationX) => 
        (File)((locationX + CenterBoardLocation(attributes))/attributes.SquareSize);

    public static Vector2 GetLocationFromSquare(PieceAttributes attributes, Square square) =>
        new(GetLocationFromFile(attributes, square ), GetLocationFromRank(attributes, square));

    public static int GetLocationFromRank(PieceAttributes attributes, Square square) =>
        (8 - (int)square.Rank) * attributes.SquareSize - CenterBoardLocation(attributes);

    public static int GetLocationFromFile(PieceAttributes attributes, Square square) => 
        ((int)square.File - 1) * attributes.SquareSize - CenterBoardLocation(attributes);
        
    public static int CenterBoardLocation(PieceAttributes attributes) => 
        attributes.SquareSize * 3 + attributes.SquareSize / 2;
    
}