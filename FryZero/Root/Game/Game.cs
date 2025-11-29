using Godot;

namespace FryZeroGodot.Root.Game;

[Tool]

[GlobalClass ]

public partial class Game : Node2D
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
