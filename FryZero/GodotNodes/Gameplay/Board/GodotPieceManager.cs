using System;
using System.Collections.Generic;
using System.Linq;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.GodotNodes.Gameplay.Pieces;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.Gameplay.Board;

[GlobalClass]

public partial class GodotPieceManager : Node2D
{
    private static ChessPosition _position;
    
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

    public override void _Ready()
    {
        DestroyExistingPieceNodes();
        SetupPositionWithEmptyBoard();
        SetupPiecesInStartingChessPosition();
        SpawnPieceNodes();
        BuildAtlasCache();
    }

    private static void SetupPositionWithEmptyBoard()
    {
        _position ??= new ChessPosition();
        _position.Squares.Clear();
        foreach (var rank in Enum.GetValues<Rank>())
        {
            foreach (var file in Enum.GetValues<File>())
            {
                _position.Squares.Add(new Square (file, rank));
            }
        }
    }

    private static void SetupPiecesInStartingChessPosition()
    {
        // White Back Rank
        SetPieceInPosition(CreateOnePiece(PieceType.Rook, PieceColor.White, Rank.One, File.A));
        SetPieceInPosition(CreateOnePiece(PieceType.Knight, PieceColor.White, Rank.One, File.B));
        SetPieceInPosition(CreateOnePiece(PieceType.Bishop, PieceColor.White, Rank.One, File.C));
        SetPieceInPosition(CreateOnePiece(PieceType.Queen, PieceColor.White, Rank.One, File.D));
        SetPieceInPosition(CreateOnePiece(PieceType.King, PieceColor.White, Rank.One, File.E));
        SetPieceInPosition(CreateOnePiece(PieceType.Bishop, PieceColor.White, Rank.One, File.F));
        SetPieceInPosition(CreateOnePiece(PieceType.Knight, PieceColor.White, Rank.One, File.G));
        SetPieceInPosition(CreateOnePiece(PieceType.Rook, PieceColor.White, Rank.One, File.H));
        // White Pawns
        foreach (var file in Enum.GetValues<File>())
        {
            SetPieceInPosition(CreateOnePiece(PieceType.Pawn, PieceColor.White, Rank.Two, file));
        }
        // Black Back Rank
        SetPieceInPosition(CreateOnePiece(PieceType.Rook, PieceColor.Black, Rank.Eight, File.A));
        SetPieceInPosition(CreateOnePiece(PieceType.Knight, PieceColor.Black, Rank.Eight, File.B));
        SetPieceInPosition(CreateOnePiece(PieceType.Bishop, PieceColor.Black, Rank.Eight, File.C));
        SetPieceInPosition(CreateOnePiece(PieceType.Queen, PieceColor.Black, Rank.Eight, File.D));
        SetPieceInPosition(CreateOnePiece(PieceType.King, PieceColor.Black, Rank.Eight, File.E));
        SetPieceInPosition(CreateOnePiece(PieceType.Bishop, PieceColor.Black, Rank.Eight, File.F));
        SetPieceInPosition(CreateOnePiece(PieceType.Knight, PieceColor.Black, Rank.Eight, File.G));
        SetPieceInPosition(CreateOnePiece(PieceType.Rook, PieceColor.Black, Rank.Eight, File.H));
        // Black Pawns
        foreach (var file in Enum.GetValues<File>())
        {
            SetPieceInPosition(CreateOnePiece(PieceType.Pawn, PieceColor.Black, Rank.Seven, file));
        }
    }

    private static void SetPieceInPosition(GodotPiece piece)
    {
        var square = _position.Squares.Single(s => s.File == piece.File && s.Rank == piece.Rank);
        square.Piece = piece;
    }

    private static GodotPiece CreateOnePiece(PieceType type, PieceColor color, Rank rank, File file)
    {
        var piece = new GodotPiece();
        piece.Type = type;
        piece.Color = color;
        piece.Rank = rank;
        piece.File = file;
        return piece;
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
            AddChild(piece);
        }
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

    public static AtlasTexture GetPieceTexture(PieceType type, PieceColor color, InteractState state)
    {
        if (_atlasCache is null)
        {
            BuildAtlasCache();
        }
        return _atlasCache?[(type, color, state)];
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
        atlas.Atlas = GameTheme.GetPieceAtlasTexture();
        atlas.Region = new Rect2(column * GameTheme.GetPieceSize(), row * GameTheme.GetPieceSize(), GameTheme.GetPieceSize(), GameTheme.GetPieceSize());
        return atlas;
    }

    public static void UpdateChessPosition(GodotPiece piece)
    {
        var newSquare = _position.Squares.SingleOrDefault(s => s.File == piece.File && s.Rank == piece.Rank);
        var oldSquare = _position.Squares.SingleOrDefault(s => s.Piece == piece);
        if (oldSquare != null) oldSquare.Piece = null;
        if (newSquare == null) return;
        var currentPiece = newSquare.Piece;
        GD.Print(piece.Type + ": " + oldSquare?.File + "" + oldSquare?.Rank + " " + newSquare.File + "" +
                 newSquare.Rank);
        currentPiece?.QueueFree();
        newSquare.Piece = piece;
    }

    public static void RemovePieceFromBoard(GodotPiece piece)
    {
        var square = _position.Squares.SingleOrDefault(s => s.Piece == piece);
        if (square != null) square.Piece = null;
        GD.Print("Piece dropped off board");
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
