using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

public class GodotPieceFactory : IGodotPieceFactory
{
	public IGodotPiece CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file) => new GodotPiece()
	{
		Type = type,
		Color = color,
		Rank = rank,
		File = file
	};
}
