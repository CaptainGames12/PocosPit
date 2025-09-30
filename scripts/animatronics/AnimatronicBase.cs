using Godot;
[GlobalClass]
public partial class AnimatronicBase : CharacterBody2D
{
    [Export]
    public AnimatedSprite2D animation;
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
    public int angrySpeed;
    [Export]
    private int normalSpeed;
    [Export]
    public NavigationAgent2D navAgent;
    [Export]
    private string screamerScene;
    public override void _Ready()
    {
        SignalBus.Instance.Connect(SignalBus.SignalName.LightsOn, Callable.From(() => angrySound.Stop()));
        attackArea.Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnAttackAreaEntered));
        angryTimer.Connect(Timer.SignalName.Timeout, Callable.From(OnAngryTimerTimeout));
        targetArea.Connect(Area2D.SignalName.BodyEntered, Callable.From<Node2D>(OnTargetAreaEntered));
        SignalBus.Instance.Connect(SignalBus.SignalName.NightStarted, Callable.From(()=>animation.Animation = "broken"));
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
        else if (!stepsSound.Playing && !animation.IsPlaying())
        {
            animation.SpriteFrames.SetAnimationLoop("broken", true);
            animation.Play();
            GD.Print(Name);
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
        
        if (Velocity.X > 0)
        {
            animation.FlipH = true;
        }
        else if (Velocity.X < 0)
        {
            animation.FlipH = false;
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
        if (body.IsInGroup("player") && !Globals.Instance.isCutSceneGoing)
            CallDeferred("SpawnScreamer");
    }
    public void OnAngryTimerTimeout()
    {
        targetArea.SetDeferred("monitoring", true);
        isAngry = false;
    }
 
}
