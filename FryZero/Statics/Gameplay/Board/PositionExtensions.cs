using System;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using Godot;
using GodotPiece = FryZeroGodot.GodotInterface.Gameplay.Pieces.GodotPiece;

namespace FryZeroGodot.Statics.Gameplay.Board;

public static class PositionExtensions
{
    public static void SetupPositionWithEmptyBoard(this ChessPosition position) // Move to ChessPositionExtensions
    {
        position ??= new ChessPosition();
        position.Squares.Clear();
        foreach (var rank in Enum.GetValues<Rank>())
        {
            foreach (var file in Enum.GetValues<File>())
            {
                position.Squares.Add(new Square (file, rank));
            }
        }
    }
    public static void SetupPiecesInStartingChessPosition(this ChessPosition position)
    {
        position.SetupBackrankInStartingPosition(PieceColor.White);
        position.SetupPawnsInStartingPosition(PieceColor.White);
        position.SetupBackrankInStartingPosition(PieceColor.Black);
        position.SetupPawnsInStartingPosition(PieceColor.Black);
    }

    private static void SetupBackrankInStartingPosition(this ChessPosition position, PieceColor color)
    {
        foreach (var file in Enum.GetValues<File>())
        {
            var rank = color switch
            {
                PieceColor.White => Rank.One,
                PieceColor.Black => Rank.Eight,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
            var type = file switch
            {
                File.A => PieceType.Rook,
                File.B => PieceType.Knight,
                File.C => PieceType.Bishop,
                File.D => PieceType.Queen,
                File.E => PieceType.King,
                File.F => PieceType.Bishop,
                File.G => PieceType.Knight,
                File.H => PieceType.Rook,
                _ => throw new ArgumentOutOfRangeException(nameof(file), file, null)
            };
            position.SetPieceInPosition(CreateOnePiece(type, color, rank, file));
        }
    }
    private static void SetupPawnsInStartingPosition(this ChessPosition position, PieceColor color)
    {
        foreach (var file in Enum.GetValues<File>())
        {
            var rank = color switch
            {
                PieceColor.White => Rank.Two,
                PieceColor.Black => Rank.Seven,
                _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
            };
            position.SetPieceInPosition(CreateOnePiece(PieceType.Pawn, color, rank, file));
        }
    }

    private static void SetPieceInPosition(this ChessPosition position, GodotPiece piece)
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

    public static void UpdateChessPosition(this ChessPosition position, GodotPiece piece)
    {
        var newSquare = position.Squares.SingleOrDefault(s => s.File == piece.File && s.Rank == piece.Rank);
        var oldSquare = position.Squares.SingleOrDefault(s => s.Piece == piece);
        if (oldSquare != null) oldSquare.Piece = null;
        if (newSquare == null) return;
        var currentPiece = newSquare.Piece;
        GD.Print(piece.Type + ": " + oldSquare?.File + "" + oldSquare?.Rank + " " + newSquare.File + "" +
                 newSquare.Rank);
        currentPiece?.QueueFree();
        newSquare.Piece = piece;
    }

    public static void RemovePieceFromBoard(this ChessPosition position, GodotPiece piece)
    {
        var square = position.Squares.SingleOrDefault(s => s.Piece == piece);
        if (square != null) square.Piece = null;
        GD.Print("Piece dropped off board");
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

}
