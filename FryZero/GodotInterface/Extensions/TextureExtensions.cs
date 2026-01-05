using Godot;

namespace FryZeroGodot.GodotInterface.Extensions;

public static class TextureExtensions
{
    public static Sprite2D AddSprite2DAsChild(this Texture2D texture, Node2D parentNode)
    {
        var darkSquares = new Sprite2D
        {
            Texture = texture
        };
        parentNode.AddChild(darkSquares);
        return darkSquares;
    }
}
