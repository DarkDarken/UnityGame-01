using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : MonoBehaviour {

	[SerializeField]
	public Scanner playerScanner;

	[SerializeField]
	SwatSoldier settings;

	PathFinder pathFinder;

	Player priorityTarget;
	List<Player> myTargets;

	public event System.Action<Player> OnTargetSelected;

	private EnemyHealth m_EnemyHealth;
	public EnemyHealth EnemyHealth {

		get{
			if (m_EnemyHealth == null)
				m_EnemyHealth = GetComponent<EnemyHealth> ();
			return m_EnemyHealth;
		}

	}

	private EnemyState m_EnemyState;
	public EnemyState EnemyState {

		get{
			if (m_EnemyState == null)
				m_EnemyState = GetComponent<EnemyState> ();
			return m_EnemyState;
		}

	}

	void Start(){
		pathFinder = GetComponent<PathFinder> ();
		pathFinder.Agent.speed = settings.WalkSpeed;

		playerScanner.OnScanReady += Scanner_OnScanReady;
		Scanner_OnScanReady ();

		EnemyHealth.OnDeath += EnemyHealth_OnDeath;
		EnemyState.OnModeChanged += EnemyState_OnModeChanged;
	}

	void EnemyState_OnModeChanged (EnemyState.EMode state)
	{
		pathFinder.Agent.speed = settings.WalkSpeed;

		if (state == EnemyState.EMode.AWARE)
			pathFinder.Agent.speed = settings.RunSpeed;
	}

	void CheckEaseWeapon() {
		if(priorityTarget != null)
			return;

		this.EnemyState.Currentmode = EnemyState.EMode.UNAWARE;
	}

	void CheckContinuePatrol(){
		if(priorityTarget != null)
			return;

		pathFinder.Agent.Resume();

	}

	internal void ClearTargetAndScan(){

		priorityTarget = null;

		GameManager.Instance.Timer.Add (CheckEaseWeapon, Random.Range (3, 6));
		GameManager.Instance.Timer.Add (CheckContinuePatrol, Random.Range (12, 15));

	}

	void EnemyHealth_OnDeath ()
	{
		
	}

	private void Scanner_OnScanReady ()
	{
		if (priorityTarget != null) 
			return;
		
		myTargets = playerScanner.ScanForTargets<Player> ();

		if (myTargets.Count == 1) {
			priorityTarget = myTargets [0];
		} else {
			SelectedClosestTarget ();
		}

		if (priorityTarget != null) {
			if (OnTargetSelected != null) {
				this.EnemyState.Currentmode = EnemyState.EMode.AWARE;
				OnTargetSelected (priorityTarget);
			}
		}
	}

	private void SelectedClosestTarget(){
		float closestTarget = playerScanner.ScanRange;

		foreach(var possibleTarget in myTargets){
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				priorityTarget = possibleTarget;
		}

	}

	void Update(){
		if (priorityTarget == null)
			return;

		transform.LookAt (priorityTarget.transform.position);
	}
}
