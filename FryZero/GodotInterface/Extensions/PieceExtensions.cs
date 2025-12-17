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

    public static RectangleShape2D WithUpdatedShape(this RectangleShape2D shape, int squareSize)
    {
        shape.Size = new Vector2(squareSize,squareSize);
        return shape;
    }

    // public static void UpdateShape(this RectangleShape2D godotRectangle, int squareSize)
    // {
    //     godotRectangle.WithUpdatedShape(squareSize);
    // }

    public static GodotArea WithUpdatedArea(this GodotArea area, RectangleShape2D shape)
    {
        area.Shape = shape;
        return area;
    }
    
    // public static void UpdateArea(this GodotArea godotArea, RectangleShape2D shape)
    // {
    //     godotArea.ToArea().WithUpdatedArea(shape.ToRectangleShape()).ToGodotArea(godotArea);
    // }

    public static GodotPhysics WithUpdatedPhysics(this GodotPhysics physics, RectangleShape2D shape)
    {
        physics.Shape = shape;
        return physics;
    }
    
    // public static void UpdatePhysics(this GodotPhysics godotPhysics, RectangleShape2D shape)
    // {
    //     godotPhysics.ToPhysics().WithUpdatedPhysics(shape.ToRectangleShape()).ToGodotPhysics(godotPhysics);
    // }

}
