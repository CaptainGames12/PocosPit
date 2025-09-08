using DialogueManagerRuntime;
using Godot;
using System;

public partial class CollectableItems : InteractionHandler
{
    [Export]
    private string itemDescription;

    private Resource dialogue = ResourceLoader.Load("res://ui/notes.dialogue");
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("interact") && isPlayerNearInteractableItem)
        {
            DialogueManager.ShowDialogueBalloon(dialogue, itemDescription);
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithItem, this, isPlayerNearInteractableItem);
            if (Name == "Key")
            {
                SignalBus.Instance.EmitSignal(SignalBus.SignalName.DoorUnlocked, "key");
            }
            if (Name == "Map" || Name == "Key")
            {
                QueueFree();
            }
        }
        else if (!isPlayerNearInteractableItem)
        {

            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithItem, this, isPlayerNearInteractableItem);
        }
    }
}
