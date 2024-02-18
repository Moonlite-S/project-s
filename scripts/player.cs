using Godot;

public partial class player : CharacterBody2D
{
	public const float JumpVelocity = -400.0f;
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimatedSprite2D anim;
	private PackedScene bulletRes;
	private CollisionShape2D collider;
	private Node2D Revolver, SubGun, Railgun;
	private Node bulletTrace;
	private Timer delay;
	private Tween jumpTween;
	private conductor conductor;
	private Vector2 velocity = new();
	private int currentWeapon = 0;
	private bool resetJumpHitBox = false;

	public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		Revolver = GetNode<Node2D>("Weapons").GetNode<Node2D>("Revolver");
		SubGun = GetNode<Node2D>("Weapons").GetNode<Node2D>("SubGun");
		Railgun = GetNode<Node2D>("Weapons").GetNode<Node2D>("Railgun");
		delay = GetNode<Node2D>("Weapons").GetNode<Timer>("Timer");
		bulletRes = ResourceLoader.Load<PackedScene>("res://entites/BulletTrace.tscn");
		conductor = (conductor)GetParent().GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		collider = GetNode<CollisionShape2D>("CollisionShape2D");
		jumpTween = CreateTween();

		jumpTween.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Update rotating weapons when drawn
		if (currentWeapon != 0)
		{
			switch (currentWeapon)
			{
				case 1:
					Revolver.LookAt(GetGlobalMousePosition());
					break;
				case 2:
					SubGun.LookAt(GetGlobalMousePosition());
					break;
				case 3:
					Railgun.LookAt(GetGlobalMousePosition());
					break;
			}
		}

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			jumpTween.TweenProperty(this, "position", new Vector2(Position.X, velocity.Y), 1.0f).SetEase(Tween.EaseType.OutIn);
		}
		// Handles Airbrone Animations
		else if (velocity.Y > 0)
		{
			anim.Play("falling");
		} 
		if (IsOnFloor() && !anim.IsPlaying())
		{
			anim.Play("run");
		}

		Velocity = velocity;	
		MoveAndSlide();
	}

    public override void _Input(InputEvent @event)
    {
		// Handle Sliding
		if (Input.IsActionJustPressed("slide") && IsOnFloor())
		{
			collider.Set("rotation", 90);
			collider.Set("position", new Vector2(1, 19));
			anim.Play("slide");
		}
		// Handle attacks
		else if (Input.IsActionJustPressed("attack") && IsOnFloor() && anim.Animation != "slide")
		{
			collider.Set("rotation", 0);
			collider.Set("position", new Vector2(1, 5));
			anim.Play("attack1");
		}
		// Handles Jumnping
		else if (@event.IsActionPressed("jump"))
		{
			collider.Set("rotation", 0);
			collider.Set("position", new Vector2(1, 5));
			anim.Play("jump");
		}

		// DEBUG: Die on command
		if (Input.IsActionJustReleased("die"))
		{
			collider.Set("rotation", 0);
			collider.Set("position", new Vector2(1, 5));
			anim.Play("die");
		}
	
		if (Input.IsActionJustPressed("left mouse click"))
		{
			// Change Weapons based on enemy click

			// Shoot
			bulletTrace = bulletRes.Instantiate();
			AddChild(bulletTrace);
			Revolver.Show();
			currentWeapon = 1;
			delay.Start();
		}
    }

	public void OnAnimFinished()
	{
		if (anim.Animation == "die")
		{
			QueueFree();
			GetTree().Quit();
		}
	}

	public void ChangeCollision(string set)
	{
		if (set == "slide")
		{
			collider.Set("rotation", 90);
			collider.Set("position", new Vector2(1, 19));
		}
		else if (set == "attack")
		{
			collider.Set("rotation", 0);
			collider.Set("position", new Vector2(1, 5));
		}
	}

	public void OnWeaponTimerTimeout()
	{
		switch(currentWeapon)
		{
			case 1:
				Revolver.Hide();
				break;
			case 2:
				SubGun.Hide();
				break;
			case 3:
				Railgun.Hide();
				break;
		}		
	}
}
