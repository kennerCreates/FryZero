#nullable enable

using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.Game.Pieces;
using GodotPiece = FryZeroGodot.GodotNodes.Gameplay.Pieces.GodotPiece;

namespace FryZeroGodot.Config.Records;

public record Square (File File, Rank Rank)
{
    public  GodotPiece? Piece { get; set; }
}
