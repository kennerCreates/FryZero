using FryZeroGodot.Config.Enums;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.Buttons;
public partial class GodotCreatePieceButton : GodotButton
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    public override void OnBeginPlay()
    {
        UpdateSpriteTexture(
            GameTheme.GameTheme.Instance.GetPieceTexture(Type, Color,InteractState.Normal),
            GameTheme.GameTheme.Instance.GetPieceTexture(Type, Color, InteractState.Hovered)
        );
        var squareSize = GameTheme.GameTheme.Instance.GetSquareSize(); ;
        UpdateSpriteSize(new Vector2(squareSize, squareSize));
    }
    public override void LeftClickDown()
    {

    }

    public override void LeftClickReleased()
    {

    }

}
