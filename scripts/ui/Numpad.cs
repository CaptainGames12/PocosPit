using DialogueManagerRuntime;
using Godot;
using System;

public partial class Numpad : Sprite2D
{
    [Export]
    private AudioStreamPlayer correct;
    [Export]
    private AudioStreamPlayer incorrect;
    
    [Export]
    private Resource numpadDialogue;
    [Export]
    private Label label;
    private const string correctCode = "1987";
    private string currentCode = "";
    private bool isNearNumpad = false;
    public override void _Ready()
    {
        SignalBus.Instance.Connect(SignalBus.SignalName.NumpadButtonPressed, Callable.From<string>(NumpadButtonPressed));
        SignalBus.Instance.Connect(SignalBus.SignalName.PlayerInteractedWithNumpad, Callable.From<bool>(CheckIfPlayerIsNearNumpad));

    }
    private void CheckIfPlayerIsNearNumpad(bool isNearNumpad)
    {
        this.isNearNumpad = isNearNumpad;
    }
    public override void _Process(double delta)
    {
        if (isNearNumpad)
        {
            if (Input.IsActionJustPressed("interact"))
            {
                Visible = !Visible;
                DialogueManager.ShowDialogueBalloon(numpadDialogue, "numpad_note");
            }
        }
        else
        {
            Visible = false;
        }
    }
    public void NumpadButtonPressed(string name)
    {
        if (!Visible)
        {
            return;
        }
        if (name != "Enter" && name != "Clear" && currentCode.Length <= 3)
        {
            currentCode += name;

        }
        else if (name == "Enter")
        {
            if (currentCode == correctCode)
            {
                correct.Play();
                SignalBus.Instance.EmitSignal(SignalBus.SignalName.DoorUnlocked, "numpad");
            }
            else
            {
                incorrect.Play();
                GD.Print("Incorrect");
            }
        }
        else if (name == "Clear")
        {
            currentCode = "";
        }
        label.Text = currentCode;
    }
}
