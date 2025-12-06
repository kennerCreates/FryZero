using Godot;

namespace FryZeroGodot.Root.Game.Pieces;

[GlobalClass]

public partial class GodotHoldPoint : StaticBody2D
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
}
