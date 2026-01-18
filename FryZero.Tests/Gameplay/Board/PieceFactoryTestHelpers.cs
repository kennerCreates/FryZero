using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using NSubstitute;
using File = FryZeroGodot.Config.Enums.File;

namespace FryZero.Tests.Gameplay.Board;

public static class PieceFactoryTestHelpers
{
	public static void SetupCreateOnePiece(IGodotPieceFactory pieceFactory, PieceType type, PieceColor color, Rank rank, File file)
	{
		var piece = Substitute.For<IGodotPiece>();
		piece.Type = type;
		piece.Color = color;
		piece.Rank = rank;
		piece.File = file;
		pieceFactory.CreateOnePiece(type, color, rank, file).Returns(piece);
	}
}
