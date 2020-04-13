using UnityEngine;
using System.Collections;
using BeardedManStudios.Forge.Networking.Generated;

public class BasicCube : BasicCubeBehavior
{
	public float speed = 5f;

	void Update()
	{
		if (networkObject == null) return;

		if (!networkObject.IsOwner)
		{
			transform.position = networkObject.position;
			transform.rotation = networkObject.rotation;
			return;
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
		}

		networkObject.position = transform.position;
		networkObject.rotation = transform.rotation;
	}
}
