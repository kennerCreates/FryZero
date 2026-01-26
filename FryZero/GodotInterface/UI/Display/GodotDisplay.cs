using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Gameplay.Pieces;
using FryZeroGodot.GodotInterface.UI.Buttons;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.Statics.UI.Display;
using Godot;
using Godot.Collections;

namespace FryZeroGodot.GodotInterface.UI.Display;

[GlobalClass]

public partial class GodotDisplay : Node2D
{
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is not InputEventMouseButton mouseButtonEvent) return;

        var isLeftMouseButtonEvent = mouseButtonEvent.ButtonIndex is MouseButton.Left;
        if (!isLeftMouseButtonEvent) return;

        var method = mouseButtonEvent.Pressed ? nameof(GodotButton.LeftClickDown) : nameof(GodotButton.LeftClickReleased);
        GetTree().CallGroup(CallGroups.LeftClick, method);
    }
    public override void _Ready()
    {
        SpawnNewPieceButtonNodes();

    }

    private void SpawnNewPieceButtonNodes()
    {
        foreach (var color in Enum.GetValues<PieceColor>())
        {
            foreach (var type in Enum.GetValues<PieceType>())
            {
                var button = PieceButtonLocations.CreateNewPieceButton(color: color, type: type, squareSize: GameTheme.GameTheme.Instance.GetSquareSize());
                button.ZIndex = 10;
                AddChild(button);
                GD.Print("Button Spawned:" + color + " " + type + " " + button.Position);
            }
        }
    }

    // private GodotPiece _pieceBeingSpawned;
    //
    // private void SpawnActualGodotPiece(PieceType type, PieceColor color, Vector2 location)
    // {
    //     _pieceBeingSpawned = PositionExtensions.CreateOneHeldPiece(type, color);
    //     _pieceBeingSpawned.SetToPickedUp();
    //     _pieceBeingSpawned.Position = location;
    //     AddChild(_pieceBeingSpawned);
    // }
    //
    // private void UpdatePieceBeingSpawned()
    // {
    //     _pieceBeingSpawned.HandlePieceOnBoardOrNot();
    // }

}
