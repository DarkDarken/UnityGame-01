using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatShield : MonoBehaviour {

	[SerializeField] Transform player;
	[SerializeField] GameObject bubbleShield;
	[SerializeField] AudioController bubbleSound;
	[SerializeField] Slider slider;
	[SerializeField] float LifeTimeShield;

	private GameObject cloneBubble;
	private bool canSpawn = true;
	private float timeUnitNextSpawn;
	private float counter;
	private float progress;

	InputController playerInput;
	Destructable dest;

	void Awake(){
		playerInput = GameManager.Instance.InputController;
		bubbleShield.SetActive(false);
	}

	void Update(){
		if (canSpawn && playerInput.shieldActive) {
			bubbleShield.SetActive(true);
			cloneBubble = Instantiate (bubbleShield);
			bubbleShield.transform.position = player.position + new Vector3 (0, 0.5f, 0);
			canSpawn = false;
			timeUnitNextSpawn = 0.0f;
			slider.maxValue = 100;
			counter = LifeTimeShield;
			bubbleSound.Play ();

		}

		if (!canSpawn) {
			timeUnitNextSpawn += Time.deltaTime;
			counter -= Time.deltaTime;
			progress = Mathf.Clamp01(counter/LifeTimeShield)*100;
		}

		if (timeUnitNextSpawn >= LifeTimeShield) {
			canSpawn = true;
			Destroy (cloneBubble);
			bubbleShield.SetActive(false);
		}
	
		UpdateShield ();

	}
	void UpdateShield(){
		
		if(progress < 100 && counter != 0){
			
			slider.value = progress;
			print (progress);
		} else {
			slider.value = 0;
		}
	}
}
