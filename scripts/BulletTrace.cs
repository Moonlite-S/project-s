using Godot;
using System;

public partial class BulletTrace : Node2D
{
	int speed {get; set;}
	Vector2 mousePos;
	Vector2 velocity = new Vector2(0, 0);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		mousePos = GetGlobalMousePosition();
		speed = 100;
		velocity = (mousePos - GlobalPosition).Normalized() * speed;
		LookAt(mousePos);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += velocity;

		// If the bullet goes too far, remove it from the scene.
		if (Math.Abs(Position.X) > 5000 || Math.Abs(Position.Y) > 5000 || Position > mousePos)
		{
			QueueFree();
		}
	}
}
