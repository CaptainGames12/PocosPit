using Godot;
using System;

public partial class SignalBus : Node
{
    
    public static SignalBus Instance;
    public override void _Ready()
    {
        Instance = this;
    }
}
