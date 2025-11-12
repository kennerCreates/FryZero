using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[Tool]
public partial class Piece : Node2D
{
	[Export] protected bool ChangingProperties;

	[Export]
	public PieceType Type
	{
		get => _pieceType;
		set
		{
			if (!ChangingProperties) return;
			_pieceType = value;
			CreatePiece();
		}
	}

	[Export]
	public PieceColor Color
	{
		get => _pieceColor;
		set
		{
			if (!ChangingProperties) return;
			_pieceColor = value;
			CreatePiece();
		}
	}

	[Export]
	public PieceStyle Style
	{
		get => _pieceStyle;
		set
		{
			if (!ChangingProperties) return;
			_pieceStyle = value;
			CreatePiece();
		}
	}

	[Export]
	public File StartingFile
	{
		get => _pieceFile;
		set
		{
			if (!ChangingProperties) return;
			_pieceFile = value;
			SetLocation();
		}
	}

	[Export]
	public Rank StartingRank
	{
		get => _pieceRank;
		set
		{
			if (!ChangingProperties) return;
			_pieceRank = value;
			SetLocation();
		}
	}

	private PieceColor _pieceColor;
	private PieceType _pieceType;
	private PieceStyle _pieceStyle;
	private File _pieceFile;
	private Rank _pieceRank;
	private Sprite2D _pieceSprite;
	private int _squareSize = 160;
	

	private void SetLocation()
	{
		Position = new Vector2(GetFileLocation(_pieceFile), GetRankLocation(_pieceRank));
	}

	private int GetFileLocation(File file)
	{
		var screenX = (int)file * _squareSize - GetHalfBoardSize();
		return screenX;
	}

	private int GetRankLocation(Rank rank)
	{
		var screenY = ((int)rank * _squareSize - GetHalfBoardSize()) * -1;
		return screenY;
	}

	private int GetHalfBoardSize()
	{
		return _squareSize * 3 + _squareSize / 2;
	}
	private void CreatePiece()
	{
		SetPieceImage();
	}

	private void SetPieceImage()
	{
		GetPieceSprite();
		_pieceSprite.Texture = GD.Load<Texture2D>($"res://Assets/Pieces/{_pieceStyle}/{_pieceColor}/{_pieceType}.svg");
	}

	private void GetPieceSprite()
	{
		_pieceSprite = GetNode<Sprite2D>("physics/RigidBody2D/Sprite2D");
	}
}