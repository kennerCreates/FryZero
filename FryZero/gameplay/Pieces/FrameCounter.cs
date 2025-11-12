using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[Tool]
public partial class FrameCounter : Label
{
	private int _frameCount;
	
	public override void _Process(double delta)
	{
		_frameCount++;
		Text = _frameCount.ToString();
	}
}