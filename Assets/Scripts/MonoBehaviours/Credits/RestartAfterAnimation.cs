using UnityEngine;

public class RestartAfterAnimation : MonoBehaviour {
	private void LoadMainMenu()
	{
		FindObjectOfType<Inventory>().Reset();
		FindObjectOfType<SceneController>().FadeAndLoadSceneByName("MainMenu");
	}
}
