using Godot;

namespace FryZeroGodot.Root.Game.Board;

[Tool]

[GlobalClass]

public partial class Board : Control
{
    public override void _Ready()
    {
        if (Engine.IsEditorHint())
        {
            EditorOnSpawn();
        }
        else
        {
            GameOnSpawn();
        }
    }

    private void EditorOnSpawn()
    {

    }

    private void GameOnSpawn()
    {
        EditorOnSpawn();
    }
}
