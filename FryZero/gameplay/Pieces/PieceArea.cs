using Godot;

namespace FryZeroGodot.gameplay.Pieces;

public partial class PieceArea : Area2D
{
    private Piece _pieceParent;
    public override void _Ready()
    {
        GetPieceParent();
    }
    
    public override void _MouseEnter()
    {
        _pieceParent.SetMouseEntered(true);
    }
    public override void _MouseExit()
    {
        _pieceParent.SetMouseEntered(false);
    }
    
    private void GetPieceParent()
    {
        var parent = GetParent<Piece>();
        if (parent != null) _pieceParent = parent;
        
    }
}