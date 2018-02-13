using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPickup : PickupItem {

	[SerializeField] AudioController mediPackSound;
	[SerializeField] int amount;
	[SerializeField] Text text;
	[SerializeField] Container inventory;

	private int value = 0;

	System.Guid containerItemId;

	void Awake(){
		containerItemId = inventory.Add ("MediPack", amount);
	}

	public override void OnTriggerEnter(Collider collider){
		if (collider.tag != "Player") 
			return;
		
		mediPackSound.Play ();
		Pickup (collider.transform);
	}
	public override void OnPickup (Transform item){
		GameManager.Instance.Respawner.Despawn (gameObject, 15f);
		inventory.Add ("MediPack", amount);
		print (containerItemId);
		value++;
		text.text = string.Format ("{0}", value);
	}
}