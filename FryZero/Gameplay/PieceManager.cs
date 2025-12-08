using System;
using System.Collections.Generic;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotNodes.Game.Pieces;

namespace FryZeroGodot.gameplay;

public static class PieceManager
{
    public static void InitializeEmptyBoard(ChessPosition position)
    {
        position.Squares.Clear();
        foreach (var rank in Enum.GetValues<Rank>())
        {
            foreach (var file in Enum.GetValues<File>())
            {
                position.Squares.Add(new Square (file, rank));
            }
        }
    }

    public static void CreatePiecesInStartingPosition(ChessPosition position)
    {
        // White Back Rank
        SetPiece(CreateOnePiece(PieceType.Rook, PieceColor.White, Rank.One, File.A), position);
        SetPiece(CreateOnePiece(PieceType.Knight, PieceColor.White, Rank.One, File.B), position);
        SetPiece(CreateOnePiece(PieceType.Bishop, PieceColor.White, Rank.One, File.C), position);
        SetPiece(CreateOnePiece(PieceType.Queen, PieceColor.White, Rank.One, File.D), position);
        SetPiece(CreateOnePiece(PieceType.King, PieceColor.White, Rank.One, File.E), position);
        SetPiece(CreateOnePiece(PieceType.Bishop, PieceColor.White, Rank.One, File.F), position);
        SetPiece(CreateOnePiece(PieceType.Knight, PieceColor.White, Rank.One, File.G),position);
        SetPiece(CreateOnePiece(PieceType.Rook, PieceColor.White, Rank.One, File.H), position);
        // White Pawns
        foreach (File file in Enum.GetValues<File>())
        {
            SetPiece(CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, file), position);
        }
        // Black Back Rank
        SetPiece(CreateOnePiece(PieceType.Rook, PieceColor.Black, Rank.Eight, File.A), position);
        SetPiece(CreateOnePiece(PieceType.Knight, PieceColor.Black, Rank.Eight, File.B), position);
        SetPiece(CreateOnePiece(PieceType.Bishop, PieceColor.Black, Rank.Eight, File.C), position);
        SetPiece(CreateOnePiece(PieceType.Queen, PieceColor.Black, Rank.Eight, File.D), position);
        SetPiece(CreateOnePiece(PieceType.King, PieceColor.Black, Rank.Eight, File.E), position);
        SetPiece(CreateOnePiece(PieceType.Bishop, PieceColor.Black, Rank.Eight, File.F), position);
        SetPiece(CreateOnePiece(PieceType.Knight, PieceColor.Black, Rank.Eight, File.G), position);
        SetPiece(CreateOnePiece(PieceType.Rook, PieceColor.Black, Rank.Eight, File.H), position);
        // Black Pawns
        foreach (File file in Enum.GetValues<File>())
        {
            SetPiece(CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, file), position);
        }
    }

    private static void SetPiece(GodotPiece piece,ChessPosition position)
    {
        var square = position.Squares.Single(s => s.File == piece.File && s.Rank == piece.Rank);
        square.Piece = piece;
    }

    private static GodotPiece CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file)
    {
        var piece = new GodotPiece();
        piece.Type = type;
        piece.Color = color;
        piece.Rank = rank;
        piece.File = file;
        return piece;
    }
}
