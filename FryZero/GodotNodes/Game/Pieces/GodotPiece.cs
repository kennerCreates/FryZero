using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.Godot.EngineFiles;
using FryZeroGodot.GodotInterface;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotInterface.Models;
using Godot;

namespace FryZeroGodot.GodotNodes.Game.Pieces;

[Tool]

[GlobalClass]

public partial class GodotPiece : Node2D
{
    private int _squareSize = 160;
    private PieceStyle _style;
    private PieceType _type;
    private PieceColor _color;
    private Rank _rank;
    private File _file;
    private Color _lightPieceColor = Colors.White;
    private Color _lightPieceOutlineColor = Colors.Black;
    private Color _darkPieceColor = Colors.Black;
    private Color _darkPieceOutlineColor = Colors.White;

    [Export] public int MovementDelay { get; set; } = 10;

    [Export] public int SquareSize
    {
        get => _squareSize;
        set
        {
            _squareSize = value;
            UpdatePiece();
        }
    }
    [Export]
    public PieceStyle Style
    {
        get => _style;
        set
        {
            _style = value;
            UpdatePiece();
        }
    }
    [Export] public PieceType Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdatePiece();
        }
    }
    [Export] public PieceColor Color
    {
        get => _color;
        set
        {
            _color = value;
            UpdatePiece();
        }
    }
    [Export] public Rank Rank
    {
        get => _rank;
        set
        {
            _rank = value;
            UpdatePiece();
        }
    }
    [Export] public File File
    {
        get => _file;
        set
        {
            _file = value;
            UpdatePiece();
        }
    }
    [Export] public Color LightPieceColor
    {
        get => _lightPieceColor;
        set
        {
            _lightPieceColor = value;
            UpdatePiece();
        }
    }
    [Export]
    public Color LightPieceOutlineColor
    {
        get => _lightPieceOutlineColor;
        set
        {
            _lightPieceOutlineColor = value;
            UpdatePiece();
        }
    }
    [Export] public Color DarkPieceColor
    {
        get => _darkPieceColor;
        set
        {
            _darkPieceColor = value;
            UpdatePiece();
        }
    }
    [Export]
    public Color DarkPieceOutlineColor
    {
        get => _darkPieceOutlineColor;
        set
        {
            _darkPieceOutlineColor = value;
            UpdatePiece();
        }
    }
    private Sprite2D _sprite;
    private ShaderMaterial _material;

    private void CreateHueShiftShader()
    {
        _material = new ShaderMaterial();
        _material.Shader = GD.Load<Shader>("res://GodotNodes/Visuals/HueShift.gdshader");
    }

    private void SetSpriteImage()
    {
        _sprite.Texture = GD.Load<Texture2D>($"res://Assets/Pieces/{Style}/{Type}.png");
    }

    private void UpdateSprite()
    {
        SetSpriteImage();
        _sprite.Material = _material;
        var spriteSize = _sprite.Texture.GetSize();
        _sprite.Scale = new Vector2(SquareSize, SquareSize) / spriteSize;
        SetSpriteColor();
    }

    private void SetSpriteColor()
    {
        var spriteColor = new Color(_color == PieceColor.White ? _lightPieceColor : _darkPieceColor);
        var outlineColor = new Color(_color == PieceColor.White ? _lightPieceOutlineColor : _darkPieceOutlineColor);
        _material.SetShaderParameter("main_color", spriteColor);
        _material.SetShaderParameter("outline_color", outlineColor);
    }

    private void UpdateLocation(Square square)
    {
        Position = square.LocationVector(_squareSize);
    }

    private void CreateSprite()
    {
        _sprite = new Sprite2D();
        AddChild(_sprite);
        CreateHueShiftShader();
    }

    public void UpdatePiece()
    {
        if (_sprite is null) CreateSprite();
        UpdateSprite();
        UpdateLocation(new Square(_file, _rank));
        if (Engine.IsEditorHint()) return;
        _shape?.UpdateShape(_squareSize);
        UpdatePhysicsPiece();
        _pinJoint2D?.UpdateSoftness(_squareSize);
    }

    private RectangleShape2D _shape;
    private GodotPhysics _physics;
    private GodotHoldPoint _holdPoint;
    private GodotArea _area;
    private PinJoint2D _pinJoint2D;
    private bool _isMouseEntered;
    private bool _isBeingMoved;
    private bool _isOnASquare = true;

    private void CreateRuntimePiece()
    {
        if (_shape == null)
        {
            CreateShape();
        }

        if (_physics == null)
        {
            CreatePhysicsPiece();
        }

        if (_area == null)
        {
            CreateArea();
        }

        if (_holdPoint == null)
        {
            CreateHoldPoint();
        }

        if (_pinJoint2D == null)
        {
            CreatePinJoint();
        }
    }

    private void CreateShape()
    {
        _shape = new RectangleShape2D();
        _shape.Size = new Vector2(_squareSize, _squareSize);
    }


    private void CreatePhysicsPiece()
    {
        _physics = new GodotPhysics();
        _physics.Shape = _shape;
        AddChild(_physics);
        if (_sprite.GetParent() != null)
        {
            _sprite.GetParent().RemoveChild(_sprite);
        }
        _physics.AddChild(_sprite);
    }

    private void UpdatePhysicsPiece()
    {
        if (_physics == null) return;
        _physics.Shape = _shape;
    }

    private void CreateArea()
    {
        _area = new GodotArea();
        _area.Shape = _shape;
        AddChild(_area);
    }

    private void UpdateArea()
    {
        if (_area == null) return;
        _area.Shape = _shape;
    }
    private void CreateHoldPoint()
    {
        _holdPoint = new GodotNodes.Game.Pieces.GodotHoldPoint();
        var circle = new CircleShape2D();
        circle.Radius = 5;
        _holdPoint.Shape = circle;
        _holdPoint.CollisionLayer = 0;
        _holdPoint.CollisionMask = 0;
        AddChild(_holdPoint);
    }

    public void CreatePinJoint()
    {
        if (_holdPoint == null || _physics == null) return;
        _pinJoint2D = new PinJoint2D();
        AddChild(_pinJoint2D);
        _pinJoint2D.UpdateSoftness(_squareSize);
        _pinJoint2D.NodeA = _holdPoint.GetPath();
        _pinJoint2D.NodeB = _physics.GetPath();
        _pinJoint2D.Position = _holdPoint.Position;
        if (_pinJoint2D.NodeA == null) GD.Print("NodeA missing");
        if (_pinJoint2D.NodeB == null) GD.Print("NodeB missing");
    }

    public void PickUpPiece()
    {
        if (!_isMouseEntered) return;
        _isBeingMoved = true;
        _isOnASquare = false;
        _physics.PickedUpPiece();
    }

    private GodotPieceManager _pieceManager;

    private void GetPieceManager()
    {
        _pieceManager = GetParent<GodotPieceManager>();
    }
    public void DropPiece()
    {
        if (_isOnASquare) return;
        _isBeingMoved = false;
        _physics.DroppedPiece();
        _file = FindClosestSquare().File;
        _rank = FindClosestSquare().Rank;
        this.UpdateChessPosition(_pieceManager.ChessPosition);
    }

    private Square FindClosestSquare() =>
        GetGlobalMousePosition().GetSquare(_squareSize);


    public void SetMouseEntered(bool isEntered)
    {
        _isMouseEntered = isEntered;
    }

    private void EditorOnReady()
    {
        GetPieceManager();
        UpdatePiece();
    }
    private void GameOnReady()
    {
        CreateRuntimePiece();
        AddToGroup(CallGroups.LeftClick);
    }

    public override void _Ready()
    {
        if (Engine.IsEditorHint())
        {
            EditorOnReady();
        }
        else
        {
            EditorOnReady();
            GameOnReady();
        }
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
                var targetLocation = new Square(_file, _rank).LocationVector(_squareSize);
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
