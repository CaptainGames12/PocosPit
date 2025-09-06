using Godot;
using System;

public partial class RabAnimatronic : AnimatronicBase
{
    [Export]
    private Timer deactivationTimer;
    public bool isDeactivated = false;
    public override void _Ready()
    {
        base._Ready();
        deactivationTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnDeactivationTimerTimeout));
    }
    public override void _PhysicsProcess(double delta)
    {
        if (!isDeactivated && Globals.Instance.panelsActivated>0)
            base._PhysicsProcess(delta);
    }
    public void Deactivate()
    {
        deactivationTimer.Start();
        isDeactivated = true;
        attackArea.SetDeferred("monitoring", false);
    }
    public void OnDeactivationTimerTimeout()
    {
        attackArea.SetDeferred("monitoring", true);
        isDeactivated = false;
    }
}
