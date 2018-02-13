using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMenuButton : MonoBehaviour {

	[SerializeField] AudioClip clip;

	bool canPlay;

	void Start () {

		canPlay = true;

	}

	public void Play(){

		if (!canPlay)
			return;

		AudioSource.PlayClipAtPoint (clip, transform.position);
	}
}
