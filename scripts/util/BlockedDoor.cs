using DialogueManagerRuntime;
using Godot;
using System;

public partial class BlockedDoor : Area2D
{
    [Export]
    private Resource dialogue;
    public void OnBlockedDoorEntered(Node2D body)
    {
        DialogueManager.ShowDialogueBalloon(dialogue, "room_is_not_used");
    }
}
