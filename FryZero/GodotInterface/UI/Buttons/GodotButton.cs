using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.EngineFiles;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.Buttons;

[GlobalClass]

public abstract partial class GodotButton : Node2D, IGodotButton
{
    [Export] public Vector2 SpriteScale { get; set; }
    [Export] public Texture2D TextureNormal { get; set; }
    [Export] public Texture2D TextureHovered { get; set; }

    public override void _Ready()
    {
        ZIndex = 9;
        AddToGroup(CallGroups.LeftClick);
        AddChild(GetButtonSprite());
        AddChild(GetButtonArea());
        _area.AddChild(GetCollisionShape());
        OnBeginPlay();
    }
    public abstract void OnBeginPlay();
    public abstract void LeftClickDown();
    public abstract void LeftClickReleased();

    protected Sprite2D ButtonSprite;
    private Sprite2D GetButtonSprite()
    {
        ButtonSprite ??= new Sprite2D();
        ButtonSprite.Material = GameTheme.GameTheme.Instance.GetThemeMaterial();
        ButtonSprite.Texture = TextureNormal;
        ButtonSprite.Scale = SpriteScale;
        return ButtonSprite;
    }

    private Area2D _area;
    private CollisionShape2D _shape;
    private Area2D GetButtonArea()
    {
        if (_area == null)
        {
            _area = new Area2D();
            _area.MouseEntered += () => SetMouseEntered(true);
            _area.MouseExited += () => SetMouseEntered(false);
            _area.InputPickable = true;

        }
        return _area;
    }
    private CollisionShape2D GetCollisionShape()
    {
        if (_shape == null)
        {
            _shape = new CollisionShape2D();
            _shape.Shape = new RectangleShape2D
            {
                Size = new Vector2(GameTheme.GameTheme.Instance.GetSquareSize(), GameTheme.GameTheme.Instance.GetSquareSize())
            };
        }
        return _shape;
    }

    private InteractState _interactState = InteractState.Normal;
    protected bool IsMouseEntered;
    private void SetMouseEntered(bool isEntered)
    {
        IsMouseEntered = isEntered;
        _interactState = isEntered ? InteractState.Hovered : InteractState.Normal;
        UpdateTextureBasedOnInteractState(_interactState);
        //GD.Print(isEntered ? "Entered" : "Exited");
    }
    private void UpdateTextureBasedOnInteractState(InteractState state)
    {
        ButtonSprite.Texture = state switch
        {
            InteractState.Normal => TextureNormal,
            InteractState.Hovered => TextureHovered,
            _ => TextureNormal
        };
    }
    protected void UpdateSpriteTexture(Texture2D textureNormal, Texture2D textureHovered)
    {
        TextureNormal = textureNormal;
        TextureHovered = textureHovered;
        UpdateTextureBasedOnInteractState(_interactState);
    }
    protected void UpdateSpriteScale(Vector2 size)
    {
        SpriteScale = size;
        ButtonSprite.Scale = size;
    }


}
