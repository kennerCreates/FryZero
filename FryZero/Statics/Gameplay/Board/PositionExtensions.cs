using System;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using Godot;

namespace FryZeroGodot.Statics.Gameplay.Board;

public static class PositionExtensions
{
	public static ChessPosition SetupPositionWithEmptyBoard(this ChessPosition position)
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
		return position;
	}

	public static ChessPosition SetupPiecesInStartingChessPosition(this ChessPosition position, IGodotPieceFactory pieceFactory)
	{
		position = position.SetupBackRankInStartingPosition(PieceColor.White, pieceFactory);
		position = position.SetupPawnsInStartingPosition(PieceColor.White, pieceFactory);
		position = position.SetupBackRankInStartingPosition(PieceColor.Black, pieceFactory);
		position = position.SetupPawnsInStartingPosition(PieceColor.Black, pieceFactory);
		return position;
	}


	public static ChessPosition SetupBackRankInStartingPosition(this ChessPosition position, PieceColor color, IGodotPieceFactory pieceFactory)
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
			position.SetPieceInPosition(pieceFactory.CreateOnePiece(type, color, rank, file));
		}
		return position;
	}

	public static ChessPosition SetupPawnsInStartingPosition(this ChessPosition position, PieceColor color, IGodotPieceFactory pieceFactory)
	{
		foreach (var file in Enum.GetValues<File>())
		{
			position.SetPieceInPosition(pieceFactory.CreateOnePiece(PieceType.Pawn, color, color.ToRankForStartingPawns(), file));
		}
		return position;
	}

	public static Rank ToRankForStartingPawns(this PieceColor color) => color switch
	{
		PieceColor.White => Rank.Two,
		PieceColor.Black => Rank.Seven,
		_ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
	};

	public static Square SetPieceInPosition(this ChessPosition position, IGodotPiece piece)
	{
		var square = position.GetSquare(piece.File, piece.Rank);
		square.Piece = piece;
		return square;
	}
	public static Square GetSquare(this ChessPosition position, File file, Rank rank) =>
		position.Squares.Single(s => s.File == file && s.Rank == rank);

	public static ChessPosition UpdateChessPosition(this ChessPosition position, IGodotPiece piece)
	{
		var newSquare = position.Squares.Single(s => s.File == piece.File && s.Rank == piece.Rank);
		var oldSquare = position.Squares.SingleOrDefault(s => s.Piece == piece);
		if (oldSquare != null)
		{
			oldSquare.Piece = null;
		}
		var currentPiece = newSquare.Piece;
		//GD.Print(piece.Type + ": " + oldSquare?.File + "" + oldSquare?.Rank + " " + newSquare.File + "" + newSquare.Rank);
		currentPiece?.QueueFree();
		newSquare.Piece = piece;
		return position;
	}

	public static ChessPosition RemovePieceFromBoard(this ChessPosition position, IGodotPiece piece)
	{
		var square = position.Squares.Single(s => s.Rank == piece.Rank && s.File == piece.File);
		square.Piece = null;
		//GD.Print("Piece dropped off board");
		return position;
	}

	// public static void UpdatePiecesFileAndRankToCurrentPosition(ChessPosition position)
	// {
	//
	//     // https://github.com/listenheremoose/lc0backend/blob/main/Lc0Backend/Chess/MoveUpdater.cs#L31-L40
	//     var squares = position.Squares;
	//     foreach ( var square in squares)
	//     {
	//         if (square.Piece is null) continue;
	//         if (square.File == square.Piece.File && square.Rank == square.Piece.Rank) continue;
	//         square.Piece.File = square.File;
	//         square.Piece.Rank = square.Rank;
	//     }
	// }

}
