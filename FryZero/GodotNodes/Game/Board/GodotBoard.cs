using System;
using System.Collections.Generic;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Records;
using Godot;
using Color = Godot.Color;
using GodotPieceManager = FryZeroGodot.GodotNodes.Game.Pieces.GodotPieceManager;
using Vector2 = Godot.Vector2;

namespace FryZeroGodot.GodotNodes.Game.Board;

[Tool]

[GlobalClass]

public partial class GodotBoard : Node2D
{
    [ExportCategory("Board")]
    private int _squareSize = 160;
    private Color _lightSquareColor = Colors.White;
    private Color _darkSquareColor = Colors.Black;
    private Texture2D _lightSquareTexture;
    private Texture2D _darkSquareTexture;
    private Sprite2D _lightSquares;
    private Sprite2D _darkSquares;

    [Export]
    public Color LightSquareColor
    {
        get => _lightSquareColor;
        set
        {
            _lightSquareColor = value;
            UpdateLightSquareColor();
        }
    }
    private void UpdateLightSquareColor()
    {
        if (_lightSquares == null) return;
        _lightSquares.Modulate = _lightSquareColor;
    }
    [Export]
    public Texture2D LightSquareTexture
    {
        get => _lightSquareTexture;
        set
        {
            _lightSquareTexture = value;
            UpdateLightSquareTexture();
        }
    }
    private void UpdateLightSquareTexture()
    {
        if (_lightSquares == null) return;
        _lightSquares.Texture = _lightSquareTexture;
    }
    [Export]
    public Color DarkSquareColor
    {
        get => _darkSquareColor;
        set
        {
            _darkSquareColor = value;
            UpdateDarkSquareColor();
        }
    }
    private void UpdateDarkSquareColor()
    {
        if (_darkSquares == null) return;
        _darkSquares.Modulate = _darkSquareColor;
    }
    [Export]
    public Texture2D DarkSquareTexture
    {
        get => _darkSquareTexture;
        set
        {
            _darkSquareTexture = value;
            UpdateDarkSquareTexture();
        }
    }
    private void UpdateDarkSquareTexture()
    {
        if (_darkSquares == null) return;
        _darkSquares.Texture = _darkSquareTexture;
    }
    private void UpdateSquares()
    {
        if (_lightSquares == null)
        {
            CreateLightSquares();
        }

        if (_darkSquares == null)
        {
            CreateDarkSquares();
        }

        UpdateLightSquareColor();
        UpdateDarkSquareColor();
        UpdateLightSquareTexture();
        UpdateDarkSquareTexture();
        SetSquareScale();
    }
    private void CreateDarkSquares()
    {
        _darkSquares = new Sprite2D();
        _darkSquares.Texture = _darkSquareTexture;
        AddChild(_darkSquares);
    }
    private void CreateLightSquares()
    {
        _lightSquares = new Sprite2D();
        _lightSquares.Texture = _lightSquareTexture;
        AddChild(_lightSquares);
    }
    private void UpdateScreenSize()
    {
        var viewportSize = DisplayServer.WindowGetSize();
        var size = viewportSize.Y / 9;
        _squareSize = Math.Max(size, 32);
    }
    private void SetSquareScale()
    {
        _lightSquares.Scale = new Vector2(_squareSize, _squareSize);
        _darkSquares.Scale = new Vector2(_squareSize, _squareSize);
    }
    private void UpdateSquareSize()
    {
        UpdateScreenSize();
        SetSquareScale();
        UpdatePieceManager();
    }



    [ExportCategory("Pieces")]
    private PieceStyle _pieceStyle = PieceStyle.Tiny;
    private Color _lightPieceColor = Colors.White;
    private Color _lightPieceOutlineColor = Colors.Black;
    private Color _darkPieceColor = Colors.Black;
    private Color _darkPieceOutlineColor = Colors.White;
    private GodotPieceManager _pieceManager;

    [Export]
    public int PieceMovementDelay { get; set; } = 10;

    [Export]
    public PieceStyle PieceStyle
    {
        get => _pieceStyle;
        set
        {
            _pieceStyle = value;
            UpdatePieceManager();
        }
    }
    [Export]
    public Color LightPieceColor
    {
        get => _lightPieceColor;
        set
        {
            _lightPieceColor = value;
            UpdatePieceManager();
        }
    }
    [Export]
    public Color LightPieceOutlineColor
    {
        get => _lightPieceOutlineColor;
        set
        {
            _lightPieceOutlineColor = value;
            UpdatePieceManager();
        }
    }
    [Export] public Color DarkPieceColor
    {
        get => _darkPieceColor;
        set
        {
            _darkPieceColor = value;
            UpdatePieceManager();
        }
    }
    [Export]
    public Color DarkPieceOutlineColor
    {
        get => _darkPieceOutlineColor;
        set
        {
            _darkPieceOutlineColor = value;
            UpdatePieceManager();
        }
    }
    private void CreatePieceManager()
    {
        _pieceManager = new GodotPieceManager();
        AddChild(_pieceManager);
        UpdatePieceManagerProperties();
    }
    private void UpdatePieceManagerProperties()
    {
        _pieceManager.SquareSize = _squareSize;
        _pieceManager.Style = _pieceStyle;
        _pieceManager.LightPieceColor = _lightPieceColor;
        _pieceManager.LightPieceOutlineColor = _lightPieceOutlineColor;
        _pieceManager.DarkPieceColor = _darkPieceColor;
        _pieceManager.DarkPieceOutlineColor = _darkPieceOutlineColor;
        _pieceManager.PieceMovementDelay = PieceMovementDelay;
    }
    private void UpdatePieceManager()
    {
        if (_pieceManager == null)
        {
            CreatePieceManager();
        }
        else
        {
           UpdatePieceManagerProperties();
        }
    }
    private void EditorOnReady()
    {
        UpdateSquares();
        UpdatePieceManager();
    }
    private void GameOnReady()
    {
        UpdateSquareSize();
        GetViewport().Connect("size_changed", Callable.From(UpdateSquareSize));
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
