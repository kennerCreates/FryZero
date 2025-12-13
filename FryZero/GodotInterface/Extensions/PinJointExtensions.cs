using FryZeroGodot.GodotInterface.Models;
using Godot;

namespace FryZeroGodot.GodotInterface.Extensions;

public static class PinJointExtensions
{
    public static PinJoint WithUpdatedSoftness(this PinJoint pinJoint, int squareSize) =>
        pinJoint with
        {
            Softness = squareSize / 100f
        };

    public static void UpdateSoftness(this PinJoint2D pinJoint2D, int squareSize)
    {
        pinJoint2D.ToPinJoint().WithUpdatedSoftness(squareSize).ToPinJoint2D(pinJoint2D);
    }
}
