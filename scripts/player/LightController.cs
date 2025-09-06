using Godot;
using System;

public partial class LightController : Node
{
    [Export]
    private Player player;
    [Export]
    private Area2D lightArea;
    [Export]
    private PointLight2D light;
    [Export]
    private Timer lightTimer;
    [Export]
    public AudioStreamPlayer2D chargingSound;
    public Vector2 directionOfPlayer;
    public float target;
    public Tween lightTween;

    public override void _Ready()
    {
        lightArea.Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnRabEnteredLightArea));
        lightTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnTimerTimeout));
    }
    public override void _Process(double delta)
    {
        if (Globals.Instance.isFlashlightDeactivated)
        {
            light.Visible = false;
        }
        else
        {
            light.Visible = true;
        }
        DirectLight();
        ChargeFlashlight();
    }
    private void DirectLight()
    {
        if (directionOfPlayer != Vector2.Zero)
        {
            if (directionOfPlayer.X > 0)
            {
                light.RotationDegrees = 0;
            }
            else if (directionOfPlayer.X < 0)
            {
                light.RotationDegrees = -180;
            }
        }
    }
    private void ChargeFlashlight()
    {
        if (Input.IsActionJustPressed("charge_flashlight") && !Globals.Instance.isFlashlightDeactivated)
        {
            player.isChargingLight = true;
            lightTimer.Start();
            chargingSound.Play();
            lightTween.Kill();
            lightTween = GetTree().CreateTween().SetParallel(true);
            float newEnergy = (float)Mathf.Lerp(light.Energy, 1.0, 0.3f);
            //Vector2 newScale = light.Scale.Lerp(Vector2.One, 0.3f);
            //lightTween.TweenProperty(light, "scale", newScale, 0.1);
            lightTween.TweenProperty(light, "energy", newEnergy, 0.1);
            lightTween.TweenProperty(light.GetChild(0), "energy", newEnergy, 0.1);
            
        }
        if (Input.IsActionJustReleased("charge_flashlight"))
        {
            player.isChargingLight = false;
        }
    }
    private void OnTimerTimeout()
    {
        lightTween = GetTree().CreateTween().SetParallel(true);
        //lightTween.TweenProperty(light, "scale", Vector2.Zero, 10);
        lightTween.TweenProperty(light, "energy", 0, 10);
        lightTween.TweenProperty(light.GetChild(0), "energy", 0, 10);
    }
    private void OnRabEnteredLightArea(Node2D body)
    {
        if (body is RabAnimatronic)
        {
            RabAnimatronic rab = body as RabAnimatronic;
            rab.Deactivate();
        }
    }
}

