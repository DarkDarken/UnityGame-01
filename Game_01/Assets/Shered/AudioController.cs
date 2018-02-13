using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

	[SerializeField] AudioClip[] clips;
	[SerializeField] float delayBetweenClips;

	bool canPlay;


	// Use this for initialization
	void Start () {
		
		canPlay = true;
		
	}

	public void Play(){

		if (!canPlay)
			return;

		GameManager.Instance.Timer.Add (() => {
			canPlay = true;
		}, delayBetweenClips);

		canPlay = false;

		int clipIndex = Random.Range (0, clips.Length);

		AudioClip clip = clips [clipIndex];

		AudioSource.PlayClipAtPoint (clip, transform.position);
	}

	public bool CanNotPlay(){
		return !canPlay;
	}
}
