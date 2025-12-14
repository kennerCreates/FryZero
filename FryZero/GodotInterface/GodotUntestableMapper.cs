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

    public static RectangleShape2D ToRectangleShape2D(this RectangleShape ourRectangle, RectangleShape2D godotRectangle)
        {
            godotRectangle.Size = ourRectangle.Size.ToVector2();
            return godotRectangle;
        }
    
    public static RectangleShape ToRectangleShape(this RectangleShape2D godotRectangle) =>
        new()
        {
            Size = godotRectangle.Size.ToVector()
        };

    // public static GodotArea ToGodotArea(this Area ourArea, GodotArea godotArea)
    // {
    //     var godotShape = godotArea.Shape
    // }

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
