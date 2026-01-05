using FryZeroGodot.GodotNodes.UI.ColorScheme;
using Godot;

namespace FryZeroGodot.GodotNodes;

public abstract partial class BaseNode : Node2D
{
    protected GodotColorScheme ColorScheme;

    public override void _EnterTree()
    {
        ColorScheme = GetParent<GodotColorScheme>();
        if (ColorScheme.IsInitialized)
        {
            OnReady();
        }
        else
        {
            ColorScheme.ColorSchemeInitialized += OnReady;
        }
    }

    protected abstract void OnReady();
}
