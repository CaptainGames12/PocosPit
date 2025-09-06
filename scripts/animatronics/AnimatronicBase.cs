using Godot;
[GlobalClass]
public partial class AnimatronicBase : CharacterBody2D
{
    [Export]
    private AudioStreamPlayer2D angrySound;
    [Export]
    private AudioStreamPlayer2D stepsSound;
    [Export]
    public Area2D attackArea;
    [Export]
    private Timer angryTimer;
    [Export]
    private Area2D targetArea;
    [Export]
    public bool isAngry = false;
    [Export]
    private Player player;
    [Export]
    private PathFollow2D pathFollow;
    [Export]
    private int angrySpeed;
    [Export]
    private int normalSpeed;
    [Export]
    private NavigationAgent2D navAgent;
    [Export]
    private string screamerScene;
    public override void _Ready()
    {
        attackArea.Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnAttackAreaEntered));
        angryTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnAngryTimerTimeout));
        targetArea.Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnTargetAreaEntered));
    }
    public void SpawnScreamer()
    {
        GetTree().ChangeSceneToFile("res://cut-scenes/screamers/" + screamerScene + ".tscn");
    }
    public override void _PhysicsProcess(double delta)
    {
        int chosenSpeed;
        if (Globals.Instance.isCutSceneGoing)
        {
            if (stepsSound.Playing)
                stepsSound.Stop();
            return;
        }
        else if (!stepsSound.Playing)
        {
            stepsSound.Play();
        }
        targetArea.Monitoring = !Globals.Instance.isCutSceneGoing;
        attackArea.Monitoring = !Globals.Instance.isCutSceneGoing;

        if (isAngry)
        {
            navAgent.TargetPosition = player.GlobalPosition;
            chosenSpeed = angrySpeed;
            if (!angrySound.Playing)
                angrySound.Play();
        }
        else
        {
            if (angrySound.Playing)
                angrySound.Stop();
            navAgent.TargetPosition = pathFollow.GlobalPosition;
            chosenSpeed = normalSpeed;
        }
        Vector2 dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
        Velocity = dir * chosenSpeed;
        MoveAndSlide();
    }
    public void OnTargetAreaEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
        {
            targetArea.SetDeferred("monitoring", false);
            angryTimer.Start();
            isAngry = true;
        }
    }
    public void OnAttackAreaEntered(Node2D body)
    {
        if (body.IsInGroup("player"))
            CallDeferred("SpawnScreamer");
    }
    public void OnAngryTimerTimeout()
    {
        targetArea.SetDeferred("monitoring", true);
        isAngry = false;
    }
}
