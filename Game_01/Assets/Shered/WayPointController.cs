using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointController : MonoBehaviour {

	WayPoint[] waypoints;

	int currentWaypointIndex = -1;
	public event System.Action<WayPoint> OnWaypontChenged;

	void Awake(){
		waypoints = GetComponentsInChildren<WayPoint> ();
	}

	public void SetNextWaypoint(){
		currentWaypointIndex++;

		if (currentWaypointIndex == waypoints.Length)
			currentWaypointIndex = 0;

		if(OnWaypontChenged != null)
			OnWaypontChenged(waypoints[currentWaypointIndex]);
	}

	WayPoint[] GetWaypoints(){
		return GetComponentsInChildren<WayPoint> ();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;

		Vector3 previusPosition = Vector3.zero;

		foreach (var waypoint in GetWaypoints()) {

			Vector3 waypointPosition = waypoint.transform.position;
			Gizmos.DrawWireSphere (waypointPosition, 0.2f);
			if (previusPosition != Vector3.zero)
				Gizmos.DrawLine (previusPosition, waypointPosition);
			previusPosition = waypointPosition;
		}
	}
}
