using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenager : MonoBehaviour {

	public GameObject pausePanel;

	public bool isPaused;

	// Use this for initialization
	void Start () {
		isPaused = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (isPaused) {
			PauseGame (true);
		} else {
			PauseGame (false);
		}

		if (Input.GetKeyDown(KeyCode.Tab))
			switchPause ();
		
	}

	void PauseGame(bool state){
		if (state) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
		pausePanel.SetActive (state);
		Cursor.visible = state;
		Cursor.lockState = CursorLockMode.None;
		GameManager.Instance.LocalPlayer.PlayerShoot.canFire = !state;
	
	}

	public void switchPause(){
		if (isPaused) {
			isPaused = false;
		} else {
			isPaused = true;
		}
	}

	public void SetActivePanel(){
		pausePanel.SetActive (false);
	}

	public void QuitGame(){
		Debug.Log ("Quitting app");
		Application.Quit ();
	}
}
