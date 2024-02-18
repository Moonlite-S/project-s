using Godot;
using System;

public partial class escConfirm : Control
{
	Main main;
	public override void _Ready()
	{
		
		main = GetNode<Main>("/root/Main");
	}
	public void OnYes()
	{
		GetTree().Paused = false;
		main.GoToScene("res://ui/MainMenu.tscn");
	}

	public void OnNo()
	{
		GetTree().Paused = false;
		Hide();
	}
}
