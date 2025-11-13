using Godot;

namespace FryZeroGodot.gameplay.Pieces;
[GlobalClass]
public partial class PhysicsPiece : RigidBody2D
{
	public override void _Ready()
	{
		CollisionLayer = 9;
		ZIndex = 10;
	}

	public void DroppedPiece()
		{
			CollisionLayer = 9;
			ZIndex = 10;
		}
	public void PickedUpPiece()
		{
			CollisionLayer = 10;
			ZIndex = 20;
		}
}
	