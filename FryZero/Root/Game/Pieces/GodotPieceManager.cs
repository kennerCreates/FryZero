using System.Net.NetworkInformation;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.Root.Game.Board;

[Tool]

[GlobalClass]

public partial class GodotPieceManager : Node2D
{
    [Export] public int SquareSize;
    [Export] public PieceStyle Style;

    private Piece CreatePiece(PieceType pieceType, PieceColor pieceColor, PieceStyle pieceStyle)
    {
        var piece = new Piece();
        UpdatePiece(piece, pieceType, pieceColor, pieceStyle);
        return piece;
    }

    private Piece UpdatePiece(Piece piece, PieceType pieceType, PieceColor pieceColor, PieceStyle pieceStyle)
    {
        piece.Type = pieceType;
        piece.Color = pieceColor;
        piece.Style = pieceStyle;
        piece.SquareSize = SquareSize;
        return piece;
    }
    private void EditorOnReady()
    {
        AddChild(CreatePiece(PieceType.Rook, PieceColor.Black, Style));
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
