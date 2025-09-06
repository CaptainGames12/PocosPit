using Godot;
using System;

public partial class NumpadAccess : InteractionHandler
{
    
    public override void OnPlayerEntered(Node2D body)
    {
        base.OnPlayerEntered(body);
        if (body.IsInGroup("player"))
        {
            
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithNumpad, isPlayerNearInteractableItem);
        }
    }
    public override void OnPlayerExited(Node2D body)
    {
        base.OnPlayerExited(body);
        if (body.IsInGroup("player"))
        {
           
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithNumpad, isPlayerNearInteractableItem);
        }

    }
}
