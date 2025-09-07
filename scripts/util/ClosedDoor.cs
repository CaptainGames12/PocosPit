using DialogueManagerRuntime;
using Godot;
using System;

public partial class ClosedDoor : RoomChanger
{
    [Export]
    private string theTypeOfLock;
    [Export]
    private string howToUnlock;
    [Export]
    private CollisionShape2D door;
    [Export]
    private Resource dialogue;
    private bool doorIsUnlocked = false;
    public override void _Ready()
    {
        base._Ready();
        SignalBus.Instance.Connect(SignalBus.SignalName.DoorUnlocked, Callable.From<string>(OpenTheDoor));
    }
    private void OpenTheDoor(string lockType)
    {
        if (howToUnlock == lockType)
        {
            doorIsUnlocked = true;
        }
        
        if (door != null)
            door.Disabled = doorIsUnlocked;
        
        
    }
    public override void OnRoomChangerEntered(Node2D body)
    {
        if (doorIsUnlocked)
            base.OnRoomChangerEntered(body);
        else
            DialogueManager.ShowDialogueBalloon(dialogue, theTypeOfLock);
    }
}
