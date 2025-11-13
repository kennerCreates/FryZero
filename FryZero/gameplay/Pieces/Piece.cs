using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[Tool]
public partial class Piece : Node2D
{
	
	[Export]
	public PieceType Type
	{
		get => _pieceType;
		set
		{
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
			_pieceFile = value;
			SetBoardLocationOfPiece();
		}
	}

	[Export]
	public Rank StartingRank
	{
		get => _pieceRank;
		set
		{
			_pieceRank = value;
			SetBoardLocationOfPiece();
		}
	}
	
	private PieceColor _pieceColor;
	private PieceType _pieceType;
	private PieceStyle _pieceStyle;
	private File _pieceFile;
	private Rank _pieceRank;
	private Sprite2D _pieceSprite;
	private PhysicsPiece _physicsPiece;
	private bool _isMouseEntered;
	private bool _isBeingMoved;
	private int _delay = 10;
	public void SetMouseEntered(bool entered)
	{
		_isMouseEntered = entered;
	}
	private void FindPhysicsPiece()
	{
		_physicsPiece = (PhysicsPiece)FindChild("RigidBody2D");
	}
	
	
	public override void _Ready()
	{
		AddToGroup("LeftClick");
		FindPhysicsPiece();
	}
	public override void _PhysicsProcess(double delta)
	{
		if (!_isBeingMoved) return;
		var tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", GetGlobalMousePosition(), _delay * delta);
	}

	
	private void PickUpPiece()
	{
		if (!_isMouseEntered) return;
		_isBeingMoved = true;
		_physicsPiece.PickedUpPiece();
	}
	private void DropPiece()
	{
		if (!_isMouseEntered) return;
		_isBeingMoved = false;
		_physicsPiece.DroppedPiece();
	}
	
	
	private void SetBoardLocationOfPiece()
	{
		Position = BoardLocations.GetPieceLocation(_pieceFile, _pieceRank);
	}
	
	private void CreatePiece() => SetPieceImage();
	private void SetPieceImage()
	{
		GetPieceSprite();
		_pieceSprite.Texture = GD.Load<Texture2D>($"res://Assets/Pieces/{_pieceStyle}/{_pieceColor}/{_pieceType}.svg");
	}

	private void GetPieceSprite() =>
		_pieceSprite = GetNode<Sprite2D>("physics/RigidBody2D/Sprite2D");
	
}