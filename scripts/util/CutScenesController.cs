using DialogueManagerRuntime;
using Godot;
using System;
using System.Threading.Tasks;

public partial class CutScenesController : AnimationPlayer
{
    [Export]
    private Node2D parents;
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
        DialogueManager.ShowDialogueBalloon(dialogue, "waking_up", [this]);
    }
    public void FinishBeginningCutscene()
    {
        parents.Visible = false;
        dayAudio.Stop();
        GD.Print("is_anim_finished");
        Globals.Instance.beginningCutsceneIsFinished = true;
        poco.GlobalPosition = new Vector2(617, 164);
        DialogueManager.ShowDialogueBalloon(dialogue, "end_of_the_cutscene", [this]);
        playerAnimTree.Active = true;
        
        ScarePlayer();
    }
    public async Task ScarePlayer()
    {
        player.isScared = true;
        await ToSignal(GetTree().CreateTimer(10), "timeout");
        DialogueManager.ShowDialogueBalloon(dialogue, "scared_dialogue", [this]);
        player.isScared = false;
    }
}
