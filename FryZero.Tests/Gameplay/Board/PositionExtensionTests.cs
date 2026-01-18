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

    [Fact]
    public void SetupPawnsInStartingPosition_Sets_Piece_To_White_Pawn_For_All_Squares_On_Rank_Two_When_PieceColor_Is_White()
    {
        var position = PositionFactory.GetEmptyPosition();
        var pieceFactory = Substitute.For<IGodotPieceFactory>();
        var piece = Substitute.For<IGodotPiece>();
        piece.Type = PieceType.Pawn;
        piece.Color = PieceColor.White;
        piece.Rank = Rank.Two;
        pieceFactory.CreateOnePiece(PieceType.Pawn, PieceColor.White, Arg.Any<Rank>(), File.A).Returns(piece);

        var actual = position.SetupPawnsInStartingPosition(PieceColor.White, pieceFactory);
        var squaresOnRankTwo = actual.Squares.Where(square => square.Rank is Rank.Two);

        bool allSquaresOnRankTwoHaveWhitePawns = squaresOnRankTwo.All(square =>
            square.Piece is { Type: PieceType.Pawn, Color: PieceColor.White });
        Assert.True(allSquaresOnRankTwoHaveWhitePawns);
    }
}
