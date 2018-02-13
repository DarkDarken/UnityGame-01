using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[SerializeField] float rateOfFire;
	[SerializeField] Projectile projectile;
	[SerializeField] Transform hand;
	[SerializeField] AudioController audioReload;
	[SerializeField] AudioController audioFire;

	public Transform aimTarget;
	public Vector3 AimTargetOffset;
		
	Transform muzzle;

	public WeaponReloader reloader;
	private ParticleSystem muzzelParticleSystem;

	private WeaponRecoil m_WeaponRecoil;
	private WeaponRecoil WeaponRecoil
	{
		get{
			if (m_WeaponRecoil == null)
				m_WeaponRecoil = GetComponent<WeaponRecoil> ();
			return m_WeaponRecoil;
		}

	}

	float nextFireAllowed;
	public bool canFire;

	public void Equip(){
		transform.SetParent (hand);
	}

	void Awake () {
		muzzle = transform.Find ("Muzzle");
		reloader = GetComponent<WeaponReloader> ();
		muzzelParticleSystem = muzzle.GetComponent<ParticleSystem> ();
	}

	public void Reload(){
		if (reloader == null)
			return;
		reloader.Reload ();
		audioReload.Play ();
	}

	void FireEffect(){
		if (muzzelParticleSystem == null)
			return;
		muzzelParticleSystem.Play ();
	}

	public virtual void Fire(){

		canFire = false;

		if (Time.time < nextFireAllowed)
			return;

		if (reloader != null) {
			if (reloader.IsReloading)
				return;
			
			if (reloader.RoundsRemainingInClip == 0)
				return;

			reloader.TakeFromClip (1);
		}

		nextFireAllowed = Time.time + rateOfFire;

		bool isLocalPlayerControlled = aimTarget == null;

		if(!isLocalPlayerControlled)
		    muzzle.LookAt (aimTarget.position + AimTargetOffset);


		Projectile newBullet = Instantiate (projectile, muzzle.position, muzzle.rotation);

		if (isLocalPlayerControlled) {
			Ray ray = Camera.main.ViewportPointToRay (new Vector3 (.5f, .5f, 0));
			RaycastHit hit;
			Vector3 targetPosition = ray.GetPoint (500);

			if (Physics.Raycast (ray, out hit))
				targetPosition = hit.point;

			newBullet.transform.LookAt (targetPosition + AimTargetOffset);
		}

		if (this.WeaponRecoil)
			this.WeaponRecoil.Activate ();

		FireEffect ();

		audioFire.Play ();
		canFire = true;
	}
}
