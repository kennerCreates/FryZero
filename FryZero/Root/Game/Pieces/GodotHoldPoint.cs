using FryZeroGodot.gameplay;
using Godot;

namespace FryZeroGodot.Root.Game.Pieces;

[GlobalClass]

public partial class GodotHoldPoint : StaticBody2D
{
    private Shape2D _shape;
    private int _squareSize;
    private CollisionShape2D _collision;
    private GodotPiece _piece;
    private bool _isMoving;

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

    [Export]
    public int SquareSize
    {
        get => _squareSize;
        set
        {
            _squareSize = value;
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
        _piece = GetParent<GodotPiece>();
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
