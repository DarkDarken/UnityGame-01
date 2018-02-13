using UnityEngine;

namespace Shered.Extension
{
	public static class TransformExtension
	{

		/// <summary>
		/// Check if the target id within line of sight
		/// </summary>
		/// <returns><c>true</c> if is in line of sight the specified origin eyeHeight targetPosition mask fieldOfView; otherwise, <c>false</c>.</returns>
		/// <param name="origin">Transform origin</param>
		/// <param name="eyeHeight">Target origin offset</param>
		/// <param name="targetPosition">Target position.</param>
		/// <param name="mask">Check against layers</param>
		/// <param name="fieldOfView">Field of view.</param>

		public static bool IsInLineOfSight(this Transform origin, Vector3 eyeHeight, Vector3 targetPosition, LayerMask mask, float fieldOfView){
			
			Vector3 direction = targetPosition - origin.position;

			float angle = Vector3.Angle (origin.forward, direction.normalized);

			if ( angle < fieldOfView / 2)
			{
				float distanceToTarget = Vector3.Distance (origin.position, targetPosition);

				RaycastHit hit;
				if (Physics.Raycast (origin.position + eyeHeight, direction.normalized, out hit, distanceToTarget, mask)) {
					return false;
				}
				return true;
			}
			return false;
		}
		
	}
}
