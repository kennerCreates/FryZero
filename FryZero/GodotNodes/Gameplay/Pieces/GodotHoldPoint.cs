using Godot;

namespace FryZeroGodot.GodotNodes.Gameplay.Pieces;

[GlobalClass]

public partial class GodotHoldPoint : StaticBody2D
{
    private static CircleShape2D _collisionShape;
    private  CollisionShape2D _collision;

    public override void _Ready()
    {
        AddChild(GetCollision());
    }

    private static CircleShape2D GetCollisionShape()
    {
        _collisionShape ??= new CircleShape2D()
        {
            Radius = 5
        };
        return _collisionShape;
    }

    private CollisionShape2D GetCollision()
    {
        _collision ??= new CollisionShape2D();
        _collision.Shape = GetCollisionShape();
        return _collision;
    }

}
