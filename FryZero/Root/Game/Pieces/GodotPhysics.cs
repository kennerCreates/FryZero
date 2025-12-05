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
        SpawnCollisionShape();
        CollisionLayer = 9;
        ZIndex = 10;
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
        CollisionLayer = 9;
        ZIndex = 10;
    }

    public void PickedUpPiece()
    {
        CollisionLayer = 10;
        ZIndex = 20;
    }
}
