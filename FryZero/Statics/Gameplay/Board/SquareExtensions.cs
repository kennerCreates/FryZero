using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using Godot;

namespace FryZeroGodot.Statics.Gameplay.Board;

public static class SquareExtensions
{
    public static Square GetSquare(this Vector2 position, int squareSize) =>
    new(position.X.GetFile(squareSize), position.Y.GetRank(squareSize));
    public static File GetFile(this float position, int squareSize)
    {
        var scaledBoardSquare = position / squareSize;
        var centeredBoardSquare = MathF.Truncate(scaledBoardSquare + 4f);
        return (File)centeredBoardSquare;
    }

    public static Rank GetRank(this float position, int squareSize)
    {
        var scaledBoardSquare = position / squareSize;
        var centeredBoardSquare = MathF.Truncate(4 - scaledBoardSquare);
        return (Rank)centeredBoardSquare;
    }

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
    private static bool IsValidFile(this File file) =>
        file switch
        {
            File.A => true,
            File.B => true,
            File.C => true,
            File.D => true,
            File.E => true,
            File.F => true,
            File.G => true,
            File.H => true,
            _ => false
        };

    private static bool IsValidRank(this Rank rank) =>
        rank switch
        {
            Rank.One => true,
            Rank.Two => true,
            Rank.Three => true,
            Rank.Four => true,
            Rank.Five => true,
            Rank.Six => true,
            Rank.Seven => true,
            Rank.Eight => true,
            _ => false
        };

    public static bool IsValidSquare(this Square square) =>
        square.File.IsValidFile() && square.Rank.IsValidRank();
}
