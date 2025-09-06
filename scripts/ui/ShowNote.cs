using Godot;
using System;

public partial class ShowNote : InteractionHandler
{
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (Input.IsActionJustPressed("interact") && isPlayerNearInteractableItem)
        {
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithNote, this);
        }
        else if (!isPlayerNearInteractableItem)
        {
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithNote, this);
        }
    }
}
