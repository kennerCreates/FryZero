#nullable enable

using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;

namespace FryZeroGodot.Config.Records;

public record Square (File File, Rank Rank, IGodotPiece? Piece = null)
{
    public IGodotPiece? Piece { get; set; } = Piece;
}
