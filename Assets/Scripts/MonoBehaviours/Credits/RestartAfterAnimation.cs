using UnityEngine;

public class RestartAfterAnimation : MonoBehaviour {
	private void LoadMainMenu()
	{
		FindObjectOfType<SceneController>().FadeAndLoadSceneByName("MainMenu");
	}
}
