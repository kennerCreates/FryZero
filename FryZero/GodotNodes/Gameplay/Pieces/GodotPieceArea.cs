using Godot;

namespace FryZeroGodot.GodotNodes.Game.Pieces;

[GlobalClass]

public partial class GodotPieceArea : Area2D
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

    private Gameplay.Pieces.GodotPiece _parentPiece;

    public override void _Ready()
    {
        if (_shape == null)
        {
            GD.Print("Shape is null");
            return;
        }

        InputPickable = true;
        SpawnCollisionShape();
        GetPieceParent();
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

    private void GetPieceParent()
    {
        var parent = GetParent<Gameplay.Pieces.GodotPiece>();
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
