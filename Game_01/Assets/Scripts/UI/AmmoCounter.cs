using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {

	[SerializeField] Text text = null;

	PlayerShoot playerShoot;
	WeaponReloader m_reloader;

	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}

	void HandleOnLocalPlayerJoined (Player player){
		playerShoot = player.PlayerShoot;
		playerShoot.OnWeaponSwitch += HandleOnWeaponSwitch;
	}

	void HandleOnWeaponSwitch (Shooter activeWeapon) {
		m_reloader = activeWeapon.reloader;
		m_reloader.OnAmmoChanged += HandleOnAmmoChanged;
		HandleOnAmmoChanged ();
	}

	void HandleOnAmmoChanged(){
		int amountInInventory = m_reloader.RoundsRemainingInInventory;
		int amountInClip = m_reloader.RoundsRemainingInClip;
		text.text = string.Format ("{0}/{1}", amountInClip, amountInInventory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
