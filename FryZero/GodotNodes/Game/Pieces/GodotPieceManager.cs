using System;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.Godot.EngineFiles;
using Godot;

namespace FryZeroGodot.GodotNodes.Game.Pieces;

[Tool]

[GlobalClass]

public partial class GodotPieceManager : Node2D
{
    [Export]public int PieceMovementDelay { get; set; } = 10;
    [Export]
    public int SquareSize
    {
        get => _size;
        set
        {
            _size = value;
            UpdateAllPieces();
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
            UpdateAllPieces();
        }
    }
    private PieceStyle _style;

    [Export]
    public Color LightPieceColor
    {
        get => _lightPieceColor;
        set
        {
            _lightPieceColor = value;
            UpdateAllPieces();
        }
    }
    private Color _lightPieceColor = Colors.White;
    private Color _lightPieceOutlineColor = Colors.Black;
    [Export]
    public Color LightPieceOutlineColor
    {
        get => _lightPieceOutlineColor;
        set
        {
            _lightPieceOutlineColor = value;
            UpdateAllPieces();
        }
    }

    [Export]
    public Color DarkPieceColor
    {
        get => _darkPieceColor;
        set
        {
            _darkPieceColor = value;
            UpdateAllPieces();
        }
    }
    private Color _darkPieceColor = Colors.White;
    private Color _darkPieceOutlineColor = Colors.White;
    [Export]
    public Color DarkPieceOutlineColor
    {
        get => _darkPieceOutlineColor;
        set
        {
            _darkPieceOutlineColor = value;
            UpdateAllPieces();
        }
    }

    public ChessPosition ChessPosition { get; } = new();

    private void UpdateAllPieces()
    {
        PositionManagement.UpdatePieceNodes(ChessPosition);
        var children = GetChildren();
        foreach (var child in children)
        {
            if (child is not GodotPiece piece) continue;
            SetPieceVisuals(piece);
        }
    }

    private void SetPieceVisuals(GodotPiece piece)
    {
        piece.Style = _style;
        piece.DarkPieceColor = _darkPieceColor;
        piece.LightPieceColor = _lightPieceColor;
        piece.DarkPieceOutlineColor = _darkPieceOutlineColor;
        piece.LightPieceOutlineColor = _lightPieceOutlineColor;
        piece.SquareSize = _size;
    }

    private void DestroyExistingPieces()
    {
        var children = GetChildren().OfType<GodotPiece>();
        foreach (var child in children)
        {
            child.QueueFree();
        }
    }

    private void SpawnPieceNodes(ChessPosition position)
    {
        foreach (var piece in position.Squares.Select(square => square.Piece).Where(piece => piece is not null))
        {
            ConfigurePieceProperties(piece);
            AddChild(piece);
        }
    }

    private void ConfigurePieceProperties(GodotPiece piece)
    {
        piece.SquareSize = _size;
        piece.Style = _style;
        piece.ZIndex = 9;
        piece.MovementDelay = PieceMovementDelay;
    }

    private void SpawnNewPieceButtonNodes()
    {
        foreach (var color in Enum.GetValues<PieceColor>())
        {
            foreach (var type in Enum.GetValues<PieceType>())
            {
                var button = ButtonLocations.CreateNewPieceButton(color: color, type: type, squareSize: _size);
                button.Style = _style;
                button.LightPieceColor = _lightPieceColor;
                button.DarkPieceColor = _darkPieceColor;
                button.LightPieceOutlineColor = _lightPieceOutlineColor;
                button.DarkPieceOutlineColor = _darkPieceOutlineColor;
                AddChild(button);
            }
        }
    }
    private Square FindClosestSquareLocation() =>
        GetGlobalMousePosition().GetSquare(_size);

    private GodotPiece _pieceBeingSpawned;
    public void SpawnActualGodotPiece(PieceType type, PieceColor color, Vector2 location)
    {
        _pieceBeingSpawned = PositionManagement.CreateOneHeldPiece(type, color);
        SetPieceVisuals(_pieceBeingSpawned);
        ConfigurePieceProperties(_pieceBeingSpawned);
        _pieceBeingSpawned.SetToPickedUp();
        _pieceBeingSpawned.Position = location;
        AddChild(_pieceBeingSpawned);
    }

    public void UpdatePieceBeingSpawned()
    {
        _pieceBeingSpawned.HandlePieceOnBoardOrNot();
    }



    private void GameOnReady()
    {
        //CreateHueShiftShader();
        PositionManagement.InitializeEmptyBoard(ChessPosition);
        PositionManagement.CreatePiecesInStartingPosition(ChessPosition);
        DestroyExistingPieces();
        SpawnPieceNodes(ChessPosition);
        SpawnNewPieceButtonNodes();
    }

    public override void _Ready()
    {
        GameOnReady();
    }

    private void PickUpOrDropPiece(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseButtonEvent) return;

        var isLeftMouseButtonEvent = mouseButtonEvent.ButtonIndex is MouseButton.Left;
        if (!isLeftMouseButtonEvent) return;

        var method = mouseButtonEvent.Pressed ? nameof(GodotPiece.LeftClickDown) : nameof(GodotPiece.LeftClickReleased);
        GetTree().CallGroup(CallGroups.LeftClick, method);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        PickUpOrDropPiece(@event);
    }
}
