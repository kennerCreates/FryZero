using FryZeroGodot.Config.Enums;
using FryZeroGodot.Root.Game.Pieces;

namespace FryZeroGodot.Config.Structs;

public struct Square(File file, Rank rank)
{
    public File File { get; set; } = file;
    public Rank Rank { get; set; } = rank;
}
