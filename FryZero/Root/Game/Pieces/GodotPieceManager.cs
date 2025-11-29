using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.Root.Game.Pieces;

[Tool]

[GlobalClass]

public partial class GodotPieceManager : Node2D
{
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

    private void UpdateAllPieces()
    {

    }
    private void CreateAllPieces()
    {
        CreateOnePiece(PieceType.King, PieceColor.White, Rank.Two, File.E);
    }

    private void CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file)
    {
        var piece = new GodotPiece();
        AddChild(piece);
        piece.SquareSize = _size;
        piece.Style = _style;
        piece.Type = type;
        piece.Color = color;
        piece.Rank = rank;
        piece.File = file;
    }

    private void EditorOnReady()
    {
        CreateAllPieces();
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
