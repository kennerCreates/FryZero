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

    public static void SetPiece(GodotPiece piece,ChessPosition position)
    {
        var square = position.Squares.Single(s => s.File == piece.File && s.Rank == piece.Rank);
        square.Piece = piece;
    }

    public static GodotPiece CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file)
    {
        var piece = new GodotPiece();
        piece.Type = type;
        piece.Color = color;
        piece.Rank = rank;
        piece.File = file;
        return piece;
    }
    public static GodotPiece CreateOneHeldPiece(PieceType type, PieceColor color)
    {
        var piece = new GodotPiece();
        piece.Type = type;
        piece.Color = color;
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
        var newSquare = position.Squares.SingleOrDefault(s => s.File == piece.File && s.Rank == piece.Rank);
        var oldSquare = position.Squares.SingleOrDefault(s => s.Piece == piece);
        if (oldSquare != null) oldSquare.Piece = null;
        if (newSquare != null)
        {
            var currentPiece = newSquare.Piece;
            if (currentPiece != null)
            {
                currentPiece.QueueFree();
            }
            newSquare.Piece = piece;
        }
        // if (oldSquare == null) return;
        // if (newSquare != null)
        //     GD.Print(oldSquare.File, oldSquare.Rank, " - ", newSquare.File, newSquare.Rank);
    }

    public static void RemovePieceFromBoard(this GodotPiece piece, ChessPosition position)
    {
        var square = position.Squares.SingleOrDefault(s => s.Piece == piece);
        if (square != null) square.Piece = null;
        GD.Print("Piece dropped off board");
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
