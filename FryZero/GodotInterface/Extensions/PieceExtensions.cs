using FryZeroGodot.GodotNodes.Game.Pieces;
using FryZeroGodot.GodotNodes.UI.Buttons;
using Godot;

namespace FryZeroGodot.GodotInterface.Extensions
{
    public static class PieceExtensions
    {
        public static void WithUpdatedSoftness(this PinJoint2D godotPinJoint, int squareSize)
        {
            godotPinJoint.Softness = squareSize / 100f;
        }

        public static void WithUpdatedShape(this RectangleShape2D shape, int squareSize)
        {
            shape.Size = new Vector2(squareSize,squareSize);
        }
        public static void WithUpdatedPieceArea(this GodotPieceArea pieceArea, RectangleShape2D shape)
        {
            pieceArea.Shape = shape;
        }

        public static void WithUpdatedNewPieceButtonArea(this GodotButtonArea area,
            RectangleShape2D shape)
        {
            area.Shape = shape;
        }
        public static void WithUpdatedPhysics(this GodotPhysics physics, RectangleShape2D shape)
        {
            physics.Shape = shape;
        }


    }
}
