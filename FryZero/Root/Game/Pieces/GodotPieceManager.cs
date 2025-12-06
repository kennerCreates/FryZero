using FryZeroGodot.Config.Enums;
using FryZeroGodot.gameplay;
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
    private void CreateAllPiecesInStartingPosition()
    {
        DestroyExistingPieces();
        CreateOnePiece(PieceType.Rook, PieceColor.White, Rank.One, File.A);
        CreateOnePiece(PieceType.Knight, PieceColor.White, Rank.One, File.B);
        CreateOnePiece(PieceType.Bishop, PieceColor.White, Rank.One, File.C);
        CreateOnePiece(PieceType.Queen, PieceColor.White, Rank.One, File.D);
        CreateOnePiece(PieceType.King, PieceColor.White, Rank.One, File.E);
        CreateOnePiece(PieceType.Bishop, PieceColor.White, Rank.One, File.F);
        CreateOnePiece(PieceType.Knight, PieceColor.White, Rank.One, File.G);
        CreateOnePiece(PieceType.Rook, PieceColor.White, Rank.One, File.H);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.A);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.B);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.C);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.D);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.E);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.F);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.G);
        CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, File.H);
        CreateOnePiece(PieceType.Rook, PieceColor.Black, Rank.Eight, File.A);
        CreateOnePiece(PieceType.Knight, PieceColor.Black, Rank.Eight, File.B);
        CreateOnePiece(PieceType.Bishop, PieceColor.Black, Rank.Eight, File.C);
        CreateOnePiece(PieceType.Queen, PieceColor.Black, Rank.Eight, File.D);
        CreateOnePiece(PieceType.King, PieceColor.Black, Rank.Eight, File.E);
        CreateOnePiece(PieceType.Bishop, PieceColor.Black, Rank.Eight, File.F);
        CreateOnePiece(PieceType.Knight, PieceColor.Black, Rank.Eight, File.G);
        CreateOnePiece(PieceType.Rook, PieceColor.Black, Rank.Eight, File.H);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.A);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.B);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.C);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.D);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.E);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.F);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.G);
        CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, File.H);
    }

    private void CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file)
    {
        var piece = new GodotPiece();
        piece.SquareSize = _size;
        piece.Style = _style;
        piece.Type = type;
        piece.Color = color;
        piece.Rank = rank;
        piece.File = file;
        piece.ZIndex = 9;
        AddChild(piece);
    }

    private void EditorOnReady()
    {
        CreateAllPiecesInStartingPosition();
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

        var method = mouseButtonEvent.Pressed ? nameof(GodotPiece.PickUpPiece) : nameof(GodotPiece.DropPiece);
        GetTree().CallGroup(CallGroups.LeftClick, method);
    }

    public override void _Input(InputEvent @event)
    {
        PickUpOrDropPiece(@event);
    }

}
