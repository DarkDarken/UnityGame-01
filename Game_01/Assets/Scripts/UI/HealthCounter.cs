using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour {

	[SerializeField] Text text = null;

	PlayerHealth m_playerHealth;

	void Awake () {

	}

	void HandleOnAmmoChanged(){
		float amountInInventory = m_playerHealth.HitPointsRemaining;
		text.text = string.Format ("{0}", amountInInventory);
	}

	// Update is called once per frame
	void Update () {
		HandleOnAmmoChanged ();
	}
}

	/*[SerializeField] Text text;

	PlayerHealth playerHealth;

	void Start(){
		playerHealth = new PlayerHealth ();
		text = GetComponent<Text> ();
	}

	void Update(){
		float healthTotal = playerHealth.HitPointsTotal;
		print(healthTotal);
		text.text = string.Format ("{0}", healthTotal);
	}
	}
*/
