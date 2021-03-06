﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	[SerializeField] public float hitPoints;

	public event System.Action OnDeath;
	public event System.Action OnDamageReceived;

	float damageTaken;

	public float HitPointsTotal {
		get {
			return hitPoints;
		}
	}

	public virtual float HitPointsRemaining {
		get {
			return hitPoints - damageTaken;
		}
	}

	public bool IsAlive {
		get {
			return HitPointsRemaining > 0;
		}
	}

	public virtual void Die() {
		if (OnDeath != null)
			OnDeath ();
	}
	public virtual void TakeDamage (float amount) {

		damageTaken += amount;

		if (OnDamageReceived != null)
			OnDamageReceived ();

		if (HitPointsRemaining <= 0) {
			Die ();
			print ("Died");
		}
	}

	public void Reset() {
		damageTaken = 0;
	}
}
