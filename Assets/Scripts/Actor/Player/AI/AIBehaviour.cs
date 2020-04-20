using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AIController))]
public class AIBehaviour : MonoBehaviour
{
	private AIController _controller;

	private List<FirstPersonController> _others = new List<FirstPersonController>();

	private PlayerController _player;

	private void Awake()
	{
		_controller = GetComponent<AIController>();
		//get enemies
		foreach (FirstPersonController c in FindObjectsOfType<FirstPersonController>())
		{
			if (c != GetComponent<FirstPersonController>())
			{
				_others.Add(c);
			}
		}

		_player = FindObjectOfType<PlayerController>();
	}

	private void Start()
	{
		StartCoroutine(UpdateBehavior());
	}

	private IEnumerator UpdateBehavior()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));

			_controller.SetTarget(_player.transform);
			_controller.LookAtTarget(false);
			_controller.LookToward(transform.position);
		}
	}
}
