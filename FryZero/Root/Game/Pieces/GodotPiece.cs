using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Structs;
using FryZeroGodot.gameplay;
using Godot;

namespace FryZeroGodot.Root.Game.Pieces;

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
    private Shader _shader = GD.Load<Shader>("res://Root/Visuals/HueShift.gdshader");
    private ShaderMaterial _material;

    private void CreateShader()
    {
        _material = new ShaderMaterial();
        _material.Shader = _shader;
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

    private void UpdateLocation()
    {
        var square = new Square(_file, _rank);
        Position = square.LocationVector(_squareSize);
    }

    private void CreateSprite()
    {
        _sprite = new Sprite2D();
        AddChild(_sprite);
        CreateShader();
    }

    private void UpdatePiece()
    {
        if (_sprite == null) CreateSprite();
        UpdateSprite();
        UpdateLocation();
        if (Engine.IsEditorHint()) return;
        UpdateShape();
        UpdatePhysicsPiece();
        UpdateArea();
        UpdateHoldPoint();
    }

    private RectangleShape2D _shape;
    private GodotPhysics _physics;
    private GodotHoldPoint _holdPoint;
    private GodotArea _area;
    private PinJoint2D _pinJoint;
    private bool _isMouseEntered;
    private bool _isBeingMoved;

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

        if (_pinJoint == null)
        {
            CreatePinJoint();
        }
    }

    private void CreateShape()
    {
        _shape = new RectangleShape2D();
        _shape.Size = new Vector2(_squareSize, _squareSize);
    }
    private void UpdateShape()
    {
        if (_shape == null) return;
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
        _holdPoint = new GodotHoldPoint();
        var circle = new CircleShape2D();
        circle.Radius = 5;
        _holdPoint.Shape = circle;
        _holdPoint.CollisionLayer = 0;
        _holdPoint.CollisionMask = 0;
        _holdPoint.Position = Position + new Vector2(0, -(_squareSize / 4));
        AddChild(_holdPoint);
    }

    private void UpdateHoldPoint()
    {
        if (_holdPoint == null) return;
        _holdPoint.Position = Position + new Vector2(0, -(_squareSize / 4));
    }

    private void CreatePinJoint()
    {
        if (_holdPoint == null || _physics == null) return;
        _pinJoint = new PinJoint2D();
        AddChild(_pinJoint);
        _pinJoint.Softness = 1;
        _pinJoint.NodeA = _holdPoint.GetPath();
        _pinJoint.NodeB = _physics.GetPath();
        _pinJoint.Position = (_physics.Position + _holdPoint.Position) / 2;
        _pinJoint.AngularLimitEnabled = true;
        _pinJoint.AngularLimitLower = -Mathf.Pi / 4;
        _pinJoint.AngularLimitUpper = Mathf.Pi / 4;
        if (_pinJoint.NodeA == null) GD.Print("NodeA missing");
        if (_pinJoint.NodeB == null) GD.Print("NodeB missing");
    }

    public void PickUpPiece()
    {
        if (!_isMouseEntered) return;
        _isBeingMoved = true;
        _physics.PickedUpPiece();
    }

    public void DropPiece()
    {
        if (!_isMouseEntered) return;
        _isBeingMoved = false;
        _physics.DroppedPiece();
    }

    public void SetMouseEntered(bool isEntered)
    {
        _isMouseEntered = isEntered;
    }

    private void EditorOnReady()
    {
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
        if (!_isBeingMoved) return;
        var tween = GetTree().CreateTween();
        tween.TweenProperty(this, "position", GetGlobalMousePosition(), 10 * delta);
    }
}
