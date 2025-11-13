using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.gameplay.Pieces;

public class PieceOptions
{
    public int SquareSize { get; init; }
    public File PieceFile { get; init; }
    public Rank PieceRank { get; init; }
}