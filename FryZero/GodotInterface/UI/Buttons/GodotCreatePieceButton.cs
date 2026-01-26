using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.EngineFiles;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.Buttons;
public partial class GodotCreatePieceButton : GodotButton
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    public override void OnBeginPlay()
    {

    }
    public override void LeftClickDown()
    {

    }

    public override void LeftClickReleased()
    {

    }

}
