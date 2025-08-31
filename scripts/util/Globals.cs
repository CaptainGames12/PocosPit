using Godot;
using System;

public partial class Globals : Node
{
    public static Globals Instance;
    public float stamina = 100;
    public string lastRoom = "DiningRoom";
    public override void _Ready()
    {
        Instance = this;
    }
}
