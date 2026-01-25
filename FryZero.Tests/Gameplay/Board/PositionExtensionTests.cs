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
    public void GetRankForStartingPawns_Returns_Two_For_White()
    {
        Assert.Equal(Rank.Two, PieceColor.White.ToRankForStartingPawns());
    }

    [Fact]
    public void GetRankForStartingPawns_Returns_Seven_For_Black()
    {
        Assert.Equal(Rank.Seven, PieceColor.Black.ToRankForStartingPawns());
    }

    [Fact]
    public void GetRankForStartingPawns_Throws_For_Invalid_Piece_Color()
    {
        var invalidPieceColor = (PieceColor)42;
        Assert.Throws<ArgumentOutOfRangeException>(() => invalidPieceColor.ToRankForStartingPawns());
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
        foreach(var file in Enum.GetValues<File>()) PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, file);

        var actual = position.SetupPawnsInStartingPosition(PieceColor.White, pieceFactory);
        var squaresOnRankTwo = actual.Squares.Where(square => square.Rank is Rank.Two);

        bool allSquaresOnRankTwoHaveWhitePawns = squaresOnRankTwo.All(square =>
            square.Piece is { Type: PieceType.Pawn, Color: PieceColor.White });
        Assert.True(allSquaresOnRankTwoHaveWhitePawns);
        pieceFactory.Received(8).CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, Arg.Any<File>());
    }

    [Fact]
    public void SetupPawnsInStartingPosition_Sets_Piece_To_Black_Pawn_For_All_Squares_On_Rank_Seven_When_PieceColor_Is_Black()
    {
        var position = PositionFactory.GetEmptyPosition();
        var pieceFactory = Substitute.For<IGodotPieceFactory>();
        foreach(var file in Enum.GetValues<File>()) PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, file);

        var actual = position.SetupPawnsInStartingPosition(PieceColor.Black, pieceFactory);
        var squaresOnRankSeven = actual.Squares.Where(square => square.Rank is Rank.Seven);

        bool allSquaresOnRankSevenHaveBlackPawns = squaresOnRankSeven.All(square =>
            square.Piece is { Type: PieceType.Pawn, Color: PieceColor.Black });
        Assert.True(allSquaresOnRankSevenHaveBlackPawns);
        pieceFactory.Received(8).CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, Arg.Any<File>());
    }

    [Fact]
    public void SetupBackRankInStartingPosition_Sets_Pieces_For_All_Squares_Correctly_When_PieceColor_Is_White()
    {
        var position = PositionFactory.GetEmptyPosition();
        var pieceFactory = Substitute.For<IGodotPieceFactory>();
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.White, Rank.One, File.A );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.White, Rank.One, File.B );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.White, Rank.One, File.C );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Queen, PieceColor.White, Rank.One, File.D );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.King, PieceColor.White, Rank.One, File.E );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.White, Rank.One, File.F );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.White, Rank.One, File.G );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.White, Rank.One, File.H );
        var expected = PositionFactory.GetWhiteBackRankStartingSetup();

        var actual = position.SetupBackRankInStartingPosition(PieceColor.White, pieceFactory);

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void SetupBackRankInStartingPosition_Sets_Pieces_For_All_Squares_Correctly_When_PieceColor_Is_Black()
    {
        var position = PositionFactory.GetEmptyPosition();
        var pieceFactory = Substitute.For<IGodotPieceFactory>();
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.Black, Rank.Eight, File.A );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.Black, Rank.Eight, File.B );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.Black, Rank.Eight, File.C );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Queen, PieceColor.Black, Rank.Eight, File.D );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.King, PieceColor.Black, Rank.Eight, File.E );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.Black, Rank.Eight, File.F );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.Black, Rank.Eight, File.G );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.Black, Rank.Eight, File.H );
        var expected = PositionFactory.GetBlackBackRankStartingSetup();

        var actual = position.SetupBackRankInStartingPosition(PieceColor.Black, pieceFactory);

        Assert.Equivalent(expected, actual);
    }

    [Fact]
    public void SetupPiecesStartingPosition_Sets_Pieces_For_All_Squares_Correctly()
    {
        var position = PositionFactory.GetEmptyPosition();
        var pieceFactory = Substitute.For<IGodotPieceFactory>();
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.White, Rank.One, File.A );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.White, Rank.One, File.B );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.White, Rank.One, File.C );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Queen, PieceColor.White, Rank.One, File.D );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.King, PieceColor.White, Rank.One, File.E );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.White, Rank.One, File.F );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.White, Rank.One, File.G );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.White, Rank.One, File.H );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.A );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.B );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.C );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.D );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.E );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.F );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.G );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.White, Rank.Two, File.H );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.A );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.B );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.C );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.D );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.E );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.F );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.G );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Pawn, PieceColor.Black, Rank.Seven, File.H );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.Black, Rank.Eight, File.A );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.Black, Rank.Eight, File.B );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.Black, Rank.Eight, File.C );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Queen, PieceColor.Black, Rank.Eight, File.D );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.King, PieceColor.Black, Rank.Eight, File.E );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Bishop, PieceColor.Black, Rank.Eight, File.F );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Knight, PieceColor.Black, Rank.Eight, File.G );
        PieceFactoryTestHelpers.SetupCreateOnePiece(pieceFactory, PieceType.Rook, PieceColor.Black, Rank.Eight, File.H );

        var expected = PositionFactory.GetStartingPosition();

        var actual = position.SetupPiecesInStartingChessPosition(pieceFactory);

        Assert.Equivalent(expected, actual);
    }

    [Theory]
    [InlineData(Rank.One, File.A)]
    [InlineData(Rank.Two, File.D)]
    [InlineData(Rank.Three, File.E)]
    [InlineData(Rank.Four, File.H)]
    [InlineData(Rank.Five, File.B)]
    [InlineData(Rank.Six, File.F)]
    [InlineData(Rank.Seven, File.G)]
    [InlineData(Rank.Eight, File.C)]
    public void RemovePieceFromBoard_Correctly(Rank rank, File file)
    {
        var position = PositionFactory.GetStartingPosition();
        var piece = Substitute.For<IGodotPiece>();
        piece.Rank = rank;
        piece.File = file;

        var expected = PositionFactory.GetStartingPositionExceptOnePiece(rank, file);

        var actual = position.RemovePieceFromBoard(piece);

        Assert.Equivalent(expected, actual);
    }

    [Theory]
    [InlineData(Rank.One, File.A, PieceColor.White, PieceType.Pawn)]
    [InlineData(Rank.Two, File.D, PieceColor.Black, PieceType.King)]
    [InlineData(Rank.Three, File.E, PieceColor.White, PieceType.Queen)]
    [InlineData(Rank.Four, File.H, PieceColor.Black, PieceType.Rook)]
    [InlineData(Rank.Five, File.B, PieceColor.White, PieceType.Bishop)]
    [InlineData(Rank.Six, File.F, PieceColor.Black, PieceType.Knight)]
    [InlineData(Rank.Seven, File.G, PieceColor.White, PieceType.Queen)]
    [InlineData(Rank.Eight, File.C, PieceColor.Black, PieceType.Pawn)]
    public void UpdateChessPosition_Correctly(Rank rank, File file, PieceColor pieceColor, PieceType pieceType)
    {
        var position = PositionFactory.GetEmptyPosition();
        var piece = Substitute.For<IGodotPiece>();
        piece.Rank = rank;
        piece.File = file;
        piece.Color = pieceColor;
        piece.Type = pieceType;

        var expected = PositionFactory.GetEmptyPositionExceptOnePiece(file, rank, pieceColor, pieceType);

        var actual = position.UpdateChessPosition(piece);

        Assert.Equivalent(expected, actual);
    }

}
