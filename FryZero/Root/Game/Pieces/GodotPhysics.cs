using Godot;

namespace FryZeroGodot.Root.Game.Pieces;

[GlobalClass]
public partial class GodotPhysics : RigidBody2D
{
    private Shape2D _shape;
    private CollisionShape2D _collision;

    [Export]
    public Shape2D Shape
    {
        get => _shape;
        set
        {
            _shape = value;
            UpdateCollisionShape();
        }
    }

    public override void _Ready()
    {
        if (_shape == null)
        {
            GD.Print("Shape is null");
            return;
        }
        SpawnCollisionShape();
        Freeze = true;
        CollisionLayer = 0;
        CollisionMask = 0;
        ZIndex = 10;
        AngularDamp = 5;
    }

    private void SpawnCollisionShape()
    {
        _collision = new CollisionShape2D();
        _collision.Shape = _shape;
        AddChild(_collision);
    }

    private void UpdateCollisionShape()
    {
        if (_collision == null) return;
        _collision.Shape = _shape;
    }

    public void DroppedPiece()
    {
        Freeze = true;
        CollisionLayer = 0;
        CollisionMask = 0;
        ZIndex = 10;
        GD.Print("Dropped Piece");
    }

    public void PickedUpPiece()
    {
        Freeze = false;
        CollisionLayer = 1;
        CollisionMask = 1;
        ZIndex = 20;
        GD.Print("Picked Up Piece");
    }
}
