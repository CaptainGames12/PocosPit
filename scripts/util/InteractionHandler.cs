using Godot;
using System;
using System.Runtime.InteropServices.Marshalling;
[GlobalClass]
public partial class InteractionHandler : Area2D
{
    [Export]
    private Sprite2D interactIndicator;
    public bool isPlayerNearInteractableItem = false;
    public override void _Ready()
    {
        Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnPlayerEntered));
        Connect(Area2D.SignalName.BodyExited, Callable.From<Node2D>(OnPlayerExited));

    }
    public override void _Process(double delta)
    {
        interactIndicator.Visible = isPlayerNearInteractableItem;
    }
    public virtual void OnPlayerEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            isPlayerNearInteractableItem = true;

        }
    }
    public virtual void OnPlayerExited(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            isPlayerNearInteractableItem = false;
            
        }
        
    }
}
