using Godot;
using System;
using System.Collections.Generic;

public partial class ButtonPlay : Area2D
{
	[Export]
	public Sprite2D sprite2D;
	[Export]
	public PackedScene pizzeria;
	private bool inArea = false;
	public Vector2 fistPosition;
	public override void _Ready()
	{
		fistPosition = sprite2D.Position;
		InputEvent += inputEvent;
		MouseEntered += () => inArea = true;
		MouseExited += () =>inArea = false;

		Timer timer;
		timer = new Timer();
		AddChild(timer);

		timer.WaitTime = 0.1f;
		timer.Autostart = true;
		timer.OneShot = false;
		timer.Start();

		timer.Timeout += tick;
	}

	private void inputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				GetTree().ChangeSceneToPacked(pizzeria);
			}
		}
	}


	private void tick()
	{
		if (inArea)
		{
			sprite2D.Position = fistPosition;
			Random random = new Random();
			sprite2D.Position += new Vector2((float)(random.Next(-100, 100)) * .01f, (float)(random.Next(-20, 20)) * .01f);
		}
	}

}
