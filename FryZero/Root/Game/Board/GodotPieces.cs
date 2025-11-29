using System.Net.NetworkInformation;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.Root.Game.Board;

[Tool]

[GlobalClass]

public partial class GodotPieces : Node2D
{
    [Export] public int SquareSize;

    private Piece CreatePiece(PieceType pieceType, PieceColor pieceColor, PieceStyle pieceStyle)
    {
        var piece = new Piece();
        piece.Type = pieceType;
        piece.Color = pieceColor;
        piece.Style = pieceStyle;
        var shape = new RectangleShape2D();
        shape.Size = new Vector2(SquareSize, SquareSize);
        piece.Shape = shape;
        return piece;
    }
    private void EditorOnReady()
    {
        AddChild(CreatePiece(PieceType.Rook, PieceColor.Black, PieceStyle.Gioco));
    }

    private void GameOnReady()
    {

    }

    public override void _EnterTree()
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
