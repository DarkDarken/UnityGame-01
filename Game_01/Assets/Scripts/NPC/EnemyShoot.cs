using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shered.Extension;

public class EnemyShoot : WeaponController {

	[SerializeField]
	float shootingSpeed;

	[SerializeField]
	float burstDurationMax;

	[SerializeField]
	float burstDurationMin;

	bool shouldFire;

	EnemyPlayer enemyPlayer;

	void Start(){
		enemyPlayer = GetComponent<EnemyPlayer> ();
		enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;
	}

	void EnemyPlayer_OnTargetSelected (Player target)
	{
		print ("OnTargetSelected");
		ActiveWeapon.aimTarget = target.transform;
		ActiveWeapon.AimTargetOffset = Vector3.up * 1.5f;
		StartBurst ();
	}

	void StartBurst(){
		if (!enemyPlayer.EnemyHealth.IsAlive)
			return;

		CheckReload ();
		shouldFire = true;

		GameManager.Instance.Timer.Add(EndBurst, Random.Range(burstDurationMin, burstDurationMax));
			
	}

	void EndBurst(){
		shouldFire = false;
		if (!enemyPlayer.EnemyHealth.IsAlive)
			return;

		CheckReload ();
		GameManager.Instance.Timer.Add (StartBurst, shootingSpeed);
	}

	bool CanSeeTarget(){
		if (!transform.IsInLineOfSight (Vector3.up, ActiveWeapon.aimTarget.position, enemyPlayer.playerScanner.mask, 90)) {
		
			enemyPlayer.ClearTargetAndScan ();
		}
		return true;
	}

	void CheckReload(){
		if (ActiveWeapon.reloader.RoundsRemainingInClip == 0) {
			ActiveWeapon.Reload ();
		}
	}

	void Update(){
		if (!shouldFire || !canFire || !enemyPlayer.EnemyHealth.IsAlive)
			return;

		ActiveWeapon.Fire ();
	}
}
