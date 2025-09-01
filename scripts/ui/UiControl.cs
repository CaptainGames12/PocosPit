using Godot;
using System;

public partial class UiControl : CanvasLayer
{
    [Export]
    private ProgressBar staminaBar;
    [Export]
    private ColorRect loadingScreen;
    private Tween loadingScreenTween;
    public override void _Ready()
    {

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
        loadingScreenTween.TweenProperty(loadingScreen, "self_modulate", new Color(1, 1, 1, 0), 1.5);
    }
}
