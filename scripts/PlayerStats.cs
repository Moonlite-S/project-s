using Godot;
using Godot.Collections;
using System;

public partial class PlayerStats : Node
{
	// Any Constants here

	// Stats for Player
	Dictionary<string, string> stats = new Dictionary<string, string>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		stats.Add("Name", "S");
		stats.Add("Score", "0");
		// Just some examples
	}
}
