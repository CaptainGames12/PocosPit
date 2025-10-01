using Godot;
using Godot.Collections;
using System;


public partial class DebugMode : CanvasLayer
{
	public static CheckButton godMode;
	public CheckButton nightVision;
	bool Pressed = false;
	public static bool isDebugMode = false;

	public override void _Ready()
	{
		godMode = GetNode<CheckButton>("/root/DebugMode/UI/GodMode");
		nightVision = GetNode<CheckButton>("/root/DebugMode/UI/NightVision");
		nightVision.Pressed += changeNight;
	}

	public override void _Process(double delta)
	{
		if (Input.IsKeyPressed(Key.J) && Pressed == false)
		{
			GD.Print("DEBUG MODE");
			Pressed = true;
		}
		else if (!Input.IsKeyPressed(Key.J) && Pressed == true)
		{
			Pressed = false;
		}
	}

	public static bool getGodMode()
	{
		return godMode.ButtonPressed;
	}

	public void changeNight()
	{
		CanvasModulate _canvasModulate = GetNode<CanvasModulate>("/root/Pizzeria/CanvasModulate");
		if (_canvasModulate != null)
		{
			_canvasModulate.Visible = !nightVision.ButtonPressed;
		}
	}
}
