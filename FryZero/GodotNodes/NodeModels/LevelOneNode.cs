using Godot;

namespace FryZeroGodot.GodotNodes.NodeModels;

public abstract partial class LevelOneNode : Node2D
{
    [Signal]
    public delegate void LevelOneNodeInitializedEventHandler();

    public bool IsInitialized { get; private set; }

    protected RootNode RootNode;

    public override void _EnterTree()
    {
        RootNode = GetParent<RootNode>();
        if (RootNode.IsInitialized)
        {
            OnRootReady();
        }
        else
        {
            RootNode.RootNodeInitialized += OnRootReady;
        }
    }

    private void OnRootReady()
    {
        OnReady();
        IsInitialized = true;
        EmitSignal(SignalName.LevelOneNodeInitialized);
    }

    protected abstract void OnReady();
}
