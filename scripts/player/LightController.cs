using Godot;
using System;

public partial class LightController : Node
{
    [Export]
    public PointLight2D light;
    [Export]
    public Timer lightTimer;
    [Export]
    public AudioStreamPlayer2D chargingSound;
    public Vector2 directionOfPlayer;
    public float target;
    public Tween lightTween;
    
    public override void _Ready()
    {
        
        lightTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnTimerTimeout));
    }
    public override void _Process(double delta)
    {
        DirectLight();
        ChargeFlashlight();
    }
    private void DirectLight()
    {
        if (directionOfPlayer != Vector2.Zero)
        {
            if (directionOfPlayer.Y != 0 && directionOfPlayer.X != 0)
            {
                directionOfPlayer = new Vector2(0, directionOfPlayer.Y);

            }
            light.Rotation = directionOfPlayer.Angle();
        }
    }
    private void ChargeFlashlight()
    {
        if (Input.IsActionJustPressed("charge_flashlight"))
        {
            lightTimer.Start();
            chargingSound.Play();
            lightTween.Pause();
            light.Energy = (float)Mathf.Lerp(light.Energy, 1.0, 0.3);
            light.Scale = light.Scale.Lerp(Vector2.One, 0.3f);
        }
        
    }
    private void OnTimerTimeout()
    {
        lightTween = GetTree().CreateTween().SetParallel(true);
        lightTween.TweenProperty(light, "scale", Vector2.Zero, 10);
        lightTween.TweenProperty(light, "energy", 0, 10);

    }
}

