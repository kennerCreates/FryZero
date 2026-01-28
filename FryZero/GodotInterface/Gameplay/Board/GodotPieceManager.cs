using System;
using System.Collections.Generic;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using FryZeroGodot.Statics.Gameplay.Board;
using Godot;
using GameTheme = FryZeroGodot.GodotInterface.UI.GameTheme.GameTheme;
using GodotPiece = FryZeroGodot.GodotInterface.Gameplay.Pieces.GodotPiece;

namespace FryZeroGodot.GodotInterface.Gameplay.Board;

[GlobalClass]

public partial class GodotPieceManager : Node2D
{
    private ChessPosition _position = new();

    public override void _Ready()
    {
        DestroyExistingPieceNodes();
        var pieceFactory = new GodotPieceFactory();
        _position = _position.SetupPositionWithEmptyBoard();
        _position = _position.SetupPiecesInStartingChessPosition(pieceFactory);
        SpawnPieceNodes();
    }
    private void DestroyExistingPieceNodes()
    {
        var children = GetChildren().OfType<GodotPiece>();
        foreach (var child in children)
        {
            child.QueueFree();
        }
    }

    private void SpawnPieceNodes()
    {
        foreach (var piece in _position.Squares.Select(square => square.Piece).Where(piece => piece is not null))
        {
            AddChild((GodotPiece)piece);
        }
    }

    public void UpdateChessPosition(GodotPiece piece)
    {
        _position = _position.UpdateChessPosition(piece);
    }

    public void RemovePieceFromBoard(GodotPiece piece)
    {
        _position = _position.RemovePieceFromBoard(piece);
    }

}
