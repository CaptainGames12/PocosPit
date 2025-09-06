using Godot;


public partial class Globals : Node
{

    public static Globals Instance;
    public string currentObjective = "";
    public bool beginningCutsceneIsFinished = false;
    public int panelsActivated = 0;
    public bool isFlashlightDeactivated = true;
    public bool isCutSceneGoing = false;
    public float stamina = 100;
    public string lastRoom = "DiningRoom";
    public bool keyIsCollected = false;
    public override void _Ready()
    {
        Instance = this;
    }
}
