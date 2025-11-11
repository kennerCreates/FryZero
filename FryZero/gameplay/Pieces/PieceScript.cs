using System.Xml;
using Godot;

namespace FryZeroGodot.gameplay.Pieces;
[GlobalClass]
public partial class PieceScript : Area2D
{
    public enum PieceType { Pawn, Knight, Bishop, Rook, Queen, King }
    public enum PieceColor { White, Black}
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }
}
