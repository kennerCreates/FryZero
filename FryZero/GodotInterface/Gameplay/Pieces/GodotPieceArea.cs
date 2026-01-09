using Godot;
using GameTheme = FryZeroGodot.GodotInterface.UI.GameTheme.GameTheme;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

[GlobalClass]

public partial class GodotPieceArea : Area2D
{
    private static RectangleShape2D _collisionShape;
    private  CollisionShape2D _collision;
    private GodotPiece _parentPiece;

    public override void _Ready()
    {
        InputPickable = true;
        AddChild(GetCollision());
        GetPieceParent();
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

    private void GetPieceParent()
    {
        var parent = GetParent<GodotPiece>();
        if (parent != null) _parentPiece = parent;
    }

    public override void _MouseEnter()
    {
        _parentPiece.SetMouseEntered(true);
    }

    public override void _MouseExit()
    {
        _parentPiece.SetMouseEntered(false);
    }
}
