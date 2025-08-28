using Godot;
using System;

public partial class UiControl : CanvasLayer
{
    [Export]
    public ProgressBar staminaBar;
    public override void _Process(double delta)
    {
        staminaBar.Value = Globals.Instance.stamina;
    }
}
