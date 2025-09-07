using Godot;
using System;

public partial class UiControl : CanvasLayer
{
    [Export]
    private Node2D map;
    [Export]
    private Sprite2D objectiveBack;
    [Export]
	private Label objectiveLabel;
	[Export]
	private ProgressBar staminaBar;
	[Export]
	private ColorRect loadingScreen;
	private Tween loadingScreenTween;
    private bool isMapCollected = false;
   
    public override void _Ready()
    {
        SignalBus.Instance.Connect(SignalBus.SignalName.ObjectiveUpdated, Callable.From<string>(UpdateObjective));
        SignalBus.Instance.Connect(SignalBus.SignalName.RoomChanged, Callable.From<string>(LoadingRoomInitialize));
        SignalBus.Instance.Connect(SignalBus.SignalName.PlayerInteractedWithItem, Callable.From<CollectableItems>(ShowMap));
    }
    public override void _Process(double delta)
    {
        staminaBar.Visible = !Globals.Instance.isCutSceneGoing;
		staminaBar.Value = Globals.Instance.stamina;
        if (Input.IsActionJustPressed("open_map") && isMapCollected)
        {
            map.Visible = !map.Visible;
        }

	}
	private void LoadingRoomInitialize(string _roomName)
	{
		if (loadingScreenTween != null)
		{
			loadingScreenTween.Kill();
		}
		loadingScreenTween = GetTree().CreateTween();

		loadingScreen.SelfModulate = new Color(1, 1, 1, 1);
		loadingScreenTween.TweenProperty(loadingScreen, "self_modulate", new Color(1, 1, 1, 0), 2);
	}



    private void UpdateObjective(string objective)
    {
        objectiveBack.Visible = true;
        objectiveLabel.VisibleRatio = 0;
        Globals.Instance.currentObjective = objective;
        objectiveLabel.Text = "Objective: " + objective;
        Tween tween = GetTree().CreateTween();
        tween.TweenProperty(objectiveLabel, "visible_ratio", 1, 1);
    }
    private void ShowMap(CollectableItems note)
    {
        if (note.Name == "Map")
        {
            map.Visible = true;
            
            isMapCollected = true;
        }
        
    }

}
