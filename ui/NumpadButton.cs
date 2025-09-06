using Godot;
using System;

public partial class NumpadButton : Button
{
	public override void _Ready()
	{
		Connect(Button.SignalName.Pressed, Callable.From(NumpadButtonPressed));
	}
	private void NumpadButtonPressed()
	{
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.NumpadButtonPressed, Name);
	}
}
