using Godot;
using System;

public partial class credits : Control
{
	Main main;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		main = GetNode<Main>("/root/Main");
	}
	public void OnMainMenuPressed()
	{
		main.GoToScene("res://ui/MainMenu.tscn");
	}
}
