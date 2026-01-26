using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.EngineFiles;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.Buttons;

[GlobalClass]

public abstract partial class GodotButton : Node2D, IGodotButton
{
    [Export]
    public Vector2 Size
    {
        get => _size;
        set
        {
            if (_size == value) return;
            _size = value;
            OnSizeChanged();
        }
    }
    private Vector2 _size;
    private InteractState _interactState = InteractState.Normal;
    private Sprite2D _sprite;
    private Area2D _area;
    private CollisionShape2D _shape;
    public override void _Ready()
    {
        AddToGroup(CallGroups.LeftClick);
        AddChild(GetButtonSprite());
        AddChild(GetButtonArea());
        OnBeginPlay();
    }

    public abstract void OnBeginPlay();
    public abstract void LeftClickDown();
    public abstract void LeftClickReleased();

    private Sprite2D GetButtonSprite()
    {
        _sprite ??= new Sprite2D();
        _sprite.Material = GameTheme.GameTheme.Instance.GetThemeMaterial();
        _sprite.Scale = Size;
        return _sprite;
    }


    private Area2D GetButtonArea()
    {
        if (_area == null)
        {
            _area = new Area2D();
            _area.MouseEntered += () => SetMouseEntered(true);
            _area.MouseExited += () => SetMouseEntered(false);
            _area.AddChild(GetCollisionShape());
        }
        return _area;
    }

    private CollisionShape2D GetCollisionShape()
    {
        if (_shape == null)
        {
            _shape = new CollisionShape2D();
            _shape.Shape = new RectangleShape2D {Size = Size};
        }
        return _shape;
    }
    private void OnSizeChanged()
    {
        GetButtonSprite();
        GetButtonArea();
    }

    private void SetMouseEntered(bool isEntered)
    {
        _interactState = isEntered ? InteractState.Hovered : InteractState.Normal;
        GetButtonSprite();
    }

}
