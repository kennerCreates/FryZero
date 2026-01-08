using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.EngineFiles;
using FryZeroGodot.GodotNodes.Gameplay.Board;
using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes.UI.Buttons;

[GlobalClass]

public partial class GodotCreatePieceButton : Node2D
{
    [Export] public PieceType Type { get; set; }
    [Export] public PieceColor Color { get; set; }

    private InteractState _interactState = InteractState.Normal;
    private Sprite2D _sprite;
    private bool _isMouseEntered;

    public override void _Ready()
    {

        AddToGroup(CallGroups.LeftClick);
    }

    private Sprite2D GetButtonSprite()
    {
        _sprite ??= new Sprite2D();
        _sprite.Texture = GodotPieceManager.GetPieceTexture(Type, Color, _interactState);
        _sprite.Material = GameTheme.GetThemeMaterial();
        _sprite.Scale = new Vector2(GameTheme.GetSquareSize(), GameTheme.GetSquareSize()) / _sprite.Texture.GetSize();
        return _sprite;
    }


    public void LeftClickDown()
    {


    }

    public void LeftClickReleased()
    {

    }

    public void SetMouseEntered(bool isEntered)
    {
        _interactState = isEntered ? InteractState.Hovered : InteractState.Normal;
        GetButtonSprite();
    }

}
