using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.GodotNodes.Game.Pieces;
using FryZeroGodot.GodotNodes.NodeModels;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.Gameplay.Pieces;

[GlobalClass]

public partial class GodotPiece : LevelOneNode
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    private InteractState _interactState = InteractState.Normal;

    [Export] public Rank Rank { get; set; }
    [Export] public File File { get; set; }

    private Sprite2D _sprite;
    private RectangleShape2D _collisionShape;
    private GodotPhysics _physics;
    private CircleShape2D _circleShape;
    private GodotHoldPoint _holdPoint;
    private GodotPieceArea _pieceArea;
    private PinJoint2D _pinJoint2D;
    private bool _isMouseEntered;
    private bool _isBeingMoved;
    private bool _isOnASquare = true;

    protected override void OnReady()
    {
        ZIndex = 9;
        CreatePiece();
        AddToGroup(CallGroups.LeftClick);
    }

    private Sprite2D GetPieceSprite()
    {
        _sprite ??= new Sprite2D();
        _sprite.Texture = GodotPieceManager.GetPieceTexture(Type, Color, _interactState);
        _sprite.Material = GameTheme.GetThemeMaterial();
        _sprite.Scale = new Vector2(GameTheme.GetSquareSize(), GameTheme.GetSquareSize()) / _sprite.Texture.GetSize();
        return _sprite;
    }

    private RectangleShape2D GetCollisionShape()
    {
        _collisionShape ??= new RectangleShape2D();
        _collisionShape.WithUpdatedShape(GameTheme.GetSquareSize());
        return _collisionShape;
    }

    private CircleShape2D GetCircleShape()
    {
        _circleShape ??= new CircleShape2D
        {
            Radius = 5
        };
        return _circleShape;
    }

    private GodotPhysics GetPhysicsPiece()
    {
        _physics ??= new GodotPhysics();
        _physics.WithUpdatedPhysics(GetCollisionShape());
        return _physics;
    }

    private GodotPieceArea GetPieceArea()
    {
        _pieceArea ??= new GodotPieceArea();
        _pieceArea?.WithUpdatedPieceArea(GetCollisionShape());
        return _pieceArea;
    }

    private GodotHoldPoint GetHoldPoint()
    {
        _holdPoint ??= new GodotHoldPoint
        {
            CollisionLayer = 0,
            CollisionMask = 0
        };
        _holdPoint.Shape = GetCircleShape();
        return _holdPoint;
    }

    private PinJoint2D GetPinJoint()
    {
        _pinJoint2D ??= new PinJoint2D();
        _pinJoint2D.WithUpdatedSoftness(GameTheme.GetSquareSize());
        _pinJoint2D.NodeA = _holdPoint.GetPath();
        _pinJoint2D.NodeB = _physics.GetPath();
        _pinJoint2D.Position = _holdPoint.Position;
        return _pinJoint2D;
    }

    private void CreatePiece()
    {
        GetCollisionShape();
        AddChild(GetPhysicsPiece());
        _physics.AddChild(GetPieceSprite());
        AddChild(GetPieceArea());
        AddChild(GetHoldPoint());
        AddChild(GetPinJoint());
    }

    private void MovePieceToSquare(Square square)
    {
        if (_isOnASquare)
        {
            Position = square.LocationVector(GameTheme.GetSquareSize());
        }
    }
    private Square FindClosestSquareLocation() =>
        GetGlobalMousePosition().GetSquare(GameTheme.GetSquareSize());

    private GodotPieceManager GetPieceManager()
    {
        return GetParent<GodotPieceManager>();
    }

    private void HandlePieceOnBoardOrNot()
    {
        var closestSquare = FindClosestSquareLocation();
        var isValidSquare = closestSquare.IsValidSquare();
        if (isValidSquare)
        {
            File = closestSquare.File;
            Rank = closestSquare.Rank;
            this.UpdateChessPosition(GetPieceManager().ChessPosition);
        }
        else
        {
            this.RemovePieceFromBoard(GetPieceManager().ChessPosition);
            QueueFree();
        }

    }
    public void SetMouseEntered(bool isEntered)
    {
        _isMouseEntered = isEntered;
        if (isEntered || _isBeingMoved)
        {
            _interactState = InteractState.Hovered;
        }
        else
        {
            _interactState = InteractState.Normal;
        }
        GetPieceSprite();
    }

    public void LeftClickDown()
    {
        if (!_isMouseEntered) return;
        SetToPickedUp();
        _physics.PickedUpPiece();
    }

    public void LeftClickReleased()
    {
        if (_isOnASquare) return;
        _isBeingMoved = false;
        _physics.DroppedPiece();
        HandlePieceOnBoardOrNot();
    }

    public void SetToPickedUp()
    {
        _isBeingMoved = true;
        _isOnASquare = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isBeingMoved || !_isOnASquare)
        {
            if (_isBeingMoved)
            {
                var tween = GetTree().CreateTween();
                var tweener = tween.TweenProperty(this, "position", GetGlobalMousePosition(), GameTheme.GetPieceDelay() * delta);
                tween.Finished += () =>
                {
                    tween.Dispose();
                    tweener.Dispose();
                };
            }
            else
            {
                var tween = GetTree().CreateTween();
                var targetLocation = new Square(File, Rank).LocationVector(GameTheme.GetSquareSize());
                var tweener = tween.TweenProperty(this, "position", targetLocation, GameTheme.GetPieceDelay() * delta);
                tween.Finished += () =>
                {
                    _isOnASquare = true;
                    tween.Dispose();
                    tweener.Dispose();
                };
            }
        }
    }
}
