using Godot;
using System;

public partial class conductor : AudioStreamPlayer
{
	[Export]
	public int bpm = 90;
	[Export]
	public int measures = 8;
	[Export]
	public int offset = 0;

	// Tracks beat and song position
	public int songPosInBeats = 0;	// Use this to track the beat and position
	public int lastReportedBeat = 0;
	public int beatsBeforeStart = 0;
	public int measure = 1;
	public double secPerBeat;
	public double songPosition = 0.0;

	public int enemyType = 0;

	Timer timer;

	[Signal]
	public delegate void beatEventHandler(int position);
	[Signal]
	public delegate void measureBeatEventHandler(int position);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
		secPerBeat = 60.0 / bpm;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Playing){
			// Gets the song's position in seconds
			songPosition = GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
			songPosition -= AudioServer.GetOutputLatency();
			songPosInBeats = (int)Math.Floor(songPosition / secPerBeat) + beatsBeforeStart;
			ReportBeat();
		}
	}

	// Reports the beat to the game and emits signals.
	public void ReportBeat(){
		if (lastReportedBeat < songPosInBeats){
			GD.Print("Song Position: " + songPosition + "\nInBeats: " + songPosInBeats + "\nMeasure: " + measure + "\n");
			if(measure >= measures){
				measure = 0;
			}
			
			lastReportedBeat = songPosInBeats;
			measure += 1;
			EmitSignal("measureBeat", measure);
			EmitSignal("beat", songPosInBeats);
		}
	}

	public void PlayWithBeatOffSet(int num){
		beatsBeforeStart = num;
		timer.WaitTime = secPerBeat;
		timer.Start();
		//GD.Print("Playing with beat offset of " + num + " beats");
	}

	public void OnTimerTimeout(){
		songPosInBeats += 1;
		if (songPosInBeats < beatsBeforeStart)
		{
			timer.Start();
		}
		else if(songPosInBeats == beatsBeforeStart - 1)
		{ 
			timer.WaitTime -= AudioServer.GetTimeSinceLastMix() + AudioServer.GetOutputLatency();
			timer.Start();
		}
		else
		{
			Play();
			timer.Stop();
		}
		ReportBeat();
	}
}
