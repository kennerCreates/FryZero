#nullable enable

using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using GodotPiece = FryZeroGodot.GodotInterface.Gameplay.Pieces.GodotPiece;

namespace FryZeroGodot.Config.Records;

public record Square (File File, Rank Rank)
{
    public  GodotPiece? Piece { get; set; }
    public PieceType? PieceType { get; set; }
    public PieceColor? PieceColor { get; set; }
}
