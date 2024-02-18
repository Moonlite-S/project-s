using Godot;
using System;

public partial class Winner : Control
{
	Main main;

	public override void _Ready()
	{
		main = GetNode<Main>("/root/Main");
	}
	public void OnMainMenuPressed()
	{
		main.GoToScene("res://ui/MainMenu.tscn");
	}

	public void OnExitPressed()
	{
		GetTree().Quit();
	}
}
