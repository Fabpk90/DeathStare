using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using Actor;

public class AIController : MonoBehaviour
{
	private FirstPersonController _controller;
	public ActorCameraMovement cameraMovement;

	private List<FirstPersonController> _others = new List<FirstPersonController>();

	NavMeshPath currentPath;
	private int cornerIndex = 0;

	private bool coroutineIsRuning = false;


	private void Awake()
	{
		_controller = GetComponent<FirstPersonController>();
		//get enemies
		foreach(FirstPersonController c in FindObjectsOfType<FirstPersonController>())
		{
			if(c!= _controller)
			{
				_others.Add(c);
			}
		}

	}

	private void Update()
	{
		if (!coroutineIsRuning)
		{
			StartCoroutine(UpdateBehaviour());
		}

		//follow path if it exist
		if (currentPath == null && currentPath.status != NavMeshPathStatus.PathComplete) return;

		if (cornerIndex >= currentPath.corners.Length) return;

		//cameraMovement.MoveCamera()
		_controller.SetInputMovement(new Vector2(0, 1));

		float sqrDelta = (transform.position - currentPath.corners[cornerIndex]).sqrMagnitude;
		if(sqrDelta < 1)
		{
			cornerIndex++;
		}
	
	
	}

	private IEnumerator UpdateBehaviour()
	{
		coroutineIsRuning = true;
		//no synch between IA, determine reaction time
		yield return new WaitForSeconds(Random.Range(0.5f,1));


		NavMesh.CalculatePath(transform.position, _others[Random.Range(0, _others.Count)].transform.position,NavMesh.AllAreas,currentPath);


		coroutineIsRuning = false;
	}
}
