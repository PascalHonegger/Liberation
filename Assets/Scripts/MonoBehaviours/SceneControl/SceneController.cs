using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This script exists in the Persistent scene and manages the content
// based scene's loading.  It works on a principle that the
// Persistent scene will be loaded first, then it loads the scenes that
// contain the player and other visual elements when they are needed.
// At the same time it will unload the scenes that are not needed when
// the player leaves them.
public class SceneController : MonoBehaviour
{
	public CanvasGroup faderCanvasGroup; // The CanvasGroup that controls the Image used for fading to black.
	public float fadeDuration = 1f; // How long it should take to fade to and from black.
	public string startingSceneName = "MainMenu";
	// The name of the scene that should be loaded first.

	public Image logoImage;

	private bool isFading; // Flag used to determine if the Image is currently fading to or from black.


	private IEnumerator Start()
	{
		// Set the initial alpha to start off with a black screen.
		logoImage.enabled = false;
		faderCanvasGroup.alpha = 1f;

		// Start the first scene loading and wait for it to finish.
		yield return StartCoroutine(LoadSceneAndSetActive(startingSceneName));

		// Once the scene is finished loading, start fading in.
		StartCoroutine(FadeWithoutLogo(0f, 1f));
	}


	public void FadeAndLoadSceneByName(string sceneName, bool showLogo = true)
	{
		// If a fade isn't happening then start fading and switching scenes.
		if (!isFading)
		{
			StartCoroutine(FadeAndSwitchScenes(sceneName, showLogo));
		}
	}


	// This is the coroutine where the 'building blocks' of the script are put together.
	private IEnumerator FadeAndSwitchScenes(string sceneName, bool showLogo)
	{
		// Start fading to black and wait for it to finish before continuing.
		yield return StartCoroutine(showLogo ? Fade(1f) : FadeWithoutLogo(1f));

		// Unload the current active scene.
		yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);

		// Start loading the given scene and wait for it to finish.
		yield return StartCoroutine(LoadSceneAndSetActive(sceneName));

		// Start fading back in and wait for it to finish before exiting the function.
		yield return StartCoroutine(showLogo ? Fade(0f) : FadeWithoutLogo(0f));
	}


	private static IEnumerator LoadSceneAndSetActive(string sceneName)
	{
		// Allow the given scene to load over several frames and add it to the already loaded scenes (just the Persistent scene at this point).
		yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

		// Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
		Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

		// Set the newly loaded scene as the active scene (this marks it as the one to be unloaded next).
		SceneManager.SetActiveScene(newlyLoadedScene);
	}

	private IEnumerator FadeWithoutLogo(float finalAlpha)
	{
		return FadeWithoutLogo(finalAlpha, fadeDuration);
	}

	private IEnumerator FadeWithoutLogo(float finalAlpha, float effectiveFloatDuration)
	{
		logoImage.enabled = false;
		yield return Fade(finalAlpha, fadeDuration);

	}

	private IEnumerator Fade(float finalAlpha)
	{
		logoImage.enabled = true;
		return Fade(finalAlpha, fadeDuration);
	}


	private IEnumerator Fade(float finalAlpha, float effectiveFloatDuration)
	{
		// Set the fading flag to true so the FadeAndSwitchScenes coroutine won't be called again.
		isFading = true;

		// Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted.
		faderCanvasGroup.blocksRaycasts = true;

		// Calculate how fast the CanvasGroup should fade based on it's current alpha, it's final alpha and how long it has to change between the two.
		float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / effectiveFloatDuration;

		// While the CanvasGroup hasn't reached the final alpha yet...
		while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
		{
			// ... move the alpha towards it's target alpha.
			faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
				fadeSpeed * Time.deltaTime);

			// Wait for a frame then continue.
			yield return null;
		}

		// Set the flag to false since the fade has finished.
		isFading = false;

		// Stop the CanvasGroup from blocking raycasts so input is no longer ignored.
		faderCanvasGroup.blocksRaycasts = false;
	}
}