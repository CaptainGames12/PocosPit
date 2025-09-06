using DialogueManagerRuntime;
using Godot;
using System;

public partial class Entrance : InteractionHandler
{
    [Export]
    private Resource dialogue;
    private bool isNotFirstInteraction = false;
    public override void _PhysicsProcess(double delta)
    {
        if (Globals.Instance.currentObjective != "Exit through the entrance door")
        {
            return;
        }
        base._PhysicsProcess(delta);
        if (isPlayerNearInteractableItem && Input.IsActionJustPressed("interact") && !isNotFirstInteraction)
        {
            DialogueManager.ShowDialogueBalloon(dialogue, "door_seems_closed");
            isNotFirstInteraction = true;
        }
        else if (!Globals.Instance.isCutSceneGoing && isPlayerNearInteractableItem && Input.IsActionJustPressed("interact") && isNotFirstInteraction)
        {
            DialogueManager.ShowDialogueBalloon(dialogue, "door_is_closed");
        }
    }
}
