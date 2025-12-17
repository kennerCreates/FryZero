using FryZeroGodot.GodotInterface.Models;
using FryZeroGodot.GodotNodes.Game.Pieces;
using Godot;

namespace FryZeroGodot.GodotInterface;

public static class GodotUntestableMapper
{
    public static Vector2 ToVector2(this Vector ourVector) =>
        new(ourVector.X, ourVector.Y);

    public static Vector ToVector(this Vector2 godotVector) =>
        new(godotVector.X, godotVector.Y);

    public static RectangleShape2D ToRectangleShape2D(this RectangleShape ourRectangle, RectangleShape2D godotRectangle) =>
        new()
        {
            Size = ourRectangle.Size.ToVector2()
        };
    
    public static RectangleShape ToRectangleShape(this RectangleShape2D godotRectangle) =>
        new()
        {
            Size = godotRectangle.Size.ToVector()
        };

// note if you want to add more properties than shape this will create more node copies -- i think
    public static GodotArea ToGodotArea(this Area ourArea, GodotArea godotArea) =>
        new()
        {
            Shape = ourArea.Shape.ToRectangleShape2D(godotArea.Shape)
        };

    public static Area ToArea (this GodotArea godotArea) =>
        new()
        {
            Shape = godotArea.Shape.ToRectangleShape()
        };

// note if you want to add more properties than shape this will create more node copies -- i think
    public static GodotPhysics ToGodotPhysics(this Physics ourPhysics, GodotPhysics godotPhysics) =>
        new()
        {
            Shape = ourPhysics.Shape.ToRectangleShape2D(godotPhysics.Shape)
        };

    public static Physics ToPhysics(this GodotPhysics godotPhysics) =>
        new()
        {
            Shape = godotPhysics.Shape.ToRectangleShape()
        };

    public static PinJoint2D ToPinJoint2D(this PinJoint ourPinJoint, PinJoint2D godotPinJoint)
        {
            godotPinJoint.Softness = ourPinJoint.Softness;
            godotPinJoint.Position = ourPinJoint.Position.ToVector2();
            return godotPinJoint;
        }

    public static PinJoint ToPinJoint(this PinJoint2D godotPinJoint) =>
        new()
        {
            Softness = godotPinJoint.Softness,
            Position = godotPinJoint.Position.ToVector()
        };
}
