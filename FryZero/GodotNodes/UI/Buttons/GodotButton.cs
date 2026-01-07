using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay;
using FryZeroGodot.GodotInterface.Extensions;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.GodotNodes.Game.Pieces;
using Godot;
using GodotPieceManager = FryZeroGodot.GodotNodes.Gameplay.Pieces.GodotPieceManager;

namespace FryZeroGodot.GodotNodes.UI.Buttons;

[GlobalClass]

public partial class GodotButton : Node2D
{
    [Export] public int SquareSize { get; set; } = 160;
    [Export] public PieceStyle Style { get; set; }
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    private Sprite2D _sprite;
    private ShaderMaterial _material;

    private void SetSpriteImage()
    {
        _sprite.Texture = _pieceManager.AtlasCache[(Color, Type, InteractState.Normal)];
    }

    private void UpdateSprite()
    {
        SetSpriteImage();
        // _sprite.Material = _pieceManager.ColorScheme.GetThemeMaterial();
        var spriteSize = _sprite.Texture.GetSize();
        _sprite.Scale = new Vector2(SquareSize, SquareSize) / spriteSize;
    }

    private void UpdateLocation()
    {

        Position = ButtonLocations.GetNewPieceButtonLocation(Color, Type, SquareSize);
    }

    private void CreateSprite()
    {
        _sprite = new Sprite2D();
        AddChild(_sprite);
    }

    private void UpdateButton()
    {
        if (_sprite is null) CreateSprite();
        UpdateSprite();
        UpdateLocation();
        _shape?.WithUpdatedShape(SquareSize);
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
        _shape.WithUpdatedShape(SquareSize);
    }


    private void CreateArea()
    {
        _pieceArea = new UI.Buttons.GodotButtonArea();
        _pieceArea?.WithUpdatedNewPieceButtonArea(_shape);
        AddChild(_pieceArea);
    }

    private GodotPieceManager _pieceManager;


    private bool _leftClickDown = false;
    public void LeftClickDown()
    {
        if (!_isMouseEntered) return;
        _isBeingMoved = true;
        _pieceManager.SpawnActualGodotPiece(Type,Color,Position);
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

    private void OnPieceManagerReady()
    {
        UpdateButton();
        CreateRuntimePiece();
        AddToGroup(CallGroups.LeftClick);
    }


}
