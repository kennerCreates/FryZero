#nullable enable

using FryZeroGodot.Config.Enums;
using GodotPiece = FryZeroGodot.GodotInterface.Gameplay.Pieces.GodotPiece;

namespace FryZeroGodot.Config.Records;

public record Square (File File, Rank Rank)
{
    public  GodotPiece? Piece { get; set; }
}
