using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//class to update state machine inputs and hold variables (target ...)
namespace Actor.AI
{
	[RequireComponent(typeof(AIController), typeof(Animator))]
	public class AIBehaviour : MonoBehaviour
	{
		private Animator _stateMachine;

		private List<FirstPersonController> _others = new List<FirstPersonController>();
		public IList<FirstPersonController> others => _others.AsReadOnly();

		//State machine variables
		[HideInInspector]
		public Transform target;

		private void Awake()
		{
			_stateMachine = GetComponent<Animator>();
		
			//To do : use game modes to get all player
			foreach (FirstPersonController c in FindObjectsOfType<FirstPersonController>())
			{
				if (c != GetComponent<FirstPersonController>())
				{
					_others.Add(c);
				}
			}
		}
	}
}
