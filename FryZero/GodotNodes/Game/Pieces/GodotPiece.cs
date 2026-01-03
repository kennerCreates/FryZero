using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.Godot.EngineFiles;
using FryZeroGodot.GodotInterface.Extensions;
using Godot;

namespace FryZeroGodot.GodotNodes.Game.Pieces;

[GlobalClass]

public partial class GodotPiece : Node2D
{


    [Export] public int MovementDelay { get; set; } = 10;

    [Export] public int SquareSize { get; set; } = 160;
    [Export] public PieceStyle Style { get; set;} = PieceStyle.Tiny;
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    [Export] public Rank Rank { get; set; }
    [Export] public File File { get; set; }

    private Sprite2D _sprite;

    private void SetSpriteImage()
    {
        _sprite.Texture = GD.Load<Texture2D>($"res://Assets/Pieces/{Style}/{Type}.png");
    }

    private void UpdateSprite()
    {
        SetSpriteImage();
        _sprite.Material = _pieceManager.ColorScheme.GetMaterial();
        var spriteSize = _sprite.Texture.GetSize();
        _sprite.Scale = new Vector2(SquareSize, SquareSize) / spriteSize;
    }

    private void UpdateLocation(Square square)
    {
        if (_isOnASquare)
        {
            Position = square.LocationVector(SquareSize);
        }
    }

    private void CreateSprite()
    {
        _sprite = new Sprite2D();
        AddChild(_sprite);
    }

    private void UpdatePiece()
    {
        if (_sprite is null) CreateSprite();
        UpdateSprite();
        UpdateLocation(new Square(File, Rank));
        if (Engine.IsEditorHint()) return;
        _shape?.WithUpdatedShape(SquareSize);
        _physics?.WithUpdatedPhysics(_shape);
        _pieceArea?.WithUpdatedPieceArea(_shape);
        _pinJoint2D?.UpdateSoftness(SquareSize);
    }

    private RectangleShape2D _shape;
    private GodotPhysics _physics;
    private GodotHoldPoint _holdPoint;
    private GodotPieceArea _pieceArea;
    private PinJoint2D _pinJoint2D;
    private bool _isMouseEntered;
    private bool _isBeingMoved;
    private bool _isOnASquare = true;

    private void CreateRuntimePiece()
    {
        if (_shape is null)
        {
            CreateShape();
        }

        if (_physics is null)
        {
            CreatePhysicsPiece();
        }

        if (_pieceArea is null)
        {
            CreateArea();
        }

        if (_holdPoint is null)
        {
            CreateHoldPoint();
        }

        if (_pinJoint2D is null)
        {
            CreatePinJoint();
        }
    }

    private void CreateShape()
    {
        _shape = new RectangleShape2D();
        _shape.WithUpdatedShape(SquareSize);
    }


    private void CreatePhysicsPiece()
    {
        _physics = new GodotPhysics();
        _physics?.WithUpdatedPhysics(_shape);
        AddChild(_physics);
        _sprite.GetParent()?.RemoveChild(_sprite);
        _physics?.AddChild(_sprite);
    }

    private void CreateArea()
    {
        _pieceArea = new GodotPieceArea();
        _pieceArea?.WithUpdatedPieceArea(_shape);
        AddChild(_pieceArea);
    }
    private void CreateHoldPoint()
    {
        _holdPoint = new GodotHoldPoint();
        var circle = new CircleShape2D();
        circle.Radius = 5;
        _holdPoint.Shape = circle;
        _holdPoint.CollisionLayer = 0;
        _holdPoint.CollisionMask = 0;
        AddChild(_holdPoint);
    }

    private void CreatePinJoint()
    {
        if (_holdPoint == null || _physics == null) return;
        _pinJoint2D = new PinJoint2D();
        AddChild(_pinJoint2D);
        _pinJoint2D?.UpdateSoftness(SquareSize);
        if (_pinJoint2D == null) return;
        _pinJoint2D.NodeA = _holdPoint.GetPath();
        _pinJoint2D.NodeB = _physics.GetPath();
        _pinJoint2D.Position = _holdPoint.Position;
        if (_pinJoint2D.NodeA == null) GD.Print("NodeA missing");
        if (_pinJoint2D.NodeB == null) GD.Print("NodeB missing");
    }

    public void LeftClickDown()
    {
        if (!_isMouseEntered) return;
        SetToPickedUp();
        _physics.PickedUpPiece();
    }

    private GodotPieceManager _pieceManager;

    private void GetPieceManager()
    {
        _pieceManager = GetParent<GodotPieceManager>();
    }
    public void LeftClickReleased()
    {
        if (_isOnASquare) return;
        _isBeingMoved = false;
        _physics.DroppedPiece();
        HandlePieceOnBoardOrNot();
    }

    private Square FindClosestSquareLocation() =>
        GetGlobalMousePosition().GetSquare(SquareSize);

    public void HandlePieceOnBoardOrNot()
    {
        var closestSquare = FindClosestSquareLocation();
        var isValidSquare = closestSquare.IsValidSquare();
        if (isValidSquare)
        {
            File = closestSquare.File;
            Rank = closestSquare.Rank;
            this.UpdateChessPosition(_pieceManager.ChessPosition);
        }
        else
        {
            this.RemovePieceFromBoard(_pieceManager.ChessPosition);
            QueueFree();
        }

    }
    public void SetMouseEntered(bool isEntered)
    {
        _isMouseEntered = isEntered;
    }

    public void SetToPickedUp()
    {
        _isBeingMoved = true;
        _isOnASquare = false;
    }

    public override void _EnterTree()
    {
        _pieceManager = GetParent<GodotPieceManager>();
        if (_pieceManager.IsInitialized)
        {
            OnPieceManagerReady();
        }
        else
        {
           _pieceManager.PieceManagerInitialized += OnPieceManagerReady;
        }
    }

    private void OnPieceManagerReady()
    {
        GetPieceManager();
        UpdatePiece();
        CreateRuntimePiece();
        AddToGroup(CallGroups.LeftClick);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isBeingMoved || !_isOnASquare)
        {
            if (_isBeingMoved)
            {
                var tween = GetTree().CreateTween();
                var tweener = tween.TweenProperty(this, "position", GetGlobalMousePosition(), MovementDelay * delta);
                tween.Finished += () =>
                {
                    tween.Dispose();
                    tweener.Dispose();
                };
            }
            else
            {
                var tween = GetTree().CreateTween();
                var targetLocation = new Square(File, Rank).LocationVector(SquareSize);
                var tweener = tween.TweenProperty(this, "position", targetLocation, MovementDelay * delta);
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
