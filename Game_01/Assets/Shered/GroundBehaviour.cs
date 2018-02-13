using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehaviour : MonoBehaviour {

	public List<GroundType> GroundTypes = new List<GroundType> ();
	public Player player;
	public string currentground;

	void Start(){
		setGroundType (GroundTypes [0]);
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		if (hit.transform.tag == "Sand")
			setGroundType (GroundTypes [1]);
		else
			setGroundType (GroundTypes [0]);
			
	}

	public void setGroundType(GroundType ground){
		if (currentground != ground.name)
			player.footSteps = ground.audioController;
		currentground = ground.name;
	}

[System.Serializable]
public class GroundType
{
		public string name;
		public AudioController audioController;

}

}
