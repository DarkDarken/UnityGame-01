using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyPatrol : MonoBehaviour {

	[SerializeField]
	WayPointController waypointController;

	[SerializeField]
	float waitTimeMax;

	[SerializeField]
	float waitTimeMin;

	PathFinder pathFinder;

	private EnemyPlayer m_EnemyPlayer;
	public EnemyPlayer EnemyPlayer {

		get {
			if (m_EnemyPlayer == null)
				m_EnemyPlayer = GetComponent<EnemyPlayer> ();
			return m_EnemyPlayer;
		}
	}

	void Start(){
		waypointController.SetNextWaypoint ();

	}

	void Awake(){
		pathFinder = GetComponent<PathFinder> ();

		EnemyPlayer.EnemyHealth.OnDeath += EnemyHealth_OnDeath;
		EnemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;

	}

	void EnemyPlayer_OnTargetSelected (Player obj)
	{
		pathFinder.Agent.isStopped = true;
	}

	private void EnemyHealth_OnDeath ()
	{
		pathFinder.Agent.isStopped = true;
	}

	private void WaypointController_OnWaypontChenged (WayPoint waypoint)
	{
		pathFinder.SetTarget (waypoint.transform.position);
	}

	private void PathFinder_OnDestinationReached ()
	{
		GameManager.Instance.Timer.Add (waypointController.SetNextWaypoint, Random.Range (waitTimeMin, waitTimeMax));

	}

	void OnEnable(){
		pathFinder.OnDestinationReached += PathFinder_OnDestinationReached;
		waypointController.OnWaypontChenged += WaypointController_OnWaypontChenged;
	}

	void OnDisable(){
		pathFinder.OnDestinationReached -= PathFinder_OnDestinationReached;
		waypointController.OnWaypontChenged -= WaypointController_OnWaypontChenged;
	}
}
