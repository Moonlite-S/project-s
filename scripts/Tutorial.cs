using Godot;
using System;

public partial class Tutorial : Label
{
	public void OnTimerTimeout()
	{
		Hide();
	}
}
