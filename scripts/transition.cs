using Godot;
using System;

public partial class transition : Control
{
	private Tween tween;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{		
		tween = CreateTween();

		tween.TweenProperty(GetNode<Sprite2D>("Sprite2D"), "position", new Vector2(25, 13), 2f).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Expo);
		tween.TweenProperty(GetNode<Sprite2D>("Sprite2D"), "position", new Vector2(25, 13), 2f).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Expo);
	}
}
