using FryZeroGodot.Config;
using FryZeroGodot.Config.Structs;
using Godot;

namespace FryZeroGodot.gameplay;

public static class BoardLocations
{
    public static Vector2 LocationVector(this Square square, BoardOptions boardOptions) =>
        new(square.XCoordinate(boardOptions), square.YCoordinate(boardOptions));

    public static float XCoordinate(this Square square, BoardOptions boardOptions)
    {
        var fileIndexFromLeftOfBoard = (int)square.File;
        float numberOfSquareWidthsToCenterOfSquareFromOrigin = fileIndexFromLeftOfBoard - (float)3.5;
        return boardOptions.SquareSize * numberOfSquareWidthsToCenterOfSquareFromOrigin;
    }

    public static float YCoordinate(this Square square, BoardOptions boardOptions)
    {
        var rankIndexFromBottomOfBoard = (int)square.Rank;
        float numberOfSquareWidthsToCenterOfSquareFromOrigin = (float)3.5 - rankIndexFromBottomOfBoard;
        return boardOptions.SquareSize * numberOfSquareWidthsToCenterOfSquareFromOrigin;
    }
}
