using FryZeroGodot.Config.Enums;
using FryZeroGodot.GodotNodes.EngineFiles;
using Godot;

namespace FryZeroGodot.GodotInterface.UI.Buttons;

[GlobalClass]

public abstract partial class GodotButton : Node2D, IGodotButton
{
    [Export] public Vector2 SpriteSize { get; set; }
    [Export] public Texture2D TextureNormal { get; set; }
    [Export] public Texture2D TextureHovered { get; set; }
    [Export] public Material SpriteMaterial { get; set; } = GameTheme.GameTheme.Instance.GetThemeMaterial();

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

    private Sprite2D _buttonSprite;

    protected Sprite2D GetButtonSprite()
    {
        _buttonSprite ??= new Sprite2D();
        _buttonSprite.Material = SpriteMaterial;
        UpdateTextureBasedOnInteractState(_interactState);

        return _buttonSprite;
    }

    private Area2D _area;
    private Area2D GetButtonArea()
    {
        if (_area != null) return _area;
        _area = new Area2D();
        _area.MouseEntered += () => SetMouseEntered(true);
        _area.MouseExited += () => SetMouseEntered(false);
        _area.InputPickable = true;
        return _area;
    }

    private CollisionShape2D _collisionShape;
    private CollisionShape2D GetCollisionShape()
    {
        _collisionShape ??= new CollisionShape2D();
        _collisionShape.Shape = GetButtonShape();
        return _collisionShape;
    }

    protected RectangleShape2D GetButtonShape()
    {
        var shape = new RectangleShape2D();
        shape.Size = SpriteSize;
        return shape;
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
        _buttonSprite.Texture = state switch
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
    protected void UpdateSpriteSize(Vector2 size)
    {
        SpriteSize = size;
        GetButtonSprite().Scale = SpriteSize / GetButtonSprite().Texture.GetSize();
        GetCollisionShape();
    }

    private void UpdateSpriteMaterial(Material material)
    {
        GetButtonSprite().Material = material;
    }


}
