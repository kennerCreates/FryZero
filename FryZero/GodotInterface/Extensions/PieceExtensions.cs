using System.Drawing;
using FryZeroGodot.GodotInterface.Models;
using FryZeroGodot.GodotNodes.Game.Board;
using FryZeroGodot.GodotNodes.Game.Pieces;
using FryZeroGodot.GodotNodes.UI.Buttons;
using Godot;

namespace FryZeroGodot.GodotInterface.Extensions
{
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
        public static GodotPieceArea WithUpdatedPieceArea(this GodotPieceArea pieceArea, RectangleShape2D shape)
        {
            pieceArea.Shape = shape;
            return pieceArea;
        }

        public static GodotButtonArea WithUpdatedNewPieceButtonArea(this GodotButtonArea area,
            RectangleShape2D shape)
        {
            area.Shape = shape;
            return area;
        }
        public static GodotPhysics WithUpdatedPhysics(this GodotPhysics physics, RectangleShape2D shape)
        {
            physics.Shape = shape;
            return physics;
        }


    }
}
