using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

	public virtual void OnTriggerEnter(Collider collider){
		
	}

	public virtual void OnPickup (Transform item) {
		
	}

	public void Pickup(Transform item){
		OnPickup (item);
	}
}
