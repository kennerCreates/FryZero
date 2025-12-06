using FryZeroGodot.Config;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.Config.Pieces;
using FryZeroGodot.Config.Structs;
using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[Tool]
[GlobalClass]
public partial class Piece : Node2D
{

	[Export] public PieceType Type;
	[Export] public PieceColor Color;
	[Export] public PieceStyle Style;
	[Export] public Shape2D Shape;
	[Export] public int SquareSize;

	[Export]
	public File StartingFile
	{
		get => _startingFile;
		set
		{
			_startingFile = value;
			CreatePiece();
		}
	}

	[Export]
	public Rank StartingRank
	{
		get => _startingRank;
		set
		{
			_startingRank = value;
			CreatePiece();
		}
	}
	private File _startingFile;
	private Rank _startingRank;
	
	private Sprite2D _pieceSprite;
	private PhysicsPiece _physicsPiece;
	private HoldPoint _holdPoint;
	private PieceArea _pieceArea;
	private PinJoint2D _pinJoint;
	
	private bool _isMouseEntered;
	private bool _isBeingMoved;
	
	private static PieceAttributes PieceAttributes => new()
	{
		SquareSize = 160,
		MovementDelay = 10
	};

	public override void _Ready()
	{
		if (Engine.IsEditorHint())
		{
			CreatePiece();
		}
		else
		{
			CreatePiece();
			CreateRuntimePiece();
			AddToGroup(CallGroups.LeftClick);
		}
	}
	public override void _PhysicsProcess(double delta)
	{
		if (!_isBeingMoved) return;
		var tween = GetTree().CreateTween();
		tween.TweenProperty(this, "position", GetGlobalMousePosition(), PieceAttributes.MovementDelay * delta);
	}

	public void PickUpPiece()
	{
		if (!_isMouseEntered) return;
		_isBeingMoved = true;
		_physicsPiece.PickedUpPiece();
	}

	public void DropPiece()
	{
		if (!_isMouseEntered) return;
		_isBeingMoved = false;
		_physicsPiece.DroppedPiece();
	}

	private void CreateRuntimePiece()
	{
		if (_physicsPiece == null)
		{
			CreatePhysicsPiece();
		}
		if (_pieceArea == null)
		{
			CreatePieceArea();
		}
		if (_holdPoint == null)
		{
			CreateHoldPoint();
		}
		if (_pinJoint == null)
		{
			CreatePinJoint();
		}
	}
	private void CreatePiece()
	{
		if (_pieceSprite == null)
		{
			CreateSprite();
		}
		
		SetPieceImage();
		SetPiecePosition();
	}
	
	private void SetPiecePosition()
	{
		var square = new Square(StartingFile, StartingRank);
		Position = square.LocationVector(SquareSize);
	}

	private void SetPieceImage()
	{
		_pieceSprite.Texture = GD.Load<Texture2D>($"res://Assets/Pieces/{Style}/{Type}.png");
	}

	private void CreateSprite()
	{
		_pieceSprite = new Sprite2D();
		AddChild(_pieceSprite);
	}

	private void CreatePhysicsPiece()
	{
		_physicsPiece = new PhysicsPiece();
		_physicsPiece.Shape = Shape;
		AddChild(_physicsPiece);
	}

	private void CreatePinJoint()
	{
		if (_holdPoint == null || _physicsPiece == null) return;
		
		_pinJoint = new PinJoint2D();
		AddChild(_pinJoint);
		
		_pinJoint.Softness = 1;
		_pinJoint.NodeA = _holdPoint.GetPath();
		_pinJoint.NodeB = _physicsPiece.GetPath();
		_pinJoint.GlobalPosition = (_holdPoint.GlobalPosition + _physicsPiece.GlobalPosition) / 2;
	}

	private void CreatePieceArea()
	{
		_pieceArea = new PieceArea();
		_pieceArea.Shape = Shape;
		AddChild(_pieceArea);
	}

	private void CreateHoldPoint()
	{
		_holdPoint = new HoldPoint();
		var circle = new CircleShape2D();
		circle.Radius = 5;
		_holdPoint.Shape = circle;
		_holdPoint.CollisionLayer = 0;
		_holdPoint.CollisionMask = 0;
		_holdPoint.Position = Position + new Vector2(0, -40);
		AddChild(_holdPoint);
	}
	
	public void SetMouseEntered(bool entered)
	{
		_isMouseEntered = entered;
	}
}
