using System.Collections.Generic;
using UnityEngine;

public class OnClickLoadStart : MonoBehaviour
{
	public string SceneName = "NavMeshExample";
	public string InvisibleInMainMenuTag = "InvisibleInMainMenu";

	private IEnumerable<GameObject> _gameObjectsToHide;

	private void Start()
	{
		_gameObjectsToHide = GameObject.FindGameObjectsWithTag(InvisibleInMainMenuTag);

		foreach (var o in _gameObjectsToHide)
		{
			o.SetActive(false);
		}
	}

	public void OnClick()
	{
		FindObjectOfType<SceneController>().FadeAndLoadSceneByName(SceneName);
	}

	private void OnDestroy()
	{
		foreach (var o in _gameObjectsToHide)
		{
			o.SetActive(true);
		}
	}
}