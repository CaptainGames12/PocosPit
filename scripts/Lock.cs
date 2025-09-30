
using System;
using Godot;

public partial class Lock : Node2D
{
    [Export]
    private float maxRange = 10.0f;
    [Export]
    private float sensitivity = 0.003f;
    [Export]
    private Node2D keyHole;
    [Export]
    private Node2D pin;
    [Export]
    private Node2D screwDriver;
    private bool isTurningKeyhole = false;
    private bool isUnlocked = false;
    private float sweetSpot = 0.0f;
    private float sweetSpotRange = 1.0f;
    private float keyholeRotationSpeed = 4.0f;
    private const float MinRange = 0.0f;
    private const float SuccessZone = -90.0f;
    private float pinPos = 0.0f;
    public override void _Ready()
    {
        pinPos = maxRange / 2.0f;
        Input.MouseMode = Input.MouseModeEnum.Captured;
        PlaceSweetSpot();
        GD.Print(sweetSpot);
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            if (!isUnlocked && !isTurningKeyhole)
            {
                pin.Rotate(mouseMotion.Relative.X * sensitivity);
                pin.Rotation = Mathf.Clamp(pin.Rotation, Mathf.DegToRad(-90.0f),  Mathf.DegToRad(90.0f));
            }
        }
    }
    public override void _PhysicsProcess(double delta)
    {
        pinPos = (float)Mathf.Snapped(Mathf.Remap(Mathf.RadToDeg(pin.Rotation), -90.0f, 90.0f, MinRange, maxRange), 0.1f);
        if (!isUnlocked)
            RotateKeyhole(delta);
    }
    private void PlaceSweetSpot()
    {
        RandomNumberGenerator rng = new();
        sweetSpot = Mathf.Snapped(rng.RandfRange(MinRange, maxRange), 0.1f);
    }
    private void RotateKeyhole(double delta)
    {
        if (Input.IsPhysicalKeyPressed(Key.Y))
        {
            keyHole.Rotate(-keyholeRotationSpeed * (float)delta);
            isTurningKeyhole = true;
        }
        else
        {
            keyHole.Rotation = Mathf.LerpAngle(keyHole.Rotation, 0.0f, (float)delta * keyholeRotationSpeed);
            isTurningKeyhole = false;
        }
        float distance = Math.Abs(pinPos - sweetSpot);
        
        float gradLock = Mathf.Snapped(Mathf.Remap(distance, sweetSpotRange, 0f, 0f, SuccessZone), 0.1f);
       
        keyHole.RotationDegrees = Mathf.Clamp(keyHole.RotationDegrees, Mathf.Clamp(gradLock, -90.0f, 0.0f), 0);
        if (Mathf.RadToDeg(keyHole.Rotation) <= SuccessZone && !isUnlocked)
        {
            isUnlocked = true;
            GD.Print("unlocked");
        }
    }
}
