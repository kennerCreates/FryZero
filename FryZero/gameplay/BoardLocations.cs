using FryZeroGodot.Config;
using FryZeroGodot.Config.Structs;
using Godot;

namespace FryZeroGodot.gameplay;

public static class BoardLocations
{
    public static Vector2 LocationVector(this Square square, int squareSize) =>
        new(square.XCoordinate(squareSize), square.YCoordinate(squareSize));

    public static float XCoordinate(this Square square,  int squareSize)
    {
        var fileIndexFromLeftOfBoard = (int)square.File;
        float numberOfSquareWidthsToCenterOfSquareFromOrigin = fileIndexFromLeftOfBoard - (float)3.5;
        return squareSize * numberOfSquareWidthsToCenterOfSquareFromOrigin;
    }

    public static float YCoordinate(this Square square,  int squareSize)
    {
        var rankIndexFromBottomOfBoard = (int)square.Rank;
        float numberOfSquareWidthsToCenterOfSquareFromOrigin = (float)3.5 - rankIndexFromBottomOfBoard;
        return squareSize * numberOfSquareWidthsToCenterOfSquareFromOrigin;
    }
}
