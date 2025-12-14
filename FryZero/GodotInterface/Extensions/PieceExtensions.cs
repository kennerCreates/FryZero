using FryZeroGodot.GodotInterface.Models;
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

    public static RectangleShape WithUpdateShape(this RectangleShape shape, int squareSize) =>
        shape with
        {
            Size = new Vector(squareSize, squareSize)
        };
    
    public static void UpdateShape(this RectangleShape2D godotRectangle, int squareSize)
    {
        godotRectangle.ToRectangleShape().WithUpdateShape(squareSize).ToRectangleShape2D(godotRectangle);
    }
}
