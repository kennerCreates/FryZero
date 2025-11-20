using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.Config.Pieces;

public class PieceAttributes
{
    public int SquareSize { get; init; }
    public int MovementDelay { get; init; }
    public File StartingFile { get; init; }
    public Rank StartingRank { get; init; }
}