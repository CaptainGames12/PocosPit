using DialogueManagerRuntime;
using Godot;
using System;

public partial class CutScenesController : AnimationPlayer
{
    [Export]
    private PocoAnimatronic poco;
    [Export]
    private Player player;
    [Export]
    private Resource dialogue;
    private int chargeCounter;
    public override void _Ready()
    {

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
        DialogueManager.ShowDialogueBalloon(dialogue, "waking_up", [this]);
    }
    public void FinishBeginningCutscene()
    {
        poco.GlobalPosition = new Vector2(29, 185);
        DialogueManager.ShowDialogueBalloon(dialogue, "end_of_the_cutscene", [this]);
    }
}
