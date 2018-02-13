﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shered.Extension;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour {

	[SerializeField]
	float scanSpeed;

	[SerializeField]
	[Range(0, 360)]
	float fieldOfView;

	[SerializeField]
	public LayerMask mask;

	SphereCollider rangeTrigger;

	public float ScanRange {
		get{
			if (rangeTrigger == null)
				rangeTrigger = GetComponent<SphereCollider> ();
			return rangeTrigger.radius;
		}
	}

	public event System.Action OnScanReady;

	void PrepareScan(){

		GameManager.Instance.Timer.Add (() =>
			{
			if(OnScanReady != null)
			OnScanReady();
			} , scanSpeed);
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(fieldOfView / 2)* GetComponent<SphereCollider>().radius);
		Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2)* GetComponent<SphereCollider>().radius);
	}

	Vector3 GetViewAngle(float angle){
		float radian = (angle + transform.eulerAngles.y)* Mathf.Deg2Rad;
		return new Vector3 (Mathf.Sin (radian), 0, Mathf.Cos (radian));
	}

	public List<T> ScanForTargets<T>(){

		List<T> targets = new List<T> ();

		Collider[] result = Physics.OverlapSphere (transform.position, ScanRange);

		for (int i = 0; i < result.Length; i++) {
			
			var player = result [i].transform.GetComponent <T>();

			if (player == null)
				continue;

			if(!transform.IsInLineOfSight(result[i].transform.position, Vector3.up, mask, fieldOfView))
				continue;
			//Transform origin, Vector3 eyeHeight, Vector3 targetPosition, LayerMask mask, float fieldOfView

			targets.Add (player);
		}
		PrepareScan ();
		return targets;
	}
		
}