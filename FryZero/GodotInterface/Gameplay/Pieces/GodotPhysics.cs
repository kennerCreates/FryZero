using Godot;
using GameTheme = FryZeroGodot.GodotInterface.UI.GameTheme.GameTheme;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

[GlobalClass]
public partial class GodotPhysics : RigidBody2D
{
    private static RectangleShape2D _collisionShape;
    private  CollisionShape2D _collision;

    public override void _Ready()
    {
        AddChild(GetCollision());
        CollisionLayer = 0;
        CollisionMask = 0;
        ZIndex = 10;
        CanSleep = false;
    }

    private static RectangleShape2D GetCollisionShape()
    {
        _collisionShape ??= new RectangleShape2D
        {
            Size = new Vector2(GameTheme.Instance.GetSquareSize(), GameTheme.Instance.GetSquareSize())
        };
        return _collisionShape;
    }

    private CollisionShape2D GetCollision()
    {
        _collision ??= new CollisionShape2D();
        _collision.Shape = GetCollisionShape();
        return _collision;
    }


    public void DroppedPiece()
    {
        CollisionLayer = 0;
        CollisionMask = 0;
        ZIndex = 10;
    }


    public void PickedUpPiece()
    {
        CollisionLayer = 1;
        CollisionMask = 1;
        ZIndex = 20;
    }
}
