using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public float speed;
    [Export]
    public float speedMultiplier;
    [Export]
    public LightController lightController;
    [Export]
    public int speedOfRest;
    [Export]
    public int speedOfTiredness;
    public bool fullyTired = false;
    public override void _PhysicsProcess(double delta)
    {
        float x = Input.GetActionStrength("player_right") - Input.GetActionStrength("player_left");
        float y = Input.GetActionStrength("player_down") - Input.GetActionStrength("player_up");
        bool canPlayerRun = Input.IsActionPressed("player_run") && !fullyTired;
        float newSpeedMultiplier = canPlayerRun && Globals.Instance.stamina > 0? speedMultiplier : 1.0f;

        Vector2 direction = new Vector2(x, y).Normalized();
        if (direction != Vector2.Zero && canPlayerRun)
        {
            Globals.Instance.stamina = Mathf.Clamp(Globals.Instance.stamina - speedOfTiredness * (float)delta, 0, 100);
            if (Globals.Instance.stamina == 0)
            {
                fullyTired = true;
            }
        }
        else
        {

            Globals.Instance.stamina = Mathf.Clamp(Globals.Instance.stamina + speedOfRest * (float)delta, 0, 100);
            if (Globals.Instance.stamina == 100)
            {
                fullyTired = false;
            }
        }
        lightController.directionOfPlayer = direction;
        Velocity = direction * speed * newSpeedMultiplier * (float)delta;
        MoveAndCollide(Velocity);
    }
}
