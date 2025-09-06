using Godot;
using System;

public partial class PanelAccess : InteractionHandler
{
    [Export]
    private AudioStreamPlayer2D activateSound;
    private bool isActivated = false;
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!isActivated && Input.IsActionJustPressed("interact") && isPlayerNearInteractableItem)
        {
            activateSound.Play();
            isActivated = true;
            Globals.Instance.panelsActivated += 1;
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.ObjectiveUpdated, $"Activate electrical panels {Globals.Instance.panelsActivated}/2");
            GD.Print("interact");
        }
    }

}
