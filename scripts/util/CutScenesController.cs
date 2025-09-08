using DialogueManagerRuntime;
using Godot;
using System;
using System.Threading.Tasks;

public partial class CutScenesController : AnimationPlayer
{
	[Export]
	private CollectableItems keyNote;
	[Export]
	private RabAnimatronic rab;
	[Export]
	private Resource finalDialogue;
	[Export]
	private Node2D parents;
	[Export]
	private AudioStreamPlayer nightAudio;
	[Export]
	private AudioStreamPlayer dayAudio;
	[Export]
	private AnimationPlayer playerAnimations;
	[Export]
	private PocoAnimatronic poco;
	[Export]
	private Player player;
	[Export]
	private Resource dialogue;
	[Export]
	private AnimationTree playerAnimTree;
	[Export]
	private CanvasModulate canvasModulate;
	private int chargeCounter;
	public override void _Ready()
	{
		player.Rotation = 0;
		parents.Visible = true;
		SignalBus.Instance.Connect(SignalBus.SignalName.PlayFinalCutscene, Callable.From(() => Play("final")));
		if (!Globals.Instance.beginningCutsceneIsFinished)
		{
			StartBeginningCutscene();

		}
		else
		{
			FinishBeginningCutscene();
			player.GlobalPosition = new Vector2(789, 174);
			canvasModulate.Visible = true;
			dayAudio.Playing = false;
			//Play("waking_up_animation");
		}


	}
	private void StartBeginningCutscene()
	{
		playerAnimTree.Active = false;
		playerAnimations.CurrentAnimation = "idle_left_beginning";
		Globals.Instance.isCutSceneGoing = true;
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.RoomChanged, "Reception");
		player.GlobalPosition = new Vector2(-444, 343);
		DialogueManager.ShowDialogueBalloon(dialogue, "start", [this]);
	}
	private void ShowTransitionText()
	{
		DialogueManager.ShowDialogueBalloon(dialogue, "after_some_time", [this]);
	}
	public void PlayWalkingInTheDiningRoom()
	{


		Play("walking_in_the_dining_room");
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.RoomChanged, "DiningRoom");
	}
	public void PlayJumpAnimation()
	{
		DialogueManager.ShowDialogueBalloon(dialogue, "goodman_tired", [this]);
	}
	public void PlayWakeUp()
	{
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.NightStarted);
		DialogueManager.ShowDialogueBalloon(dialogue, "waking_up", [this]);
	}
	public void FinishBeginningCutscene()
	{
		keyNote.Visible = true;
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.NightStarted);
		parents.Visible = false;
		dayAudio.Stop();
		nightAudio.Play();
		GD.Print("is_anim_finished");
		Globals.Instance.beginningCutsceneIsFinished = true;
		rab.GlobalPosition = new(913, 349);
		poco.GlobalPosition = new(689, 174);
		DialogueManager.ShowDialogueBalloon(dialogue, "end_of_the_cutscene", [this]);
		playerAnimTree.Active = true;

		ScarePlayer();
	}
	public async Task ScarePlayer()
	{
		player.isScared = true;

		poco.angrySpeed = 0;
		await ToSignal(GetTree().CreateTimer(3), "timeout");
		poco.angrySpeed = 100;

		await ToSignal(GetTree().CreateTimer(8), "timeout");
		DialogueManager.ShowDialogueBalloon(dialogue, "scared_dialogue", [this]);
		player.isScared = false;
		poco.angrySpeed = 180;
	}
	public void ShowFinalCutscene()
	{
		Globals.Instance.isCutSceneGoing = true;
	}
	public void ShowFinalDialogue()
	{
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.LightsOn);
		DialogueManager.ShowDialogueBalloon(finalDialogue, "start", [this]);
	}
	public void MovePlayerToPit()
	{
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.RoomChanged, "DiningRoom");
	}
	public void ShowTitles()
	{
		Globals.Instance.isCutSceneGoing = false;
		Globals.Instance.beginningCutsceneIsFinished = false;
		GetTree().ChangeSceneToFile("uid://baooqrd1p5b44");
	}
}
