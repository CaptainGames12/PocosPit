using Godot;
using System;

public partial class PanelAccess : InteractionHandler
{
    [Export]
    private Sprite2D switchSprite;
    [Export]
    private CompressedTexture2D switchOn;
    [Export]
    private AudioStreamPlayer2D activateSound;
    private bool isActivated = false;
    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!isActivated && Input.IsActionJustPressed("interact") && isPlayerNearInteractableItem)
        {
            switchSprite.Texture = switchOn;
            switchSprite.Position = new Vector2(6.0f, -23.0f);
            activateSound.Play();
            isActivated = true;
            Globals.Instance.panelsActivated += 1;
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.PlayerInteractedWithItem, "electricity");
            SignalBus.Instance.EmitSignal(SignalBus.SignalName.ObjectiveUpdated, $"Activate electrical panels {Globals.Instance.panelsActivated}/2");
            GD.Print("interact");
        }
    }

}
