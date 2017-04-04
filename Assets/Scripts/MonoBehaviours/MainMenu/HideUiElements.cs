using System.Collections.Generic;
using UnityEngine;

public class HideUiElements : MonoBehaviour
{
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

	private void OnDestroy()
	{
		foreach (var o in _gameObjectsToHide)
		{
			o.SetActive(true);
		}
	}
}