using Godot;
using System;

public partial class parallaxPlatform : TileMap
{
    public override void _Ready()
    {

    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		Position += new Vector2(-0.20833333333333334f * 16, 0);
	}
}
