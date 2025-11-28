using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.Config.Structs;

public struct Square(File file, Rank rank)
{
    public File File { get; set; } = file;
    public Rank Rank { get; set; } = rank;
}
