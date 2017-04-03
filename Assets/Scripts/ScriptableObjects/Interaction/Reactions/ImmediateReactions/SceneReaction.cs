// The SceneReaction is used to change between scenes.
// Though there is a delay while the scene fades out,
// this is done with the SceneController class and so
// this is just a Reaction not a DelayedReaction.

public class SceneReaction : Reaction
{
	public string sceneName; // The name of the scene to be loaded.
	public string startingPointInLoadedScene; // The name of the StartingPosition in the newly loaded scene.
	public SaveData playerSaveData; // Reference to the save data asset that will store the StartingPosition.


	private SceneController _sceneController;
	// Reference to the SceneController to actually do the loading and unloading of scenes.


	protected override void SpecificInit()
	{
		_sceneController = FindObjectOfType<SceneController>();
	}


	protected override void ImmediateReaction()
	{
		// Start the scene loading process.
		_sceneController.FadeAndLoadScene(this);
	}
}