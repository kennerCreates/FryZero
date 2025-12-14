using System.Drawing;
using FryZeroGodot.GodotInterface.Models;
using FryZeroGodot.GodotNodes.Game.Pieces;
using Godot;

namespace FryZeroGodot.GodotInterface.Extensions;

public static class PieceExtensions
{
    public static PinJoint WithUpdatedSoftness(this PinJoint pinJoint, int squareSize) =>
        pinJoint with
        {
            Softness = squareSize / 100f
        };

    public static void UpdateSoftness(this PinJoint2D godotPinJoint, int squareSize)
    {
        godotPinJoint.ToPinJoint().WithUpdatedSoftness(squareSize).ToPinJoint2D(godotPinJoint);
    }

    public static RectangleShape WithUpdatedShape(this RectangleShape ourShape, int squareSize) =>
        ourShape with
        {
            Size = new Vector(squareSize, squareSize)
        };
    
    public static void UpdateShape(this RectangleShape2D godotRectangle, int squareSize)
    {
        godotRectangle.ToRectangleShape().WithUpdatedShape(squareSize).ToRectangleShape2D(godotRectangle);
    }

    public static Area WithUpdatedArea(this Area ourArea, RectangleShape shape) =>
        ourArea with
        {
            Shape = shape
        };
    
    public static void UpdateArea(this GodotArea godotArea, RectangleShape2D shape)
    {
        godotArea.ToArea().WithUpdatedArea(shape.ToRectangleShape()).ToGodotArea(godotArea);
    }

    public static Physics WithUpdatedPhysics(this Physics ourPhysics, RectangleShape shape) =>
        ourPhysics with
        {
            Shape = shape
        };
    
    public static void UpdatePhysics(this GodotPhysics godotPhysics, RectangleShape2D shape)
    {
        godotPhysics.ToPhysics().WithUpdatedPhysics(shape.ToRectangleShape()).ToGodotPhysics(godotPhysics);
    }

}
