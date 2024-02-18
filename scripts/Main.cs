using Godot;

// This is the main node, which tracks and controls everything in the game.
public partial class Main : Node
{
	public Node CurrentScene { get; set; }
	private Viewport root;

	public int Score { get; set; }
	public int Combo { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		root = GetTree().Root;
		CurrentScene = root.GetChild(root.GetChildCount() - 1);
		GD.Print(CurrentScene.Name);
	} 

	public void GoToScene(string path){
		// CallDeffered is used to ensure no code is executed when we switch scenes.
		CallDeferred(MethodName.DefferredGoToScene, path);
	}

	public void DefferredGoToScene(string path){
		// It is now safe to remove the current scene.
		CurrentScene.Free();

		// Load a new scene.
		var nextScene = GD.Load<PackedScene>(path);

		// Instance the new scene.
		CurrentScene = nextScene.Instantiate();

		// Add it to the active scene, as child of root.
		GetTree().Root.AddChild(CurrentScene);

		// Optionally, to make it compatible with the SceneTree.change_scene_to_file() API.
		GetTree().CurrentScene = CurrentScene;
	}

	public void ResetCombo(){
		// Reset the combo
		Combo = 0;
	}
}
