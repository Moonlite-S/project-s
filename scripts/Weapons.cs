using Godot;
using System;

public partial class Weapons : Node2D
{
	Sprite2D texture;
	Node2D Revolver, SubGun, Railgun;
	PackedScene bulletRes;
	Node bulletTrace;
	
	Timer delay;
	player player;
	Area2D tapEnemy;

	[Signal]
	public delegate void OnWeaponSpawnEventHandler(int weaponType);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		texture = GetNode<Sprite2D>("Texture");
		player = (player)GetParent();

		Revolver = GetNode<Node2D>("Weapons").GetNode<Node2D>("Revolver");
		SubGun = GetNode<Node2D>("Weapons").GetNode<Node2D>("SubGun");
		Railgun = GetNode<Node2D>("Weapons").GetNode<Node2D>("Railgun");

		bulletRes = ResourceLoader.Load<PackedScene>("res://entites/BulletTrace.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		texture.LookAt(GetGlobalMousePosition());
	}

    public override void _Input(InputEvent @event)
    {
       if (Input.IsActionJustPressed("left mouse click"))
	   {
			EmitSignal("OnWeaponSpawn", 1);
			// Shoot
			bulletTrace = bulletRes.Instantiate();
			AddChild(bulletTrace);
			Show();
			delay.Start();
	   }
    }

	public void OnDelayTimeout()
	{
		Hide();
	}
}
