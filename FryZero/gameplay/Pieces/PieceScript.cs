using Godot;

namespace FryZeroGodot.gameplay.Pieces;

[Tool]
[GlobalClass]
public partial class PieceScript : Area2D
{
    [Export]
    public Sprite2D Sprite { get; set; }
    public PackedScene PieceScene { get; set; }

    private Node _instance;

    public override void _EnterTree()
    {
        if (!Engine.IsEditorHint())
            return;
        Regenerate();
    }

    private void Regenerate()
    {

        if (_instance != null)
        {
            _instance.QueueFree();
            _instance = null;
        }

        if (PieceScene == null)
            return;

        _instance = PieceScene.Instantiate();
        AddChild(_instance);
        
        SetOwnerRecursive(_instance, GetTree().EditedSceneRoot);
    }

    private void SetOwnerRecursive(Node node, Node owner)
    {
        node.Owner = owner;
        foreach (Node child in node.GetChildren())
        {
            SetOwnerRecursive(child, owner);
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationEditorPreSave)
        {
            Regenerate();
        }
    }
}
