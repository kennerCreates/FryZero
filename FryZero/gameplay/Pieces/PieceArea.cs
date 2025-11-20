using Godot;

namespace FryZeroGodot.gameplay.Pieces;
[GlobalClass]
public partial class PieceArea : Area2D
{
    [Export] public Shape2D Shape;
    
    private Piece _boardLocationParent;

    public override void _Ready()
    {
        SpawnCollisionShape();
        GetPieceParent();
    }

    public override void _MouseEnter()
    {
        _boardLocationParent.SetMouseEntered(true);
        GD.Print("Mouse entered");
    } 
        
    public override void _MouseExit()
    {
        _boardLocationParent.SetMouseEntered(false);
        GD.Print("Mouse entered");
    }
       

    private void SpawnCollisionShape()
    {
        var collisionShape = new CollisionShape2D();
        collisionShape.Shape = Shape;
        AddChild(collisionShape);
        GD.Print("Area Collision added");
    }
    private void GetPieceParent()
    {
        var parent = GetParent<Piece>();
        if (parent != null) _boardLocationParent = parent;
        
    }
}