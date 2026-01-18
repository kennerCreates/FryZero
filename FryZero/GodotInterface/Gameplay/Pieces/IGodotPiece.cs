using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

public interface IGodotPiece
{
    public PieceType Type { get; set; }
    public PieceColor Color { get; set; }
    public Rank Rank { get; set; }
    public File File { get; set; }
    void QueueFree();
}
