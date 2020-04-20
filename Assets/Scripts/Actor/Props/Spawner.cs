using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(transform.position, 1);
	}
}
