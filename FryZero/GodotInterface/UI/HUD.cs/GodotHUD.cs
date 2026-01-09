using Godot;

namespace FryZeroGodot.GodotInterface.UI.HUD.cs;

[GlobalClass]

public partial class GodotHud : Node2D
{

    // private void SpawnNewPieceButtonNodes()
    // {
    //     foreach (var color in Enum.GetValues<PieceColor>())
    //     {
    //         foreach (var type in Enum.GetValues<PieceType>())
    //         {
    //             var button = ButtonLocations.CreateNewPieceButton(color: color, type: type, squareSize: SquareSize);
    //             button.Style = Style;
    //             AddChild(button);
    //         }
    //     }
    // }
    //
    // private GodotPiece _pieceBeingSpawned;
    // public void SpawnActualGodotPiece(PieceType type, PieceColor color, Vector2 location)
    // {
    //     _pieceBeingSpawned = PositionExtensions.CreateOneHeldPiece(type, color);
    //     SetPieceVisuals(_pieceBeingSpawned);
    //     ConfigurePieceProperties(_pieceBeingSpawned);
    //     _pieceBeingSpawned.SetToPickedUp();
    //     _pieceBeingSpawned.Position = location;
    //     AddChild(_pieceBeingSpawned);
    // }
    //
    // public void UpdatePieceBeingSpawned()
    // {
    //     _pieceBeingSpawned.HandlePieceOnBoardOrNot();
    // }

    // public override void _UnhandledInput(InputEvent @event)
    // {
    //     if (@event is not InputEventMouseButton mouseButtonEvent) return;
    //
    //     var isLeftMouseButtonEvent = mouseButtonEvent.ButtonIndex is MouseButton.Left;
    //     if (!isLeftMouseButtonEvent) return;
    //
    //     var method = mouseButtonEvent.Pressed ? nameof(GodotPiece.LeftClickDown) : nameof(GodotPiece.LeftClickReleased);
    //     GetTree().CallGroup(CallGroups.LeftClick, method);
    // }
}
