using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    private AudioStreamPlayer2D walkingSound;
    [Export]
    private AudioStreamPlayer2D runningSound;
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
    public PlayerState currentState = PlayerState.WALK;
    public bool isRunning = false;
    public bool isChargingLight = false;
    public bool isWalking = false;
    public bool isScared = false;
    public override void _PhysicsProcess(double delta)
    {

        HandleMovement(delta);
        //HandleSounds();
    }
    private void HandleMovement(double delta)
    {
        float x = Globals.Instance.isCutSceneGoing? 0 : Input.GetActionStrength("player_right") - Input.GetActionStrength("player_left");
        float y = Globals.Instance.isCutSceneGoing? 0 : Input.GetActionStrength("player_down") - Input.GetActionStrength("player_up");
        bool canPlayerRun = Input.IsActionPressed("player_run") && !fullyTired;
        float newSpeedMultiplier = canPlayerRun && Globals.Instance.stamina > 0 ? speedMultiplier : 1.0f;

        Vector2 direction = new Vector2(x, y).Normalized();
        if (direction != Vector2.Zero && canPlayerRun)
        {
            Globals.Instance.stamina = Mathf.Clamp(Globals.Instance.stamina - speedOfTiredness * (float)delta, 0, 100);
            currentState = PlayerState.RUN;
            isRunning = true;
            isWalking = false;
            if (Globals.Instance.stamina == 0)
            {
                fullyTired = true;
            }
        }
        else
        {
            currentState = PlayerState.WALK;
            isRunning = false;
            isWalking = true;
            Globals.Instance.stamina = Mathf.Clamp(Globals.Instance.stamina + speedOfRest * (float)delta, 0, 100);
            if (Globals.Instance.stamina == 100)
            {
                fullyTired = false;
            }
        }


        lightController.directionOfPlayer = direction;
        Velocity = direction * speed * newSpeedMultiplier;
        MoveAndSlide();
    }
    private void HandleSounds()
    {
        switch (currentState)
        {
            case PlayerState.RUN:
                runningSound.Play();
                walkingSound.Stop();
                break;
            case PlayerState.WALK:
                walkingSound.Play();
                runningSound.Stop();
                break;
        }

    }
}
public enum PlayerState
{
    IDLE,
    RUN,
    WALK
}
