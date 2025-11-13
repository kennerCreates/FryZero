using Godot;

namespace FryZeroGodot.gameplay;

public partial class Game : Node
{
	public override void _Input(InputEvent @event)
	{
		if (@event is not InputEventMouseButton mouseButtonEvent) return;
		if (mouseButtonEvent.ButtonIndex != MouseButton.Left) return;
		GetTree().CallGroup("LeftClick", mouseButtonEvent.Pressed ? "PickUpPiece" : "DropPiece");
	}
}