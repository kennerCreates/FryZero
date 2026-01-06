using Godot;

namespace FryZeroGodot.GodotNodes.Game.Pieces;

[GlobalClass]
public partial class GodotPhysics : RigidBody2D
{
    private RectangleShape2D _shape;
    private CollisionShape2D _collision;


    [Export]
    public RectangleShape2D Shape
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
        CollisionLayer = 0;
        CollisionMask = 0;
        ZIndex = 10;
        CanSleep = false;
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
        CollisionLayer = 0;
        CollisionMask = 0;
        ZIndex = 10;
    }


    public void PickedUpPiece()
    {
        //ApplyImpulse(new Vector2(5000,0), Position);
        CollisionLayer = 1;
        CollisionMask = 1;
        ZIndex = 20;
    }
}
