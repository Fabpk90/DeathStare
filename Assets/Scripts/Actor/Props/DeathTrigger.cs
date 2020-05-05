using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Actor.Player;
[RequireComponent(typeof(BoxCollider))]
public class DeathTrigger : MonoBehaviour
{
	private void OnTriggerStay(Collider other)
	{
		PlayerHealth hp = other.transform.root.GetComponent<PlayerHealth>();
		if (!hp) return;
		Debug.Log(hp.name + " Falls");
		hp.Suicide();
	}
}
