#nullable enable

using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.Game.Pieces;

namespace FryZeroGodot.Config.Records;

public record Square (File File, Rank Rank)
{
    public  GodotPiece? Piece { get; set; }
}
