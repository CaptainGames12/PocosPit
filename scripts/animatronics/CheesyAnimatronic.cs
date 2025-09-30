using Godot;
using System;

public partial class CheesyAnimatronic : AnimatronicBase
{
    
    public bool isActivated = false;
    private bool crawling = false;
    public override void _PhysicsProcess(double delta)
    {
        if (isActivated)
            base._PhysicsProcess(delta);
    }
    public void ChangeCrawlingState()
    {
        crawling = !crawling;
        navAgent.SetNavigationLayerValue(1, !crawling);
        navAgent.SetNavigationLayerValue(2, crawling);
        animation.Play("crawling");
    }
}
