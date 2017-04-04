using System.Collections.Generic;
using UnityEngine;

public class OnClickLoadStart : MonoBehaviour
{
	public string SceneName = "NavMeshExample";

	public void OnClick()
	{
		FindObjectOfType<SceneController>().FadeAndLoadSceneByName(SceneName);
	}
}