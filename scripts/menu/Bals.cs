using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Bals : Node2D
{
	[Export]
	private Texture2D[] ballsTexture;

	private List<Sprite2D> balls = new List<Sprite2D>();
	private List<Sprite2D> removeBallsList = new List<Sprite2D>();
	public override void _Ready()
	{
		Timer timer;
		timer = new Timer();
		AddChild(timer);

		timer.WaitTime = 0.1f;
		timer.Autostart = true;
		timer.OneShot = false;
		timer.Start();

		timer.Timeout += tick;
	}

	public void tick()
	{
		spawnBalls();
	}

	public void spawnBalls()
	{
		Random random = new Random();
		Sprite2D sprite2D = new Sprite2D();
		sprite2D.Texture = ballsTexture[random.Next(0, 5)];
		sprite2D.Scale = new Vector2(0.6f, 0.6f);
		sprite2D.Position = new Vector2(410.759f, random.Next(0, 200));

		AddChild(sprite2D);
		balls.Add(sprite2D);
	}
	public override void _Process(double delta)
	{
		foreach (Sprite2D sprite2D in balls)
		{
			sprite2D.Position += new Vector2(-0.7f, 0f);

			if (sprite2D.Position.X <= -200)
			{
				removeBallsList.Add(sprite2D);
			}
		}

		removeBalls();
	}

	public void removeBalls()
	{
		foreach (Sprite2D sprite2D in removeBallsList)
		{
			if (sprite2D != null && sprite2D.IsInsideTree())
			{
				sprite2D.QueueFree();
			}
		}
		
		balls.RemoveAll(ball => removeBallsList.Contains(ball));
		removeBallsList.Clear();
	}

}
