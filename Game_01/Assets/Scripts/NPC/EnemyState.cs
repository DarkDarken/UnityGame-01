using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour {

	public enum EMode {
		AWARE,
		UNAWARE
}
	private EMode m_Currentmode;
	public EMode Currentmode {
		get{
			return m_Currentmode;
		}
		set{
			if (m_Currentmode == value)
				return;

			m_Currentmode = value;

			if (OnModeChanged != null)
				OnModeChanged (value);
		}
	}

	public event System.Action<EMode> OnModeChanged;

	void Awake(){
		Currentmode = EMode.UNAWARE;
	}
}
