using Godot;
using System;

public partial class SignalBus : Node
{
    [Signal]
    public delegate void RoomChangedEventHandler(string newSceneName);
    public static SignalBus Instance;
    public override void _Ready()
    {
        Instance = this;
    }
}
