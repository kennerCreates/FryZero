using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using NSubstitute;
using ChessPosition = FryZeroGodot.Config.Records.ChessPosition;
using File = FryZeroGodot.Config.Enums.File;
using Rank = FryZeroGodot.Config.Enums.Rank;
using Square = FryZeroGodot.Config.Records.Square;


namespace FryZero.Tests.Gameplay.Board;

public static class PositionFactory
{
    public static IGodotPiece GetMockGodotPiece(File file, Rank rank, PieceType type, PieceColor color)
    {
        var piece = Substitute.For<IGodotPiece>();
        piece.File = file;
        piece.Rank = rank;
        piece.Type = type;
        piece.Color = color;
        return piece;
    }

    public static Square GetSquareWithMockPiece(File file, Rank rank, PieceType? type = null, PieceColor? color = null)
    {
        if (type is null || color is null)
        {
            return new Square(file, rank, Piece: null);
        }

        return new Square(file, rank, GetMockGodotPiece(file, rank, type.Value, color.Value));
    }

    public static ChessPosition GetStartingPosition()
    {
        var position = new ChessPosition
        {
            Squares = [
                GetSquareWithMockPiece(File.A, Rank.One, PieceType.Rook, PieceColor.White),
                GetSquareWithMockPiece(File.B, Rank.One, PieceType.Knight, PieceColor.White),
                GetSquareWithMockPiece(File.C, Rank.One, PieceType.Bishop, PieceColor.White),
                GetSquareWithMockPiece(File.D, Rank.One, PieceType.Queen, PieceColor.White),
                GetSquareWithMockPiece(File.E, Rank.One, PieceType.King, PieceColor.White),
                GetSquareWithMockPiece(File.F, Rank.One, PieceType.Bishop, PieceColor.White),
                GetSquareWithMockPiece(File.G, Rank.One, PieceType.Knight, PieceColor.White),
                GetSquareWithMockPiece(File.H, Rank.One, PieceType.Rook, PieceColor.White),
                GetSquareWithMockPiece(File.A, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.B, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.C, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.D, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.E, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.F, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.G, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.H, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.A, Rank.Three),
                GetSquareWithMockPiece(File.B, Rank.Three),
                GetSquareWithMockPiece(File.C, Rank.Three),
                GetSquareWithMockPiece(File.D, Rank.Three),
                GetSquareWithMockPiece(File.E, Rank.Three),
                GetSquareWithMockPiece(File.F, Rank.Three),
                GetSquareWithMockPiece(File.G, Rank.Three),
                GetSquareWithMockPiece(File.H, Rank.Three),
                GetSquareWithMockPiece(File.A, Rank.Four),
                GetSquareWithMockPiece(File.B, Rank.Four),
                GetSquareWithMockPiece(File.C, Rank.Four),
                GetSquareWithMockPiece(File.D, Rank.Four),
                GetSquareWithMockPiece(File.E, Rank.Four),
                GetSquareWithMockPiece(File.F, Rank.Four),
                GetSquareWithMockPiece(File.G, Rank.Four),
                GetSquareWithMockPiece(File.H, Rank.Four),
                GetSquareWithMockPiece(File.A, Rank.Five),
                GetSquareWithMockPiece(File.B, Rank.Five),
                GetSquareWithMockPiece(File.C, Rank.Five),
                GetSquareWithMockPiece(File.D, Rank.Five),
                GetSquareWithMockPiece(File.E, Rank.Five),
                GetSquareWithMockPiece(File.F, Rank.Five),
                GetSquareWithMockPiece(File.G, Rank.Five),
                GetSquareWithMockPiece(File.H, Rank.Five),
                GetSquareWithMockPiece(File.A, Rank.Six),
                GetSquareWithMockPiece(File.B, Rank.Six),
                GetSquareWithMockPiece(File.C, Rank.Six),
                GetSquareWithMockPiece(File.D, Rank.Six),
                GetSquareWithMockPiece(File.E, Rank.Six),
                GetSquareWithMockPiece(File.F, Rank.Six),
                GetSquareWithMockPiece(File.G, Rank.Six),
                GetSquareWithMockPiece(File.H, Rank.Six),
                GetSquareWithMockPiece(File.A, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.B, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.C, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.D, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.E, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.F, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.G, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.H, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.A, Rank.Eight, PieceType.Rook, PieceColor.Black),
                GetSquareWithMockPiece(File.B, Rank.Eight, PieceType.Knight, PieceColor.Black),
                GetSquareWithMockPiece(File.C, Rank.Eight, PieceType.Bishop, PieceColor.Black),
                GetSquareWithMockPiece(File.D, Rank.Eight, PieceType.Queen, PieceColor.Black),
                GetSquareWithMockPiece(File.E, Rank.Eight, PieceType.King, PieceColor.Black),
                GetSquareWithMockPiece(File.F, Rank.Eight, PieceType.Bishop, PieceColor.Black),
                GetSquareWithMockPiece(File.G, Rank.Eight, PieceType.Knight, PieceColor.Black),
                GetSquareWithMockPiece(File.H, Rank.Eight, PieceType.Rook, PieceColor.Black)
            ]
        };
        return position;
    }

    public static ChessPosition GetWhiteBackRankStartingSetup()
    {
        var position = new ChessPosition
        {
            Squares = [
                GetSquareWithMockPiece(File.A, Rank.One, PieceType.Rook, PieceColor.White),
                GetSquareWithMockPiece(File.B, Rank.One, PieceType.Knight, PieceColor.White),
                GetSquareWithMockPiece(File.C, Rank.One, PieceType.Bishop, PieceColor.White),
                GetSquareWithMockPiece(File.D, Rank.One, PieceType.Queen, PieceColor.White),
                GetSquareWithMockPiece(File.E, Rank.One, PieceType.King, PieceColor.White),
                GetSquareWithMockPiece(File.F, Rank.One, PieceType.Bishop, PieceColor.White),
                GetSquareWithMockPiece(File.G, Rank.One, PieceType.Knight, PieceColor.White),
                GetSquareWithMockPiece(File.H, Rank.One, PieceType.Rook, PieceColor.White),
                GetSquareWithMockPiece(File.A, Rank.Two),
                GetSquareWithMockPiece(File.B, Rank.Two),
                GetSquareWithMockPiece(File.C, Rank.Two),
                GetSquareWithMockPiece(File.D, Rank.Two),
                GetSquareWithMockPiece(File.E, Rank.Two),
                GetSquareWithMockPiece(File.F, Rank.Two),
                GetSquareWithMockPiece(File.G, Rank.Two),
                GetSquareWithMockPiece(File.H, Rank.Two),
                GetSquareWithMockPiece(File.A, Rank.Three),
                GetSquareWithMockPiece(File.B, Rank.Three),
                GetSquareWithMockPiece(File.C, Rank.Three),
                GetSquareWithMockPiece(File.D, Rank.Three),
                GetSquareWithMockPiece(File.E, Rank.Three),
                GetSquareWithMockPiece(File.F, Rank.Three),
                GetSquareWithMockPiece(File.G, Rank.Three),
                GetSquareWithMockPiece(File.H, Rank.Three),
                GetSquareWithMockPiece(File.A, Rank.Four),
                GetSquareWithMockPiece(File.B, Rank.Four),
                GetSquareWithMockPiece(File.C, Rank.Four),
                GetSquareWithMockPiece(File.D, Rank.Four),
                GetSquareWithMockPiece(File.E, Rank.Four),
                GetSquareWithMockPiece(File.F, Rank.Four),
                GetSquareWithMockPiece(File.G, Rank.Four),
                GetSquareWithMockPiece(File.H, Rank.Four),
                GetSquareWithMockPiece(File.A, Rank.Five),
                GetSquareWithMockPiece(File.B, Rank.Five),
                GetSquareWithMockPiece(File.C, Rank.Five),
                GetSquareWithMockPiece(File.D, Rank.Five),
                GetSquareWithMockPiece(File.E, Rank.Five),
                GetSquareWithMockPiece(File.F, Rank.Five),
                GetSquareWithMockPiece(File.G, Rank.Five),
                GetSquareWithMockPiece(File.H, Rank.Five),
                GetSquareWithMockPiece(File.A, Rank.Six),
                GetSquareWithMockPiece(File.B, Rank.Six),
                GetSquareWithMockPiece(File.C, Rank.Six),
                GetSquareWithMockPiece(File.D, Rank.Six),
                GetSquareWithMockPiece(File.E, Rank.Six),
                GetSquareWithMockPiece(File.F, Rank.Six),
                GetSquareWithMockPiece(File.G, Rank.Six),
                GetSquareWithMockPiece(File.H, Rank.Six),
                GetSquareWithMockPiece(File.A, Rank.Seven),
                GetSquareWithMockPiece(File.B, Rank.Seven),
                GetSquareWithMockPiece(File.C, Rank.Seven),
                GetSquareWithMockPiece(File.D, Rank.Seven),
                GetSquareWithMockPiece(File.E, Rank.Seven),
                GetSquareWithMockPiece(File.F, Rank.Seven),
                GetSquareWithMockPiece(File.G, Rank.Seven),
                GetSquareWithMockPiece(File.H, Rank.Seven),
                GetSquareWithMockPiece(File.A, Rank.Eight),
                GetSquareWithMockPiece(File.B, Rank.Eight),
                GetSquareWithMockPiece(File.C, Rank.Eight),
                GetSquareWithMockPiece(File.D, Rank.Eight),
                GetSquareWithMockPiece(File.E, Rank.Eight),
                GetSquareWithMockPiece(File.F, Rank.Eight),
                GetSquareWithMockPiece(File.G, Rank.Eight),
                GetSquareWithMockPiece(File.H, Rank.Eight)
            ]
        };
        return position;
    }

     public static ChessPosition GetBlackBackRankStartingSetup()
    {
        var position = new ChessPosition
        {
            Squares = [
                GetSquareWithMockPiece(File.A, Rank.One),
                GetSquareWithMockPiece(File.B, Rank.One),
                GetSquareWithMockPiece(File.C, Rank.One),
                GetSquareWithMockPiece(File.D, Rank.One),
                GetSquareWithMockPiece(File.E, Rank.One),
                GetSquareWithMockPiece(File.F, Rank.One),
                GetSquareWithMockPiece(File.G, Rank.One),
                GetSquareWithMockPiece(File.H, Rank.One),
                GetSquareWithMockPiece(File.A, Rank.Two),
                GetSquareWithMockPiece(File.B, Rank.Two),
                GetSquareWithMockPiece(File.C, Rank.Two),
                GetSquareWithMockPiece(File.D, Rank.Two),
                GetSquareWithMockPiece(File.E, Rank.Two),
                GetSquareWithMockPiece(File.F, Rank.Two),
                GetSquareWithMockPiece(File.G, Rank.Two),
                GetSquareWithMockPiece(File.H, Rank.Two),
                GetSquareWithMockPiece(File.A, Rank.Three),
                GetSquareWithMockPiece(File.B, Rank.Three),
                GetSquareWithMockPiece(File.C, Rank.Three),
                GetSquareWithMockPiece(File.D, Rank.Three),
                GetSquareWithMockPiece(File.E, Rank.Three),
                GetSquareWithMockPiece(File.F, Rank.Three),
                GetSquareWithMockPiece(File.G, Rank.Three),
                GetSquareWithMockPiece(File.H, Rank.Three),
                GetSquareWithMockPiece(File.A, Rank.Four),
                GetSquareWithMockPiece(File.B, Rank.Four),
                GetSquareWithMockPiece(File.C, Rank.Four),
                GetSquareWithMockPiece(File.D, Rank.Four),
                GetSquareWithMockPiece(File.E, Rank.Four),
                GetSquareWithMockPiece(File.F, Rank.Four),
                GetSquareWithMockPiece(File.G, Rank.Four),
                GetSquareWithMockPiece(File.H, Rank.Four),
                GetSquareWithMockPiece(File.A, Rank.Five),
                GetSquareWithMockPiece(File.B, Rank.Five),
                GetSquareWithMockPiece(File.C, Rank.Five),
                GetSquareWithMockPiece(File.D, Rank.Five),
                GetSquareWithMockPiece(File.E, Rank.Five),
                GetSquareWithMockPiece(File.F, Rank.Five),
                GetSquareWithMockPiece(File.G, Rank.Five),
                GetSquareWithMockPiece(File.H, Rank.Five),
                GetSquareWithMockPiece(File.A, Rank.Six),
                GetSquareWithMockPiece(File.B, Rank.Six),
                GetSquareWithMockPiece(File.C, Rank.Six),
                GetSquareWithMockPiece(File.D, Rank.Six),
                GetSquareWithMockPiece(File.E, Rank.Six),
                GetSquareWithMockPiece(File.F, Rank.Six),
                GetSquareWithMockPiece(File.G, Rank.Six),
                GetSquareWithMockPiece(File.H, Rank.Six),
                GetSquareWithMockPiece(File.A, Rank.Seven),
                GetSquareWithMockPiece(File.B, Rank.Seven),
                GetSquareWithMockPiece(File.C, Rank.Seven),
                GetSquareWithMockPiece(File.D, Rank.Seven),
                GetSquareWithMockPiece(File.E, Rank.Seven),
                GetSquareWithMockPiece(File.F, Rank.Seven),
                GetSquareWithMockPiece(File.G, Rank.Seven),
                GetSquareWithMockPiece(File.H, Rank.Seven),
                GetSquareWithMockPiece(File.A, Rank.Eight, PieceType.Rook, PieceColor.Black),
                GetSquareWithMockPiece(File.B, Rank.Eight, PieceType.Knight, PieceColor.Black),
                GetSquareWithMockPiece(File.C, Rank.Eight, PieceType.Bishop, PieceColor.Black),
                GetSquareWithMockPiece(File.D, Rank.Eight, PieceType.Queen, PieceColor.Black),
                GetSquareWithMockPiece(File.E, Rank.Eight, PieceType.King, PieceColor.Black),
                GetSquareWithMockPiece(File.F, Rank.Eight, PieceType.Bishop, PieceColor.Black),
                GetSquareWithMockPiece(File.G, Rank.Eight, PieceType.Knight, PieceColor.Black),
                GetSquareWithMockPiece(File.H, Rank.Eight, PieceType.Rook, PieceColor.Black)
            ]
        };
        return position;
    }
    public static ChessPosition GetEmptyPosition()
    {
        var position = new ChessPosition
        {
            Squares = [
                GetSquareWithMockPiece(File.A, Rank.One),
                GetSquareWithMockPiece(File.B, Rank.One),
                GetSquareWithMockPiece(File.C, Rank.One),
                GetSquareWithMockPiece(File.D, Rank.One),
                GetSquareWithMockPiece(File.E, Rank.One),
                GetSquareWithMockPiece(File.F, Rank.One),
                GetSquareWithMockPiece(File.G, Rank.One),
                GetSquareWithMockPiece(File.H, Rank.One),
                GetSquareWithMockPiece(File.A, Rank.Two),
                GetSquareWithMockPiece(File.B, Rank.Two),
                GetSquareWithMockPiece(File.C, Rank.Two),
                GetSquareWithMockPiece(File.D, Rank.Two),
                GetSquareWithMockPiece(File.E, Rank.Two),
                GetSquareWithMockPiece(File.F, Rank.Two),
                GetSquareWithMockPiece(File.G, Rank.Two),
                GetSquareWithMockPiece(File.H, Rank.Two),
                GetSquareWithMockPiece(File.A, Rank.Three),
                GetSquareWithMockPiece(File.B, Rank.Three),
                GetSquareWithMockPiece(File.C, Rank.Three),
                GetSquareWithMockPiece(File.D, Rank.Three),
                GetSquareWithMockPiece(File.E, Rank.Three),
                GetSquareWithMockPiece(File.F, Rank.Three),
                GetSquareWithMockPiece(File.G, Rank.Three),
                GetSquareWithMockPiece(File.H, Rank.Three),
                GetSquareWithMockPiece(File.A, Rank.Four),
                GetSquareWithMockPiece(File.B, Rank.Four),
                GetSquareWithMockPiece(File.C, Rank.Four),
                GetSquareWithMockPiece(File.D, Rank.Four),
                GetSquareWithMockPiece(File.E, Rank.Four),
                GetSquareWithMockPiece(File.F, Rank.Four),
                GetSquareWithMockPiece(File.G, Rank.Four),
                GetSquareWithMockPiece(File.H, Rank.Four),
                GetSquareWithMockPiece(File.A, Rank.Five),
                GetSquareWithMockPiece(File.B, Rank.Five),
                GetSquareWithMockPiece(File.C, Rank.Five),
                GetSquareWithMockPiece(File.D, Rank.Five),
                GetSquareWithMockPiece(File.E, Rank.Five),
                GetSquareWithMockPiece(File.F, Rank.Five),
                GetSquareWithMockPiece(File.G, Rank.Five),
                GetSquareWithMockPiece(File.H, Rank.Five),
                GetSquareWithMockPiece(File.A, Rank.Six),
                GetSquareWithMockPiece(File.B, Rank.Six),
                GetSquareWithMockPiece(File.C, Rank.Six),
                GetSquareWithMockPiece(File.D, Rank.Six),
                GetSquareWithMockPiece(File.E, Rank.Six),
                GetSquareWithMockPiece(File.F, Rank.Six),
                GetSquareWithMockPiece(File.G, Rank.Six),
                GetSquareWithMockPiece(File.H, Rank.Six),
                GetSquareWithMockPiece(File.A, Rank.Seven),
                GetSquareWithMockPiece(File.B, Rank.Seven),
                GetSquareWithMockPiece(File.C, Rank.Seven),
                GetSquareWithMockPiece(File.D, Rank.Seven),
                GetSquareWithMockPiece(File.E, Rank.Seven),
                GetSquareWithMockPiece(File.F, Rank.Seven),
                GetSquareWithMockPiece(File.G, Rank.Seven),
                GetSquareWithMockPiece(File.H, Rank.Seven),
                GetSquareWithMockPiece(File.A, Rank.Eight),
                GetSquareWithMockPiece(File.B, Rank.Eight),
                GetSquareWithMockPiece(File.C, Rank.Eight),
                GetSquareWithMockPiece(File.D, Rank.Eight),
                GetSquareWithMockPiece(File.E, Rank.Eight),
                GetSquareWithMockPiece(File.F, Rank.Eight),
                GetSquareWithMockPiece(File.G, Rank.Eight),
                GetSquareWithMockPiece(File.H, Rank.Eight)
            ]
        };
        return position;
    }

        public static ChessPosition GetEmptyPositionExceptOnePiece(File file, Rank rank, PieceColor color, PieceType type)
    {
        var position = GetEmptyPosition();
        var square = position.Squares.Single(s => s.File == file && s.Rank == rank);
        square.Piece = GetMockGodotPiece(file, rank, type, color);
        return position;
    }

      public static ChessPosition GetStartingPositionExceptOnePiece (Rank rank, File file)
    {
        var position = new ChessPosition
        {
            Squares = [
                GetSquareWithMockPiece(File.A, Rank.One, PieceType.Rook, PieceColor.White),
                GetSquareWithMockPiece(File.B, Rank.One, PieceType.Knight, PieceColor.White),
                GetSquareWithMockPiece(File.C, Rank.One, PieceType.Bishop, PieceColor.White),
                GetSquareWithMockPiece(File.D, Rank.One, PieceType.Queen, PieceColor.White),
                GetSquareWithMockPiece(File.E, Rank.One, PieceType.King, PieceColor.White),
                GetSquareWithMockPiece(File.F, Rank.One, PieceType.Bishop, PieceColor.White),
                GetSquareWithMockPiece(File.G, Rank.One, PieceType.Knight, PieceColor.White),
                GetSquareWithMockPiece(File.H, Rank.One, PieceType.Rook, PieceColor.White),
                GetSquareWithMockPiece(File.A, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.B, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.C, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.D, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.E, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.F, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.G, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.H, Rank.Two, PieceType.Pawn, PieceColor.White),
                GetSquareWithMockPiece(File.A, Rank.Three),
                GetSquareWithMockPiece(File.B, Rank.Three),
                GetSquareWithMockPiece(File.C, Rank.Three),
                GetSquareWithMockPiece(File.D, Rank.Three),
                GetSquareWithMockPiece(File.E, Rank.Three),
                GetSquareWithMockPiece(File.F, Rank.Three),
                GetSquareWithMockPiece(File.G, Rank.Three),
                GetSquareWithMockPiece(File.H, Rank.Three),
                GetSquareWithMockPiece(File.A, Rank.Four),
                GetSquareWithMockPiece(File.B, Rank.Four),
                GetSquareWithMockPiece(File.C, Rank.Four),
                GetSquareWithMockPiece(File.D, Rank.Four),
                GetSquareWithMockPiece(File.E, Rank.Four),
                GetSquareWithMockPiece(File.F, Rank.Four),
                GetSquareWithMockPiece(File.G, Rank.Four),
                GetSquareWithMockPiece(File.H, Rank.Four),
                GetSquareWithMockPiece(File.A, Rank.Five),
                GetSquareWithMockPiece(File.B, Rank.Five),
                GetSquareWithMockPiece(File.C, Rank.Five),
                GetSquareWithMockPiece(File.D, Rank.Five),
                GetSquareWithMockPiece(File.E, Rank.Five),
                GetSquareWithMockPiece(File.F, Rank.Five),
                GetSquareWithMockPiece(File.G, Rank.Five),
                GetSquareWithMockPiece(File.H, Rank.Five),
                GetSquareWithMockPiece(File.A, Rank.Six),
                GetSquareWithMockPiece(File.B, Rank.Six),
                GetSquareWithMockPiece(File.C, Rank.Six),
                GetSquareWithMockPiece(File.D, Rank.Six),
                GetSquareWithMockPiece(File.E, Rank.Six),
                GetSquareWithMockPiece(File.F, Rank.Six),
                GetSquareWithMockPiece(File.G, Rank.Six),
                GetSquareWithMockPiece(File.H, Rank.Six),
                GetSquareWithMockPiece(File.A, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.B, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.C, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.D, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.E, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.F, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.G, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.H, Rank.Seven, PieceType.Pawn, PieceColor.Black),
                GetSquareWithMockPiece(File.A, Rank.Eight, PieceType.Rook, PieceColor.Black),
                GetSquareWithMockPiece(File.B, Rank.Eight, PieceType.Knight, PieceColor.Black),
                GetSquareWithMockPiece(File.C, Rank.Eight, PieceType.Bishop, PieceColor.Black),
                GetSquareWithMockPiece(File.D, Rank.Eight, PieceType.Queen, PieceColor.Black),
                GetSquareWithMockPiece(File.E, Rank.Eight, PieceType.King, PieceColor.Black),
                GetSquareWithMockPiece(File.F, Rank.Eight, PieceType.Bishop, PieceColor.Black),
                GetSquareWithMockPiece(File.G, Rank.Eight, PieceType.Knight, PieceColor.Black),
                GetSquareWithMockPiece(File.H, Rank.Eight, PieceType.Rook, PieceColor.Black)
            ]
        };
        position.Squares.Single(s => s.File == file && s.Rank == rank).Piece = null;
        return position;
    }
}
