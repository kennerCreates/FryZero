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

    [Export] public Shape2D Shape;

    private Sprite2D _sprite;
    private PhysicsPiece _physics;
    private HoldPoint _holdPoint;
    private PieceArea _pieceArea;
    private PinJoint2D _pinJoint;



    private void SetSpriteImage()
    {
        _sprite.Texture = GD.Load<Texture2D>($"res://Assets/Pieces/{Style}/{Color}/{Type}.svg");
    }
    private void UpdateSprite()
    {
        SetSpriteImage();
        var spriteSize = _sprite.Texture.GetSize();
        _sprite.Scale = new Vector2(SquareSize, SquareSize) / spriteSize;
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
    }
    private void UpdatePiece()
    {
        if (_sprite == null) CreateSprite();
        UpdateSprite();
        UpdateLocation();
    }

    private void EditorOnReady()
    {
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
