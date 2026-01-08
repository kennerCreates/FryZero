using System;
using System.Collections.Generic;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.gameplay;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.GodotNodes.NodeModels;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.Gameplay.Pieces;

[GlobalClass]

public partial class GodotPieceManager : RootNode
{
    public ChessPosition ChessPosition { get; } = new();

    private void DestroyExistingPieceNodes()
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
            AddChild(piece);
        }
    }

    // private void SpawnNewPieceButtonNodes()
    // {
    //     foreach (var color in Enum.GetValues<PieceColor>())
    //     {
    //         foreach (var type in Enum.GetValues<PieceType>())
    //         {
    //             var button = ButtonLocations.CreateNewPieceButton(color: color, type: type, squareSize: SquareSize);
    //             button.Style = Style;
    //             AddChild(button);
    //         }
    //     }
    // }
    //
    // private GodotPiece _pieceBeingSpawned;
    // public void SpawnActualGodotPiece(PieceType type, PieceColor color, Vector2 location)
    // {
    //     _pieceBeingSpawned = PositionManagement.CreateOneHeldPiece(type, color);
    //     SetPieceVisuals(_pieceBeingSpawned);
    //     ConfigurePieceProperties(_pieceBeingSpawned);
    //     _pieceBeingSpawned.SetToPickedUp();
    //     _pieceBeingSpawned.Position = location;
    //     AddChild(_pieceBeingSpawned);
    // }
    //
    // public void UpdatePieceBeingSpawned()
    // {
    //     _pieceBeingSpawned.HandlePieceOnBoardOrNot();
    // }

    protected override void OnReady()
    {
        PositionManagement.InitializeEmptyBoard(ChessPosition);
        PositionManagement.CreateStartingChessPosition(ChessPosition);
        DestroyExistingPieceNodes();
        SpawnPieceNodes(ChessPosition);
        PositionManagement.UpdatePiecesFileAndRankToCurrentPosition(ChessPosition);
        BuildAtlasCache();
    }

    private static Dictionary<(PieceType type, PieceColor color, InteractState state), AtlasTexture> _atlasCache;

    private static void BuildAtlasCache()
    {
        _atlasCache = new Dictionary<(PieceType type, PieceColor color, InteractState state), AtlasTexture>();
        foreach (var type in Enum.GetValues<PieceType>())
        {
            foreach (var color in Enum.GetValues<PieceColor>())
            {
                foreach (var state in Enum.GetValues<InteractState>())
                {
                    var atlas = CreateAtlasTexture(type, color, state);
                    _atlasCache[(type, color, state)] = atlas;
                }
            }
        }
    }

    public static AtlasTexture GetPieceTexture(PieceType type, PieceColor color, InteractState state) => _atlasCache[( type, color, state)];

    private static AtlasTexture CreateAtlasTexture(PieceType type, PieceColor color, InteractState state)
    {
        var column = (int)type;
        var row = color switch
        {
            PieceColor.White => state == InteractState.Normal ? 0 : 1,
            PieceColor.Black => state == InteractState.Normal ? 2 : 3,
            _ => 0
        };
        var atlas = new AtlasTexture();
        atlas.Atlas = GameTheme.GetPieceAtlasTexture();
        atlas.Region = new Rect2(column * GameTheme.GetPieceSize(), row * GameTheme.GetPieceSize(), GameTheme.GetPieceSize(), GameTheme.GetPieceSize());
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
