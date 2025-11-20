using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[GlobalClass]
public partial class HoldPoint : StaticBody2D
{
	[Export] public Shape2D Shape;

	public override void _Ready()
	{
		SpawnCollisionShape();
	}

	private void SpawnCollisionShape()
	{
		var collisionShape = new CollisionShape2D();
		collisionShape.Shape = Shape;
		AddChild(collisionShape);
		GD.Print("Hold point collision shape added");
	}
}