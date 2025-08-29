using Godot;
[GlobalClass]
public partial class AnimatronicBase : CharacterBody2D
{
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
    private int speed;
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
        if (this.isAngry)
        {
            navAgent.TargetPosition = player.GlobalPosition;
            Vector2 dir = ToLocal(navAgent.GetNextPathPosition()).Normalized();
            Velocity = dir * speed;
            MoveAndSlide();
        }

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
