using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Destructable {

	[SerializeField]
	Ragdoll ragdoll;

	public Text text;
	public Slider slider;

	public override void Die ()
	{
		base.Die ();
		ragdoll.EnableRagdoll (true);
	}


	void Update(){
		UpdateHealth ();
	}

	public void UpdateHealth(){
		text.text = string.Format("{0}", HitPointsRemaining);
		slider.maxValue = hitPoints;
		slider.value = HitPointsRemaining;
	}
}
