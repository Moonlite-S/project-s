using Godot;

public partial class parallaxBG : ParallaxLayer
{
	[Export]
	private float Speed = 1.0f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		MotionOffset += new Vector2(Speed, 0);
	}
}