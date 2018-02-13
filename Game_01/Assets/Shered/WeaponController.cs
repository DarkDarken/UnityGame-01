using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	[SerializeField] float weaponSwitchTime;

	[HideInInspector]
	public bool canFire;

	Shooter[] weapons;

	int currentWeaponIndex;

	Transform weaponHolder;

	public event System.Action<Shooter> OnWeaponSwitch;

	Shooter m_ActiveWeapon;
	public Shooter ActiveWeapon {
		get {
			return m_ActiveWeapon;
		}
	}

	void Awake() {
		canFire = true;
		weaponHolder = transform.Find("Weapons");
		weapons = weaponHolder.GetComponentsInChildren<Shooter> ();

		if (weapons.Length > 0)
			Equip (0);
	}

	void DeactiveWeapons(){
		for (int i = 0; i < weapons.Length; i++) {
			weapons [i].gameObject.SetActive (false);
			weapons [i].transform.SetParent (weaponHolder);
		}
	}

	internal void SwitchWeapon(int direction) {

		canFire = false;
		currentWeaponIndex += direction;

		if (currentWeaponIndex > weapons.Length - 1)
			currentWeaponIndex = 0;

		if (currentWeaponIndex < 0)
			currentWeaponIndex = weapons.Length - 1;

		GameManager.Instance.Timer.Add (() => {
			Equip (currentWeaponIndex);
		}, weaponSwitchTime);
	}

	internal void Equip (int index){
		DeactiveWeapons ();
		canFire = true;
		m_ActiveWeapon = weapons [index];
		m_ActiveWeapon.Equip ();
		weapons [index].gameObject.SetActive (true);
		if (OnWeaponSwitch != null)
			OnWeaponSwitch (m_ActiveWeapon);
	}
}
