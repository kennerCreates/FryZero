using Godot;

namespace FryZeroGodot.GodotNodes.NodeModels;

public abstract partial class LevelTwoNode : Node2D
{
    [Signal]
    public delegate void LevelTwoNodeInitializedEventHandler();

    public bool IsInitialized { get; private set; }

    protected LevelOneNode LevelOneNode;

    public override void _EnterTree()
    {
        LevelOneNode = GetParent<LevelOneNode>();
        if (LevelOneNode.IsInitialized)
        {
            OnLevelOneReady();
        }
        else
        {
            LevelOneNode.LevelOneNodeInitialized += OnLevelOneReady;
        }
    }

    private void OnLevelOneReady()
    {
        OnReady();
        IsInitialized = true;
        EmitSignal(SignalName.LevelTwoNodeInitialized);
    }

    protected abstract void OnReady();
}
