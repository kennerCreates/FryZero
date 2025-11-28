using FryZeroGodot.gameplay.Pieces;
using Godot;

namespace FryZeroGodot.gameplay;

public partial class Game : Node
{
	public override void _Input(InputEvent @event)
	{
		if (@event is not InputEventMouseButton mouseButtonEvent) return;

		bool isLeftMouseButtonEvent = mouseButtonEvent.ButtonIndex is MouseButton.Left;
		if (!isLeftMouseButtonEvent) return;

		string method = mouseButtonEvent.Pressed ? nameof(Piece.PickUpPiece) : nameof(Piece.DropPiece);
		GetTree().CallGroup(CallGroups.LeftClick, method);
	}
}
