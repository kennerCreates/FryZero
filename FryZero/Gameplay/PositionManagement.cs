using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using GodotPiece = FryZeroGodot.GodotNodes.Gameplay.Pieces.GodotPiece;

namespace FryZeroGodot.gameplay;

public static class PositionManagement
{
    public static GodotPiece CreateOneHeldPiece(PieceType type, PieceColor color)
    {
        var piece = new GodotPiece();
        piece.Type = type;
        piece.Color = color;
        return piece;
    }
    public static void UpdatePiecesFileAndRankToCurrentPosition(ChessPosition position)
    {

        // https://github.com/listenheremoose/lc0backend/blob/main/Lc0Backend/Chess/MoveUpdater.cs#L31-L40
        var squares = position.Squares;
        foreach ( var square in squares)
        {
            if (square.Piece is null) continue;
            if (square.File == square.Piece.File && square.Rank == square.Piece.Rank) continue;
            square.Piece.File = square.File;
            square.Piece.Rank = square.Rank;
        }
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
