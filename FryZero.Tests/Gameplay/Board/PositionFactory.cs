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
    public static IGodotPiece GetMockGodotPiece(PieceType type, PieceColor color)
    {
        var piece = Substitute.For<IGodotPiece>();
        piece.Type = type;
        piece.Color = color;
        return piece;
    }

    public static ChessPosition GetStartingPosition()
    {
        var position = new ChessPosition
        {
            Squares = [
                new Square(File.A, Rank.One, GetMockGodotPiece(PieceType.Rook, PieceColor.White)),
                new Square(File.B, Rank.One, GetMockGodotPiece(PieceType.Knight, PieceColor.White)),
                new Square(File.C, Rank.One, GetMockGodotPiece(PieceType.Bishop, PieceColor.White)),
                new Square(File.D, Rank.One, GetMockGodotPiece(PieceType.Queen, PieceColor.White)),
                new Square(File.E, Rank.One, GetMockGodotPiece(PieceType.King, PieceColor.White)),
                new Square(File.F, Rank.One, GetMockGodotPiece(PieceType.Bishop, PieceColor.White)),
                new Square(File.G, Rank.One, GetMockGodotPiece(PieceType.Knight, PieceColor.White)),
                new Square(File.H, Rank.One, GetMockGodotPiece(PieceType.Rook, PieceColor.White)),
                new Square(File.A, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.B, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.C, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.D, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.E, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.F, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.G, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.H, Rank.Two, GetMockGodotPiece(PieceType.Pawn, PieceColor.White)),
                new Square(File.A, Rank.Three),
                new Square(File.B, Rank.Three),
                new Square(File.C, Rank.Three),
                new Square(File.D, Rank.Three),
                new Square(File.E, Rank.Three),
                new Square(File.F, Rank.Three),
                new Square(File.G, Rank.Three),
                new Square(File.H, Rank.Three),
                new Square(File.A, Rank.Four),
                new Square(File.B, Rank.Four),
                new Square(File.C, Rank.Four),
                new Square(File.D, Rank.Four),
                new Square(File.E, Rank.Four),
                new Square(File.F, Rank.Four),
                new Square(File.G, Rank.Four),
                new Square(File.H, Rank.Four),
                new Square(File.A, Rank.Five),
                new Square(File.B, Rank.Five),
                new Square(File.C, Rank.Five),
                new Square(File.D, Rank.Five),
                new Square(File.E, Rank.Five),
                new Square(File.F, Rank.Five),
                new Square(File.G, Rank.Five),
                new Square(File.H, Rank.Five),
                new Square(File.A, Rank.Six),
                new Square(File.B, Rank.Six),
                new Square(File.C, Rank.Six),
                new Square(File.D, Rank.Six),
                new Square(File.E, Rank.Six),
                new Square(File.F, Rank.Six),
                new Square(File.G, Rank.Six),
                new Square(File.H, Rank.Six),
                new Square(File.A, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.B, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.C, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.D, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.E, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.F, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.G, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.H, Rank.Seven, GetMockGodotPiece(PieceType.Pawn, PieceColor.Black)),
                new Square(File.A, Rank.Eight, GetMockGodotPiece(PieceType.Rook, PieceColor.Black)),
                new Square(File.B, Rank.Eight, GetMockGodotPiece(PieceType.Knight, PieceColor.Black)),
                new Square(File.C, Rank.Eight, GetMockGodotPiece(PieceType.Bishop, PieceColor.Black)),
                new Square(File.D, Rank.Eight, GetMockGodotPiece(PieceType.Queen, PieceColor.Black)),
                new Square(File.E, Rank.Eight, GetMockGodotPiece(PieceType.King, PieceColor.Black)),
                new Square(File.F, Rank.Eight, GetMockGodotPiece(PieceType.Bishop, PieceColor.Black)),
                new Square(File.G, Rank.Eight, GetMockGodotPiece(PieceType.Knight, PieceColor.Black)),
                new Square(File.H, Rank.Eight, GetMockGodotPiece(PieceType.Rook, PieceColor.Black))
            ]
        };
        return position;
    }

    public static ChessPosition GetEmptyPosition()
    {
        var position = new ChessPosition
        {
            Squares = [
                new Square(File.A, Rank.One),
                new Square(File.B, Rank.One),
                new Square(File.C, Rank.One),
                new Square(File.D, Rank.One),
                new Square(File.E, Rank.One),
                new Square(File.F, Rank.One),
                new Square(File.G, Rank.One),
                new Square(File.H, Rank.One),
                new Square(File.A, Rank.Two),
                new Square(File.B, Rank.Two),
                new Square(File.C, Rank.Two),
                new Square(File.D, Rank.Two),
                new Square(File.E, Rank.Two),
                new Square(File.F, Rank.Two),
                new Square(File.G, Rank.Two),
                new Square(File.H, Rank.Two),
                new Square(File.A, Rank.Three),
                new Square(File.B, Rank.Three),
                new Square(File.C, Rank.Three),
                new Square(File.D, Rank.Three),
                new Square(File.E, Rank.Three),
                new Square(File.F, Rank.Three),
                new Square(File.G, Rank.Three),
                new Square(File.H, Rank.Three),
                new Square(File.A, Rank.Four),
                new Square(File.B, Rank.Four),
                new Square(File.C, Rank.Four),
                new Square(File.D, Rank.Four),
                new Square(File.E, Rank.Four),
                new Square(File.F, Rank.Four),
                new Square(File.G, Rank.Four),
                new Square(File.H, Rank.Four),
                new Square(File.A, Rank.Five),
                new Square(File.B, Rank.Five),
                new Square(File.C, Rank.Five),
                new Square(File.D, Rank.Five),
                new Square(File.E, Rank.Five),
                new Square(File.F, Rank.Five),
                new Square(File.G, Rank.Five),
                new Square(File.H, Rank.Five),
                new Square(File.A, Rank.Six),
                new Square(File.B, Rank.Six),
                new Square(File.C, Rank.Six),
                new Square(File.D, Rank.Six),
                new Square(File.E, Rank.Six),
                new Square(File.F, Rank.Six),
                new Square(File.G, Rank.Six),
                new Square(File.H, Rank.Six),
                new Square(File.A, Rank.Seven),
                new Square(File.B, Rank.Seven),
                new Square(File.C, Rank.Seven),
                new Square(File.D, Rank.Seven),
                new Square(File.E, Rank.Seven),
                new Square(File.F, Rank.Seven),
                new Square(File.G, Rank.Seven),
                new Square(File.H, Rank.Seven),
                new Square(File.A, Rank.Eight),
                new Square(File.B, Rank.Eight),
                new Square(File.C, Rank.Eight),
                new Square(File.D, Rank.Eight),
                new Square(File.E, Rank.Eight),
                new Square(File.F, Rank.Eight),
                new Square(File.G, Rank.Eight),
                new Square(File.H, Rank.Eight)
            ]
        };
        return position;
    }
}
