using Godot;
using System;

public partial class SignalBus : Node
{
    [Signal]
    public delegate void RoomChangedEventHandler(string newSceneName);
    [Signal]
    public delegate void NumpadButtonPressedEventHandler(string btnName);
    [Signal]
    public delegate void PlayerInteractedWithNumpadEventHandler(bool isNearNumpad);
    [Signal]
    public delegate void DoorUnlockedEventHandler(string type_of_lock);
    [Signal]
    public delegate void ObjectiveUpdatedEventHandler(string objective);
    [Signal]
    public delegate void PlayerInteractedWithItemEventHandler(Node2D note, bool isPlayerNearInteractableItem);
    [Signal]
    public delegate void NightStartedEventHandler();
    [Signal]
    public delegate void PlayFinalCutsceneEventHandler();
    [Signal]
    public delegate void LightsOnEventHandler();
    public static SignalBus Instance;
    public override void _Ready()
    {
        Instance = this;
    }
}
