using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoader : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;
	public Text progressTextLoader;

	public void LoadLevel (int sceneIndex)
	{
		StartCoroutine (LoadAsynchronously (sceneIndex));
	}

	IEnumerator LoadAsynchronously (int sceneIndex){
		AsyncOperation operation =	SceneManager.LoadSceneAsync (sceneIndex);

		loadingScreen.SetActive (true);

		while (!operation.isDone) {

			float progress = Mathf.Clamp01 (operation.progress / 0.9f);

			slider.value = progress;
			progressTextLoader.text = Mathf.Round(progress * 100f) + "%";

			yield return null;
		}
	}
}
