using FryZeroGodot.Config.Enums;

namespace FryZeroGodot.GodotInterface.Models;

public record Piece
{
    public PieceType Type {get; init;}
    public PieceColor Color {get; init;}
    public Rank Rank {get; init;}
    public File File {get; init;}
}
