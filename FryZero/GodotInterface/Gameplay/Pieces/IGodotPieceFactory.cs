using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

public interface IGodotPieceFactory
{
    IGodotPiece CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file);
}
