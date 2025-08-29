using Godot;
using System;

public partial class NotAngryAnimatronicMoving : Path2D
{
    [Export]
    private int speed;
    [Export] 
    private PathFollow2D pathFollow;
    [Export]
    private AnimatronicBase animatronic;
    public override void _PhysicsProcess(double delta)
    {
        if (animatronic.isAngry == false)
        {
            animatronic.Position = animatronic.Position.Lerp(Vector2.Zero, 0.5f * (float)delta);
            pathFollow.ProgressRatio += speed * 0.01f * (float)delta;
        }

    }  


}
