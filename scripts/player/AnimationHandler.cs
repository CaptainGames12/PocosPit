using Godot;
using System;

public partial class AnimationHandler : Node
{
    [Export]
    private Player player;
    [Export]
    private AnimationTree animationTree;
    
    public override void _Ready()
    {

    }
    public override void _PhysicsProcess(double delta)

    {

        if (player.Velocity.X != 0)
        {
            animationTree.Set("parameters/ChargeFlashlightIdle/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/Idle/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/ChargeFlashlightOnRun/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/ChargeFlashlightOnWalk/blend_position", player.Velocity.Normalized().X);

            animationTree.Set("parameters/Run/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/Walk/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/WalkScared/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/IdleScared/blend_position", player.Velocity.Normalized().X);
            animationTree.Set("parameters/RunScared/blend_position", player.Velocity.Normalized().X);
        }
        //AnimationNodeStateMachinePlayback state_machine_playback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
        //GD.Print(state_machine_playback.GetCurrentNode());
    }
}
