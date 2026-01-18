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
        BuildAtlasCache();
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

    private Dictionary<(PieceType type, PieceColor color, InteractState state), AtlasTexture> _atlasCache;

    private void BuildAtlasCache()
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
        atlas.Atlas = GameTheme.Instance.GetPieceAtlasTexture();
        atlas.Region = new Rect2(column * GameTheme.Instance.GetPieceSize(), row * GameTheme.Instance.GetPieceSize(), GameTheme.Instance.GetPieceSize(), GameTheme.Instance.GetPieceSize());
        return atlas;
    }

    public AtlasTexture GetPieceTexture(PieceType type, PieceColor color, InteractState state)
    {
        if (_atlasCache is null)
        {
            BuildAtlasCache();
        }
        return _atlasCache?[(type, color, state)];
    }


}
