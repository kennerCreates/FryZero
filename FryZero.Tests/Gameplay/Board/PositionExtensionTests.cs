using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using FryZeroGodot.Statics.Gameplay.Board;
using NSubstitute;
using File = FryZeroGodot.Config.Enums.File;

namespace FryZero.Tests.Gameplay.Board;

public class PositionExtensionTests
{
    [Fact]
    public void SetupPositionWithEmptyBoardTest()
    {
        var position = new ChessPosition();
        var expected = PositionFactory.GetEmptyPosition();
        var actual = position.SetupPositionWithEmptyBoard();
        Assert.Equal(expected.Squares.Count, actual.Squares.Count);
        Assert.Equivalent(expected.Squares, actual.Squares);
    }

    [Fact]
    public void SetPieceInPosition_Returns_A_Square_With_The_Correct_File_Rank_And_Piece()
    {
        var position = PositionFactory.GetEmptyPosition();
        var square = position.GetSquare(File.D, Rank.One);
        var piece = Substitute.For<IGodotPiece>();
        piece.File = square.File;
        piece.Rank = square.Rank;
        piece.Type = PieceType.Queen;
        piece.Color = PieceColor.White;
        var expected = square with { Piece = piece };

        var actual = position.SetPieceInPosition(piece);

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void GetSquare_Returns_Square_With_Correct_File_And_Rank()
    {
        var position = PositionFactory.GetStartingPosition();
        var file = File.D;
        var rank = Rank.One;
        var expected = position.Squares.Single(s => s.File == file && s.Rank == rank);

        var actual = position.GetSquare(file, rank);

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void GetSquare_Returns_Square_With_Correct_Piece()
    {
        var position = PositionFactory.GetStartingPosition();
        var file = File.D;
        var rank = Rank.One;
        var expectedPiece = Substitute.For<IGodotPiece>();
        expectedPiece.File = file;
        expectedPiece.Rank = rank;
        expectedPiece.Type = PieceType.Queen;
        expectedPiece.Color = PieceColor.White;

        var actual = position.GetSquare(file, rank);

        Assert.Equivalent(expectedPiece, actual.Piece);
    }
}
