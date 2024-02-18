using System;
using Godot;

public partial class tap_enemy : Area2D
{
	const int BPMMODIFIER = 2;
	AnimatedSprite2D anim;
	Tween tween;
	Timer timer, scoreTimer;
	AnimatedSprite2D Circle, TextScore, texture;
	Main main;
	test parent;
	bool IsDead = false;
	bool IsMouseOnMe = false;
	bool HasSpawned = false;
	bool Missed = false;
	int SongPos;
	double SecPerBeat;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Circle = GetNode<AnimatedSprite2D>("Circle");
		texture = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		TextScore = GetNode<AnimatedSprite2D>("Score Text");
		scoreTimer = GetNode<Timer>("Score Timer");
		
		TextScore.Hide();
		anim.Play("idle");
		Circle.Play("idle");
		main = GetNode<Main>("/root/Main");
		parent = (test)GetParent<Node2D>().GetParent<Node2D>();
		SongPos = parent.SongPosition;
		SecPerBeat = parent.secPerBeat;
		
		timer = new Timer
		{
			WaitTime = 3,
			OneShot = true,
			Autostart = true
		};
		
		Position = new Vector2(Position.X - 1000, Position.Y);
		tween = CreateTween();
		tween.TweenProperty(this, "position", new Vector2(Position.X + 1000,Position.Y), SecPerBeat*2 / BPMMODIFIER).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Back);
		tween.TweenProperty(Circle, "scale", new Vector2(1.5f, 1.5f), SecPerBeat * BPMMODIFIER * 6).SetEase(Tween.EaseType.Out).SetTrans(Tween.TransitionType.Linear);
		AddChild(timer);
		timer.Timeout += OnTimerTimeout;
    }

	public override void _PhysicsProcess(double delta){
		// Free enemy entity if it's out of the screen
		if (Math.Abs(Position.X) > 1200 || Math.Abs(Position.Y) > 950)
		{
			QueueFree();
		}

		// If the Circle goes smaller than the enemy, hide it
		if (Circle.Scale.X < 2.8f && !Missed)
		{
			Circle.Hide();
			main.ResetCombo();
			Missed = true;
			texture.Modulate = new Color(0,0,0,117);
			TextScore.Frame = 3;
			timer.Start();
			scoreTimer.Start();
			TextScore.Show();
		}

		if (TextScore.Visible)
		{
			TextScore.Position -= new Vector2(0, 1);
		}
	}	

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && IsMouseOnMe && mouseEvent.Pressed && !IsDead && Circle.Visible)
		{
			// Perfect hit
			if (Circle.Scale.X < 3.4f && Circle.Scale.X > 2.8f)
			{
				IsDead = true;
				main.Score += 200;
				main.Combo += 1;
				TextScore.Frame = 0;
				scoreTimer.Start();
				TextScore.Show();
				//GD.Print("Perfect hit " + Circle.Scale.X);
			}
			// Good hit
			else if (Circle.Scale.X < 4.5f && Circle.Scale.X > 3.4f)
			{
				IsDead = true;
				main.Score += 50;
				main.Combo += 1;
				TextScore.Frame = 1;
				scoreTimer.Start();
				TextScore.Show();
				//GD.Print("Good hit" + Circle.Scale.X);
			}
			// Bad hit
			else if (Circle.Scale.X < 5f)
			{
				IsDead = true;
				main.Score += 5;
				main.ResetCombo();
				TextScore.Frame = 2;
				scoreTimer.Start();
				TextScore.Show();
				//GD.Print("Bad hit" + Circle.Scale.X);
			}	
			// Missed hit
			else
			{
				IsDead = false;
				//GD.Print("Missed hit" + Circle.Scale.X);
				return;
			}

			anim.Play("die");
			tween.Stop();
			Circle.Hide();
		}
    }

	public void OnAnimFinished()
	{
		if (anim.Animation == "die")
		{
			QueueFree();
		}
	}

	public void OnMouseEnter()
	{
		IsMouseOnMe = true;
	}
	
	public void OnMouseExit()
	{
		IsMouseOnMe = false;
	}

	public void OnTimerTimeout()
	{
		if (!IsDead && !tween.IsValid()){
			tween = CreateTween();
			tween.TweenProperty(this, "position", new Vector2(Position.X + 2000,Position.Y), SecPerBeat).SetEase(Tween.EaseType.In).SetTrans(Tween.TransitionType.Expo);
		}
	}

	public void OnScoreTimerTimeout()
	{
		TextScore.Hide();
	}
}