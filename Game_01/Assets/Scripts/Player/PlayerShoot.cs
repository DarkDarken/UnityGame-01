using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : WeaponController {

	void Update(){

		if (GameManager.Instance.InputController.MouseWheelDown)
			SwitchWeapon (1);

		if (GameManager.Instance.InputController.MouseWheelUp)
			SwitchWeapon (-1);

		if (GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.SPRINTING)
			return;

		if (!canFire)
			return;

		if (GameManager.Instance.InputController.Fire1) {
		    ActiveWeapon.Fire ();
		}
	}
}
