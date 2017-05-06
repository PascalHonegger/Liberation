using UnityEngine;

public class OnClickLoadStart : MonoBehaviour
{
	public string SceneName = "Introduction";

	public void OnClick()
	{
		FindObjectOfType<SceneController>().FadeAndLoadSceneByName(SceneName, false);
	}
}