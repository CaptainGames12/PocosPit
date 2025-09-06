using Godot;
using System;

public partial class PanelRoomDoor : RoomChanger
{
    [Export]
    private CollisionShape2D door;
    private bool doorIsUnlocked = false;
    public override void _Ready()
    {
        base._Ready();
        SignalBus.Instance.Connect(SignalBus.SignalName.NumLockOpened, Callable.From(OpenTheDoor));
    }
    private void OpenTheDoor()
    {
        doorIsUnlocked = true;
        door.Disabled = doorIsUnlocked;
    }
    public override void OnRoomChangerEntered(Node2D body)
    {
        if(doorIsUnlocked)
            base.OnRoomChangerEntered(body);
    }
}
