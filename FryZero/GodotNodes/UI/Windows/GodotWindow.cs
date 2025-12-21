using Godot;

namespace FryZeroGodot.GodotNodes.UI.Windows;

[Tool]

[GlobalClass]

public partial class GodotBoard : SubViewportContainer
{
    private SubViewport _subViewport;

    private void CreateSubViewport()
    {
        _subViewport = new SubViewport();
        AddChild(_subViewport);
    }

    private void ResizeViewport()
    {
        if (_subViewport == null) return;
        var size = Size;
        if (size.X == 0 || size.Y == 0) return;
    }
    private void EditorOnReady()
    {
        CreateSubViewport();
    }
    private void GameOnReady()
    {

    }


    public override void _EnterTree()
    {
    }
    public override void _Ready()
    {
        if (Engine.IsEditorHint())
        {
            EditorOnReady();
        }
        else
        {
            EditorOnReady();
            GameOnReady();
        }
    }
}
