using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[GlobalClass]
public partial class PhysicsPiece : RigidBody2D
{
	[Export] public Shape2D Shape;
	public override void _Ready()
	{
		SpawnCollisionShape();
		CollisionLayer = 9;
		ZIndex = 10;
	}

	private void SpawnCollisionShape()
	{
		var collisionShape = new CollisionShape2D();
		collisionShape.Shape = Shape;
		AddChild(collisionShape);
		GD.Print("Collision shape added");
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
