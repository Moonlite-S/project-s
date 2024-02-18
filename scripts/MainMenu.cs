using Godot;
using System;

public partial class MainMenu : Node
{
	PackedScene level;
	Main main;

    public override void _Ready()
    {
		main = GetNode<Main>("/root/Main");
        level = ResourceLoader.Load<PackedScene>("res://levels/test.tscn");
    }
    public void OnPlayButtonClick()
	{
		main.GoToScene(level.ResourcePath);
	}

	public void OnExitButtonClick()
	{
		GetTree().Quit();
	}

	public void OnCreditButtonClick()
	{
        level = ResourceLoader.Load<PackedScene>("res://ui/Credits.tscn");		
		main.GoToScene(level.ResourcePath);
	}
}
