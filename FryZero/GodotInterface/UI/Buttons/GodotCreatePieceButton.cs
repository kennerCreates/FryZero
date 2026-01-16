using System;
using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotInterface.Gameplay.Board;
using FryZeroGodot.GodotNodes.EngineFiles;
using Godot;
using Godot.Collections;
using GameTheme = FryZeroGodot.GodotInterface.UI.GameTheme.GameTheme;

namespace FryZeroGodot.GodotInterface.UI.Buttons;

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
        _sprite.Material = GameTheme.GameTheme.Instance.GetThemeMaterial();
        _sprite.Scale = new Vector2(GameTheme.GameTheme.Instance.GetSquareSize(), GameTheme.GameTheme.Instance.GetSquareSize()) / _sprite.Texture.GetSize();
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
