using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay;
using FryZeroGodot.Godot.EngineFiles;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotNodes.Game.Pieces;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.Buttons;

[GlobalClass]

public partial class GodotButton : Node2D
{
    private int _squareSize = 160;
    private PieceStyle _style;
    private PieceType _type;
    private PieceColor _color;
    [Export] public int SquareSize
    {
        get => _squareSize;
        set
        {
            _squareSize = value;
            UpdateButton();
        }
    }
    [Export]
    public PieceStyle Style
    {
        get => _style;
        set
        {
            _style = value;
            UpdateButton();
        }
    }
    [Export] public PieceType Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdateButton();
        }
    }
    [Export] public PieceColor Color
    {
        get => _color;
        set
        {
            _color = value;
            UpdateButton();
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
    }



    private void UpdateLocation()
    {

        Position = ButtonLocations.GetNewPieceButtonLocation(_color, _type, _squareSize);
    }

    private void CreateSprite()
    {
        _sprite = new Sprite2D();
        AddChild(_sprite);
        CreateHueShiftShader();
    }

    private void UpdateButton()
    {
        if (_sprite is null) CreateSprite();
        UpdateSprite();
        UpdateLocation();
        _shape?.WithUpdatedShape(_squareSize);
        _pieceArea?.WithUpdatedNewPieceButtonArea(_shape);
    }

    private RectangleShape2D _shape;

    private GodotButtonArea _pieceArea;

    private bool _isMouseEntered;
    private bool _isBeingMoved;

    private void CreateRuntimePiece()
    {
        if (_shape is null)
        {
            CreateShape();
        }
        if (_pieceArea is null)
        {
            CreateArea();
        }
    }

    private void CreateShape()
    {
        _shape = new RectangleShape2D();
        _shape.WithUpdatedShape(_squareSize);
    }


    private void CreateArea()
    {
        _pieceArea = new UI.Buttons.GodotButtonArea();
        _pieceArea?.WithUpdatedNewPieceButtonArea(_shape);
        AddChild(_pieceArea);
    }

    private GodotPieceManager _pieceManager;

    private void GetPieceManager()
    {
        _pieceManager = GetParent<GodotPieceManager>();
    }

    private bool _leftClickDown = false;
    public void LeftClickDown()
    {
        if (!_isMouseEntered) return;
        _isBeingMoved = true;
        _pieceManager.SpawnActualGodotPiece(_type,_color,Position);
        _leftClickDown = true;
    }

    public void LeftClickReleased()
    {
        if (!_leftClickDown) return;
        _pieceManager.UpdatePieceBeingSpawned();
        _leftClickDown = false;

    }
    public void SetMouseEntered(bool isEntered)
    {
        _isMouseEntered = isEntered;
    }

    private void EditorOnReady()
    {
        GetPieceManager();
        UpdateButton();
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

}
