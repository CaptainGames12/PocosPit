using Godot;
using System;

public partial class UiControl : CanvasLayer
{
	[Export]
	private Label objectiveLabel;
	[Export]
	private ProgressBar staminaBar;
	[Export]
	private ColorRect loadingScreen;
	private Tween loadingScreenTween;
	[Export]
	private Sprite2D objectiveLabelUI;
	public override void _Ready()
	{
		SignalBus.Instance.Connect(SignalBus.SignalName.ObjectiveUpdated, Callable.From<string>(UpdateObjective));
		SignalBus.Instance.Connect(SignalBus.SignalName.RoomChanged, Callable.From<string>(LoadingRoomInitialize));
	}
	public override void _Process(double delta)
	{
		staminaBar.Visible = !Globals.Instance.isCutSceneGoing;

		staminaBar.Value = Globals.Instance.stamina;


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
		objectiveLabel.VisibleRatio = 0;
		objectiveLabel.Text = "Objective: " + objective;
		Tween tween = GetTree().CreateTween();
		tween.TweenProperty(objectiveLabel, "visible_ratio", 1, 1);
	}

}
