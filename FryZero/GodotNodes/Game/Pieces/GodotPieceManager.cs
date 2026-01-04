using System;
using System.Collections.Generic;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.Godot.EngineFiles;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.Game.Pieces;

[GlobalClass]

public partial class GodotPieceManager : Node2D
{
    [Signal]
    public delegate void PieceManagerInitializedEventHandler();
    public bool IsInitialized { get; private set; }
    [Export] public int PieceMovementDelay { get; set; } = 10;
    [Export] public int SquareSize { get; set; }= 160;
    [Export] public PieceStyle Style { get; set; }

    [Export] public Texture2D PieceAtlasTexture { get; set; }

    [Export] public int AtlasSize{ get; set; } = 32;
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
        piece.Style = Style;
        piece.SquareSize = SquareSize;
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
        piece.SquareSize = SquareSize;
        piece.Style = Style;
        piece.ZIndex = 9;
        piece.MovementDelay = PieceMovementDelay;
    }

    private void SpawnNewPieceButtonNodes()
    {
        foreach (var color in Enum.GetValues<PieceColor>())
        {
            foreach (var type in Enum.GetValues<PieceType>())
            {
                var button = ButtonLocations.CreateNewPieceButton(color: color, type: type, squareSize: SquareSize);
                button.Style = Style;
                AddChild(button);
            }
        }
    }

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
    public GodotColorScheme ColorScheme;


    public override void _EnterTree()
    {
        ColorScheme = GetParent<GodotColorScheme>();
        if (ColorScheme.IsInitialized)
        {
            OnColorSchemeReady();
        }
        else
        {
            ColorScheme.ColorSchemeInitialized += OnColorSchemeReady;
        }
    }

    private void OnColorSchemeReady()
    {
        PositionManagement.InitializeEmptyBoard(ChessPosition);
        PositionManagement.CreatePiecesInStartingPosition(ChessPosition);
        DestroyExistingPieces();
        SpawnPieceNodes(ChessPosition);
        SpawnNewPieceButtonNodes();
        BuildAtlasCache();
        IsInitialized = true;
        EmitSignal(SignalName.PieceManagerInitialized);
    }

    public Dictionary<(PieceColor color, PieceType type, PieceState state), AtlasTexture> AtlasCache;

    private void BuildAtlasCache()
    {
        AtlasCache = new();
        foreach (PieceType type in Enum.GetValues<PieceType>())
        {
            foreach (PieceColor color in Enum.GetValues<PieceColor>())
            {
                foreach (PieceState state in Enum.GetValues<PieceState>())
                {
                    var atlas = CreateAtlasTexture(type, color, state);
                    AtlasCache[(color, type, state)] = atlas;
                }
            }
        }
    }

    private AtlasTexture CreateAtlasTexture(PieceType type, PieceColor color, PieceState state)
    {
        var column = (int)type;

        var row = color switch
        {
            PieceColor.White => state == PieceState.Normal ? 0 : 1,
            PieceColor.Black => state == PieceState.Normal ? 2 : 3,
            _ => 0
        };

        var atlas = new AtlasTexture();
        atlas.Atlas = PieceAtlasTexture;
        atlas.Region = new Rect2(column * AtlasSize, row * AtlasSize, AtlasSize, AtlasSize);
        return atlas;
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
