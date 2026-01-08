using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.GodotNodes.Gameplay.Board;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.Gameplay.Pieces;

[GlobalClass]

public partial class GodotPiece : Node2D
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    private InteractState _interactState = InteractState.Normal;

    [Export] public Rank Rank { get; set; }
    [Export] public File File { get; set; }

    private Sprite2D _sprite;
    private GodotPhysics _physics;
    private GodotHoldPoint _holdPoint;
    private GodotPieceArea _pieceArea;
    private PinJoint2D _pinJoint2D;
    private bool _isMouseEntered;
    private bool _isBeingMoved;
    private bool _isOnASquare = true;

    public override void _Ready()
    {
        ZIndex = 9;
        UpdateLocation(new Square(File, Rank));
        AddChild(GetPhysicsPiece());
        _physics.AddChild(GetPieceSprite());
        AddChild(GetPieceArea());
        AddChild(GetHoldPoint());
        AddChild(GetPinJoint());
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
    private GodotPhysics GetPhysicsPiece()
    {
        _physics ??= new GodotPhysics();
        return _physics;
    }

    private GodotPieceArea GetPieceArea()
    {
        _pieceArea ??= new GodotPieceArea();
        return _pieceArea;
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
        _pinJoint2D.Softness = GameTheme.GetSquareSize()/ 100f;
        _pinJoint2D.NodeA = _holdPoint.GetPath();
        _pinJoint2D.NodeB = _physics.GetPath();
        _pinJoint2D.Position = _holdPoint.Position;
        return _pinJoint2D;
    }

    private void UpdateLocation(Square square)
    {
        if (_isOnASquare)
        {
            Position = square.LocationVector(GameTheme.GetSquareSize());
        }
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

    private void HandlePieceOnBoardOrNot()
    {
        var closestSquare = GetGlobalMousePosition().GetSquare(GameTheme.GetSquareSize());
        var isValidSquare = closestSquare.IsValidSquare();
        if (isValidSquare)
        {
            File = closestSquare.File;
            Rank = closestSquare.Rank;
            GodotPieceManager.UpdateChessPosition(this);
        }
        else
        {
            GodotPieceManager.RemovePieceFromBoard(this);
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
