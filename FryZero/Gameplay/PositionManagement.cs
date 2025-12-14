using System;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotNodes.Game.Pieces;
using Godot;

namespace FryZeroGodot.gameplay;

public static class PositionManagement
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
        foreach (var file in Enum.GetValues<File>())
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
        foreach (var file in Enum.GetValues<File>())
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
    public static void UpdatePieceNodes(ChessPosition position)
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

    public static void UpdateChessPosition(this GodotPiece piece, ChessPosition position)
    {
        var newSquare = position.Squares.Single(s => s.File == piece.File && s.Rank == piece.Rank);
        var oldSquare = position.Squares.Single(s => s.Piece == piece);
        oldSquare.Piece = null;
        newSquare.Piece = piece;
        GD.Print(oldSquare.File, oldSquare.Rank," - ", newSquare.File, newSquare.Rank);
    }


}
