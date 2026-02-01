using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotInterface.UI.Buttons;
using FryZeroGodot.Statics.Gameplay.Board;
using Godot;
using GameTheme = FryZeroGodot.GodotInterface.UI.GameTheme.GameTheme;
using PieceManager = FryZeroGodot.GodotInterface.Gameplay.Board.GodotPieceManager;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

[GlobalClass]

public partial class GodotPiece : GodotButton, IGodotPiece
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }
    [Export] public Rank Rank { get; set; }
    [Export] public File File { get; set; }

    private RigidBody2D _physics;
    private CollisionShape2D _physicsShape;
    private StaticBody2D _holdPoint;
    private PinJoint2D _pinJoint2D;
    private bool _isMouseEntered;
    private bool _isBeingMoved;
    private bool _isOnASquare = true;

    public override void OnBeginPlay()
    {
        UpdateLocation(new Square(File, Rank));
        UpdatePieceSprite();
        AddChild(GetPhysicsPiece());
        _physics.AddChild(GetCollision());
        GetButtonSprite().Reparent(_physics);
        AddChild(GetHoldPoint());
        _holdPoint.AddChild(GetCollision());
        AddChild(GetPinJoint());
    }

    private void UpdatePieceSprite()
    {
        var normalTexture = GameTheme.Instance.GetPieceTexture(Type, Color, InteractState.Normal);
        var hoveredTexture = GameTheme.Instance.GetPieceTexture(Type, Color, InteractState.Hovered);
        UpdateSpriteTexture(normalTexture, hoveredTexture);

        var squareSize = GameTheme.Instance.GetSquareSize();
        UpdateSpriteSize(new Vector2(squareSize, squareSize));
    }
    private RigidBody2D GetPhysicsPiece()
    {
        _physics ??= new RigidBody2D
        {
            CollisionLayer = 0,
            CollisionMask = 0,
            ZIndex = 10,
            CanSleep = false,
            LockRotation = true
        };
        return _physics;
    }
    private CollisionShape2D GetCollision()
    {
        var collision = new CollisionShape2D();
        collision.Shape = GetButtonShape();
        return collision;
    }
    private StaticBody2D GetHoldPoint()
    {
        _holdPoint ??= new StaticBody2D()
        {
            CollisionLayer = 0,
            CollisionMask = 0
        };

        return _holdPoint;
    }
    private PinJoint2D GetPinJoint()
    {
        _pinJoint2D ??= new PinJoint2D();
        _pinJoint2D.Softness = GameTheme.Instance.GetSquareSize()/ 100f;
        _pinJoint2D.NodeA = _holdPoint.GetPath();
        _pinJoint2D.NodeB = _physics.GetPath();
        _pinJoint2D.Position = _holdPoint.Position;
        return _pinJoint2D;
    }
    private void UpdateLocation(Square square)
    {
        if (_isOnASquare)
        {
            Position = square.LocationVector(GameTheme.Instance.GetSquareSize());
        }
    }

    public override void LeftClickDown()
    {
        if (!IsMouseEntered) return;
        SetToPickedUp();
    }

    public override void LeftClickReleased()
    {
        if (_isOnASquare) return;
        _isBeingMoved = false;
        HandlePieceOnBoardOrNot();
    }

    private Vector2 _grabOffset;
    public void SetToPickedUp()
    {
        _isBeingMoved = true;
        _isOnASquare = false;
        ZIndex = 20;
        _grabOffset = GlobalPosition - GetGlobalMousePosition();
    }

    private void HandlePieceOnBoardOrNot()
    {
        var closestSquare = GetGlobalMousePosition().GetSquare(GameTheme.Instance.GetSquareSize());
        var isValidSquare = closestSquare.IsValidSquare();
        if (isValidSquare)
        {
            File = closestSquare.File;
            Rank = closestSquare.Rank;
            PieceManager.Instance.UpdateChessPosition(this);
            CreateMoveTween(closestSquare);
        }
        else
        {
            PieceManager.Instance.RemovePieceFromBoard(this);
            QueueFree();
        }

    }

    private Tween _moveTween;

    private void CreateMoveTween(Square square)
    {
        _moveTween?.Kill();
        _moveTween = GetTree().CreateTween();
        var targetLocation = square.LocationVector(GameTheme.Instance.GetSquareSize());
        _moveTween.TweenProperty(this, "position", targetLocation, .1);
        _moveTween.Finished += OnMoveFinished;

    }
    private void OnMoveFinished()
    {
        if (!IsInstanceValid(this) || !IsInsideTree())
            return;
        _isOnASquare = true;
        ZIndex = 10;
        _moveTween?.Kill();
    }
    public override void _Process(double delta)
    {
        if (!IsInsideTree())
            return;
        if (_isBeingMoved)
        {
            GlobalPosition = GlobalPosition.Lerp(GetGlobalMousePosition() + _grabOffset, 0.25f);
        }

    }
}
