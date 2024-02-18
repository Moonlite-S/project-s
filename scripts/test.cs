using System;
using Godot;

public partial class test : Node2D
{

	Label scoreLabel;
	ParallaxBackground bgfg;
	Main main;
	PackedScene enemy;
	AudioStreamPlayer song;
	Control escConfirm, win;
	conductor conductor;
	TileMap map;
	CharacterBody2D player;
	Node2D enemyGroup;
	public int SongPosition {get; set;}
	public int score, combo, SongPosInBeats;
	public double secPerBeat;
	private int enemySection = 0;	// Controls when enemies spawn

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SongPosition = 0;
		scoreLabel = GetNode<Label>("Score");
		main = GetNode<Main>("/root/Main");
		song = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		escConfirm = GetNode<Control>("escConfirm");
		win = GetNode<Control>("Winner");
		map = GetNode<TileMap>("Platform");
		player = GetNode<CharacterBody2D>("Player");
		enemyGroup = GetNode<Node2D>("Enemies");
		bgfg = GetNode<ParallaxBackground>("ParallaxBackgroundFG");

		conductor = (conductor)GetNode<AudioStreamPlayer>("AudioStreamPlayer");
		secPerBeat = conductor.secPerBeat;
		SongPosInBeats = conductor.songPosInBeats;
		conductor.PlayWithBeatOffSet(10);

		GD.Print(conductor.bpm);
		enemy = ResourceLoader.Load<PackedScene>("res://entites/tap_enemy.tscn");

		// For testing purposes
		// Should actually be reset some other way
		main.Score = 0;
		main.Combo = 0;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		scoreLabel.Text = "Score: " + main.Score + "\nCombo: " + main.Combo;
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey)
		{
			if (Input.IsActionJustPressed("esc"))
			{
				GetTree().Paused = true;
				escConfirm.Show();
			}
		}
    }

    // Spawn Enemies
    public void SpawnEnemy(int posX, int posY)
	{
		var newEnemy = enemy.Instantiate();

		newEnemy.Set("position", new Vector2(posX, posY));
		enemyGroup.AddChild(newEnemy);

	}
	

	// 8 Beats = 1 Measure, 16 Beats = 2 Measures, etc.
	// Notes take 16 beats to finish
	// Beats count eighth notes
	// (Must start on last beat of the measure before it)
	public void OnConductorBeat(int position)
	{
		SongPosInBeats = position;
		switch(SongPosInBeats)
		{
			case 16:
				enemySection = 1; break;
			case 40:
				enemySection = 2; break;
			case 72:
				enemySection = 3; break;
			case 88:
				enemySection = 4; break;
			case 104:
				enemySection = 5; break;
			case 120:
				enemySection = 6; break;
			case 136:
				enemySection = 7; break;
			case 152:
				enemySection = 8; break;
			case 168:
				enemySection = 9; break;
			case 184:
				enemySection = 10; break;
			case 200:
				enemySection = 11; break;
			case 216:
				enemySection = 12; break;
			case 232:
				enemySection = 13; break;
			case 248:
				enemySection = 14; break;
			case 264:
				enemySection = 15; break;
			case 280:
				enemySection = 16; break;
			case 296:
				enemySection = 17; break;
			case 312:
				enemySection = 18; break;
			case 328:
				enemySection = 19; break;
			case 344:
				enemySection = 20; break;
			case 360:
				enemySection = 21; break;
			case 376:
				enemySection = 22; break;
			case 392:
				enemySection = 23; break;
			case 408:
				enemySection = 24; break;
			case 424:
				enemySection = 25; break;
			case 440:
				enemySection = 26; break;
			// End

		}
			
		if (position >= 458)
		{
			bgfg.Hide();
			win.Show();
			win.GetNode<Label>("Label").Text = "You're Winner!\n" + "Score: " + main.Score + "\nCombo: " + main.Combo;
		}
	}

	// DO NOT READ THIS LMAO
	public void OnMeasureBeat(int position)
	{
		switch(enemySection)
		{
			case 1:
				if(position == 2)
					SpawnEnemy(500, 200);
				else if(position == 6)
					SpawnEnemy(900, 80);
				break;
				
			case 2:
				if(position == 2)
					SpawnEnemy(900, 200);
				else if (position == 6)
					SpawnEnemy(500, 400);
				break;
			case 3:
				if(position == 2)
					SpawnEnemy(400, 200);
				else if (position == 4)
					SpawnEnemy(600, 400);
				else if (position == 6)
					SpawnEnemy(800, 200);
				else if (position == 8)
					SpawnEnemy(1000, 400);
				break;
			case 4:
				if(position == 2)
					SpawnEnemy(400, 200);
				else if (position == 4)
					SpawnEnemy(600, 400);
				else if (position == 6)
					SpawnEnemy(800, 200);
				else if (position == 8)
					SpawnEnemy(1000, 400);
				break;
			case 5:
				if(position == 2)
					SpawnEnemy(600, 60);
				else if (position == 4)
					SpawnEnemy(600, 180);
				else if (position == 6)
					SpawnEnemy(600, 300);
				else if (position == 8)
					SpawnEnemy(600, 420);
				break;
			case 6:
				if(position == 2)
					SpawnEnemy(200, 200);
				else if (position == 4)
					SpawnEnemy(450, 200);
				else if (position == 6)
					SpawnEnemy(700, 200);
				else if (position == 8)
					SpawnEnemy(950, 200);
				break;
			case 7:
				if(position == 2)
					SpawnEnemy(180, 150);
				else if (position == 6)
					SpawnEnemy(900, 300);
				break;
			case 8:
				if(position == 2)
					SpawnEnemy(890, 170);
				else if (position == 6)
					SpawnEnemy(160, 400);
				break;
			case 9:
				if(position == 2)
					SpawnEnemy(250, 100);
				else if (position == 4)
					SpawnEnemy(250, 250);
				else if (position == 6)
					SpawnEnemy(250, 380);
				else if (position == 8)
					SpawnEnemy(250, 450);
				break;
			case 10:
				if(position == 2)
					SpawnEnemy(450, 100);
				else if (position == 4)
					SpawnEnemy(450, 200);
				else if (position == 6)
					SpawnEnemy(450, 300);
				else if (position == 8)
					SpawnEnemy(450, 450);
				break;
			case 11:
				if(position == 2)
					SpawnEnemy(160, 370);
				else if (position == 4)
					SpawnEnemy(280, 370);
				else if (position == 6)
					SpawnEnemy(430, 370);
				else if (position == 8)
					SpawnEnemy(530, 370);
				break;
			case 12:
				if(position == 2)
					SpawnEnemy(450, 100);
				else if (position == 4)
					SpawnEnemy(450, 200);
				else if (position == 6)
					SpawnEnemy(450, 350);
				else if (position == 8)
					SpawnEnemy(450, 450);
				break;
			case 13:
				if(position == 2)
					SpawnEnemy(60, 70);
				else if (position == 4)
					SpawnEnemy(200, 100);
				else if (position == 6)
					SpawnEnemy(320, 70);
				else if (position == 8)
					SpawnEnemy(400, 150);
				break;
			case 14:
				if(position == 2)
					SpawnEnemy(830, 80);
				else if (position == 4)
					SpawnEnemy(700, 180);
				else if (position == 6)
					SpawnEnemy(620, 230);
				else if (position == 8)
					SpawnEnemy(550, 330);
				break;
			case 15:
				if(position == 2)
					SpawnEnemy(1050, 450);
				else if (position == 4)
					SpawnEnemy(950, 450);
				else if (position == 6)
					SpawnEnemy(850, 450);
				else if (position == 8)
					SpawnEnemy(750, 450);
				break;
			case 16:
				if(position == 2)
					SpawnEnemy(780, 150);
				else if (position == 4)
					SpawnEnemy(200, 200);
				else if (position == 6)
					SpawnEnemy(970, 380);
				else if (position == 8)
					SpawnEnemy(200, 450);
				break;
			case 17:
				if(position == 2)
					SpawnEnemy(840, 150);
				else if (position == 6)
					SpawnEnemy(840, 324);
				break;
			case 18:
				if(position == 2)
					SpawnEnemy(600, 150);
				else if (position == 6)
					SpawnEnemy(600, 324);
				break;
			case 19:
				if (position == 1)
					SpawnEnemy(950, 200);
				if(position == 2)
					SpawnEnemy(850, 200);
				else if (position == 3)
					SpawnEnemy(750, 200);
				else if (position == 4)
					SpawnEnemy(650, 200);
				else if (position == 5)
					SpawnEnemy(550, 200);
				else if (position == 6)
					SpawnEnemy(450, 200);
				else if (position == 7)
					SpawnEnemy(350, 200);
				else if (position == 8)
					SpawnEnemy(250, 200);
				break;
			case 20:
				if (position == 1)
					SpawnEnemy(950, 300);
				if(position == 2)
					SpawnEnemy(850, 300);
				else if (position == 3)
					SpawnEnemy(750, 300);
				else if (position == 4)
					SpawnEnemy(650, 300);
				else if (position == 5)
					SpawnEnemy(550, 300);
				else if (position == 6)
					SpawnEnemy(450, 300);
				else if (position == 7)
					SpawnEnemy(350, 300);
				else if (position == 8)
					SpawnEnemy(250, 300);
				break;
			case 21:
				if(position == 2)
					SpawnEnemy(780, 150);
				else if (position == 4)
					SpawnEnemy(200, 200);
				else if (position == 6)
					SpawnEnemy(970, 380);
				else if (position == 8)
					SpawnEnemy(200, 450);
				break;
			case 22:
				if(position == 2)
					SpawnEnemy(220, 80);
				else if (position == 4)
					SpawnEnemy(800, 150);
				else if (position == 6)
					SpawnEnemy(270, 280);
				else if (position == 8)
					SpawnEnemy(800, 380);
				break;
			case 23:
				if (position == 1)
					SpawnEnemy(950, 300);
				if(position == 2)
					SpawnEnemy(850, 300);
				else if (position == 3)
					SpawnEnemy(750, 300);
				else if (position == 4)
					SpawnEnemy(650, 300);
				else if (position == 5)
					SpawnEnemy(550, 300);
				else if (position == 6)
					SpawnEnemy(450, 300);
				else if (position == 7)
					SpawnEnemy(350, 300);
				else if (position == 8)
					SpawnEnemy(250, 300);
				break;
			case 24:
				if (position == 1)
					SpawnEnemy(950, 200);
				if(position == 2)
					SpawnEnemy(850, 200);
				else if (position == 3)
					SpawnEnemy(750, 200);
				else if (position == 4)
					SpawnEnemy(650, 200);
				else if (position == 5)
					SpawnEnemy(550, 200);
				else if (position == 6)
					SpawnEnemy(450, 200);
				else if (position == 7)
					SpawnEnemy(350, 200);
				else if (position == 8)
					SpawnEnemy(250, 200);
				break;
			case 25:
				if(position == 2)
					SpawnEnemy(900, 200);
				else if (position == 6)
					SpawnEnemy(500, 400);
				break;
			case 26:
				if(position == 2)
					SpawnEnemy(500, 200);
				else if (position == 6)
					SpawnEnemy(900, 300);
				break;
		}

		if (position == 8)
		{
			enemySection = 0;
		}
	}
}
