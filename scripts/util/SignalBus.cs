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
    public delegate void NumLockOpenedEventHandler();
    [Signal]
    public delegate void ObjectiveUpdatedEventHandler(string objective);
    [Signal]
    public delegate void PlayerInteractedWithNoteEventHandler(Node2D note);
    public static SignalBus Instance;
    public override void _Ready()
    {
        Instance = this;
    }
}
