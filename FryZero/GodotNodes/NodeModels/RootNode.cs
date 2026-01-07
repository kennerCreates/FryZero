using Godot;

namespace FryZeroGodot.GodotNodes.NodeModels;

public abstract partial class RootNode : Node2D
{
    [Signal]
    public delegate void RootNodeInitializedEventHandler();

    public bool IsInitialized { get; private set; }

    public override void _Ready()
    {
        OnReady();
        IsInitialized = true;
        EmitSignal(SignalName.RootNodeInitialized);
    }

    protected abstract void OnReady();

}
