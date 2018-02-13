using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour {

	[System.Serializable]
	public struct Layer
	{
		public AnimationCurve curve;
		public Vector3 diraction;
	}

	[SerializeField]
	Layer[] layers;

	[SerializeField]
	float recoilSpeed;

	[SerializeField]
	float recoilColldowne;

	[SerializeField]
	float strength;

	float nextRecoilCooldown;
	float recoilActiveTime;

	Shooter m_Shooter;
	Shooter Shooter {
		get{
			if (m_Shooter == null)
				m_Shooter = GetComponent<Shooter> ();
			return m_Shooter;
		}
	}

	Crosshair m_Crosshair;
	Crosshair Crosshair {
		get{
			if (m_Crosshair == null)
				m_Crosshair = GameManager.Instance.LocalPlayer.playerAim.GetComponentInChildren<Crosshair> ();
			return m_Crosshair;
		}
	}

	public void Activate()
	{
		nextRecoilCooldown = Time.time + recoilColldowne;
	}

	void Update(){

		if (nextRecoilCooldown > Time.time) {
			recoilActiveTime += Time.deltaTime;
			float precentage = getPercentage ();

			Vector3 recoilAmount = Vector3.zero;
			for (int i = 0; i < layers.Length; i++) 
				recoilAmount += layers [i].diraction * layers [i].curve.Evaluate (precentage);


			this.Shooter.AimTargetOffset = Vector3.Lerp (Shooter.AimTargetOffset, Shooter.AimTargetOffset + recoilAmount, strength * Time.deltaTime);
			this.Crosshair.ApplyScale (precentage * Random.Range(strength * 5, strength * 7));

		} else {

			recoilActiveTime -= Time.deltaTime;
			if (recoilActiveTime < 0)
				recoilActiveTime = 0;

			this.Crosshair.ApplyScale (getPercentage ());

			if (recoilActiveTime == 0) {
				this.Shooter.AimTargetOffset = Vector3.zero;
				this.Crosshair.ApplyScale (0);
			}

		}

	}

	float getPercentage(){
		float precentage = recoilActiveTime / recoilSpeed;
		return Mathf.Clamp01 (precentage);
	}
}
