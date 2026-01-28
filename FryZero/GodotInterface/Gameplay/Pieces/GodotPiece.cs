using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotInterface.UI.Buttons;
using FryZeroGodot.Statics.Gameplay.Board;
using Godot;
using GameTheme = FryZeroGodot.GodotInterface.UI.GameTheme.GameTheme;
using GodotPieceManager = FryZeroGodot.GodotInterface.Gameplay.Board.GodotPieceManager;

namespace FryZeroGodot.GodotInterface.Gameplay.Pieces;

[GlobalClass]

public partial class GodotPiece : GodotButton, IGodotPiece
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }
    [Export] public Rank Rank { get; set; }
    [Export] public File File { get; set; }

    private GodotPieceManager _pieceManager;
    private RigidBody2D _physics;
    private CollisionShape2D _physicsShape;
    private GodotHoldPoint _holdPoint;
    private PinJoint2D _pinJoint2D;
    private bool _isMouseEntered;
    private bool _isBeingMoved;
    private bool _isOnASquare = true;

    public override void OnBeginPlay()
    {
        _pieceManager = GetParent<GodotPieceManager>();

        UpdateLocation(new Square(File, Rank));
        UpdatePieceSprite();
        AddChild(GetPhysicsPiece());
        _physics.AddChild(GetPhysicsCollision());
        GetButtonSprite().Reparent(_physics);
        AddChild(GetHoldPoint());
        AddChild(GetPinJoint());
    }

    private void UpdatePieceSprite()
    {
        UpdateSpriteTexture(
            GameTheme.Instance.GetPieceTexture(Type, Color,InteractState.Normal),
            GameTheme.Instance.GetPieceTexture(Type, Color, InteractState.Hovered)
            );
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
    private CollisionShape2D _physicsCollision;
    private CollisionShape2D GetPhysicsCollision()
    {
        _physicsCollision ??= new CollisionShape2D();
        _physicsCollision.Shape = GetButtonShape();
        return _physicsCollision;
    }
    private GodotHoldPoint GetHoldPoint()
    {
        _holdPoint ??= new GodotHoldPoint
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
    public void SetToPickedUp()
    {
        _isBeingMoved = true;
        _isOnASquare = false;
        ZIndex = 20;
    }

    private void HandlePieceOnBoardOrNot()
    {
        var closestSquare = GetGlobalMousePosition().GetSquare(GameTheme.Instance.GetSquareSize());
        var isValidSquare = closestSquare.IsValidSquare();
        if (isValidSquare)
        {
            File = closestSquare.File;
            Rank = closestSquare.Rank;
            _pieceManager.UpdateChessPosition(this);
        }
        else
        {
            _pieceManager.RemovePieceFromBoard(this);
            QueueFree();
        }

    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isBeingMoved || !_isOnASquare)
        {
            if (_isBeingMoved)
            {
                var tween = GetTree().CreateTween();
                var tweener = tween.TweenProperty(this, "position", GetGlobalMousePosition(), GameTheme.Instance.GetPieceDelay() * delta);
                tween.Finished += () =>
                {
                    tween.Dispose();
                    tweener.Dispose();
                };
            }
            else
            {
                var tween = GetTree().CreateTween();
                var targetLocation = new Square(File, Rank).LocationVector(GameTheme.Instance.GetSquareSize());
                var tweener = tween.TweenProperty(this, "position", targetLocation, GameTheme.Instance.GetPieceDelay() * delta);
                tween.Finished += () =>
                {
                    _isOnASquare = true;
                    ZIndex = 10;
                    tween.Dispose();
                    tweener.Dispose();
                };
            }
        }
    }
}
