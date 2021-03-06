﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem {

	[SerializeField] EWeaponType weaponType;
	[SerializeField] float respawnTime;
	[SerializeField] int amount;
	[SerializeField] AudioController pickUpSound;

	public override void OnTriggerEnter(Collider collider){
		if (collider.tag != "Player") 
			return;
	
		pickUpSound.Play ();
		Pickup (collider.transform);
	}
		
	public override void OnPickup (Transform item){
		var playerInventory = item.GetComponentInChildren<Container> ();
		GameManager.Instance.Respawner.Despawn (gameObject, respawnTime);
		playerInventory.Put (weaponType.ToString (), amount);
		item.GetComponent<Player> ().PlayerShoot.ActiveWeapon.reloader.HandleOnAmmoChanged();
	}
}
