using Godot;
using System;

public partial class WorkshopDoor : RoomChanger
{
    public override void OnRoomChangerEntered(Node2D body)
    {
        base.OnRoomChangerEntered(body);
        if (Globals.Instance.panelsActivated == 2)
        {
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayFinalCutscene);
        }
    }
}
