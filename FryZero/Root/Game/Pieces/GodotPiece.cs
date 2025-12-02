using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Structs;
using FryZeroGodot.gameplay;
using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.Root.Game.Pieces;

[Tool]

[GlobalClass]

public partial class GodotPiece : Node2D
{
    [Export] public int SquareSize
    {
        get => _size;
        set
        {
            _size = value;
            UpdatePiece();
        }
    }
    private int _size = 160;

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
    private PieceStyle _style;

    [Export] public PieceType Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdatePiece();
        }
    }
    private PieceType _type;

    [Export] public PieceColor Color
    {
        get => _color;
        set
        {
            _color = value;
            UpdatePiece();
        }
    }
    private PieceColor _color;

    [Export] public Rank Rank
    {
        get => _rank;
        set
        {
            _rank = value;
            UpdatePiece();
        }
    }
    private Rank _rank;

    [Export] public File File
    {
        get => _file;
        set
        {
            _file = value;
            UpdatePiece();
        }
    }
    private File _file;
    [Export] public Color LightPieceColor
    {
        get => _lightPieceColor;
        set
        {
            _lightPieceColor = value;
            UpdatePiece();
        }
    }
    private Color _lightPieceColor;

    [Export] public Color DarkPieceColor
    {
        get => _darkPieceColor;
        set
        {
            _darkPieceColor = value;
            UpdatePiece();
        }
    }
    private Color _darkPieceColor;

    [Export] public Shape2D Shape;

    private Sprite2D _sprite;
    private PhysicsPiece _physics;
    private HoldPoint _holdPoint;
    private PieceArea _pieceArea;
    private PinJoint2D _pinJoint;

    private Shader _shader = GD.Load<Shader>("res://Root/Visuals/HueShiftShadowsHighlights.gdshader");
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
        var outlineColor = new Color(_color == PieceColor.Black ? _lightPieceColor : _darkPieceColor);
        _material.SetShaderParameter("main_color", spriteColor);
        _material.SetShaderParameter("outline_color", outlineColor);
    }

    private void UpdateLocation()
    {
        var square = new Square(_file, _rank);
        Position = square.LocationVector(_size);
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
    }

    private void EditorOnReady()
    {
        SetZIndex(5);
        UpdatePiece();
    }


    private void GameOnReady()
    {

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
}
