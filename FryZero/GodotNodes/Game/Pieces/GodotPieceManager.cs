using System;
using System.Collections.Generic;
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

    private new ChessPosition Position { get; set; } = new();

    private void UpdateAllPieces()
    {
        var children = GetChildren();
        foreach (var child in children)
        {
            if (child is not GodotPiece piece) continue;
            piece.Style = _style;
            piece.DarkPieceColor = _darkPieceColor;
            piece.LightPieceColor = _lightPieceColor;
            piece.DarkPieceOutlineColor = _darkPieceOutlineColor;
            piece.LightPieceOutlineColor = _lightPieceOutlineColor;
            piece.SquareSize = _size;
        }
    }

    private void DestroyExistingPieces()
    {
        var children = GetChildren();
        foreach (var child in children)
        {
            child.QueueFree();
        }
    }

    private void SpawnPieceNodes(ChessPosition position)
    {
        var squares = position.Squares;
        foreach (var piece in squares.Select(square => square.Piece).Where(piece => piece != null))
        {
            piece.SquareSize = _size;
            piece.Style = _style;
            piece.ZIndex = 9;
            piece.MovementDelay = PieceMovementDelay;
            AddChild(piece);
        }
    }

    private void EditorOnReady()
    {
        PieceManager.InitializeEmptyBoard(Position);
        PieceManager.CreatePiecesInStartingPosition(Position);
        DestroyExistingPieces();
        SpawnPieceNodes(Position);
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

    private void PickUpOrDropPiece(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseButtonEvent) return;

        var isLeftMouseButtonEvent = mouseButtonEvent.ButtonIndex is MouseButton.Left;
        if (!isLeftMouseButtonEvent) return;

        var method = mouseButtonEvent.Pressed ? nameof(GodotNodes.Game.Pieces.GodotPiece.PickUpPiece) : nameof(GodotNodes.Game.Pieces.GodotPiece.DropPiece);
        GetTree().CallGroup(CallGroups.LeftClick, method);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        PickUpOrDropPiece(@event);
    }
}
