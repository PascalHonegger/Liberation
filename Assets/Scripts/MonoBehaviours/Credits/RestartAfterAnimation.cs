using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartAfterAnimation : MonoBehaviour {
	private void LoadMainMenu()
	{
		FindObjectOfType<SceneController>().FadeAndLoadSceneByName("MainMenu");
	}
}
