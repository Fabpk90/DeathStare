using UnityEngine;
using System.Collections;

namespace Actor.AI.States
{
	public class FollowTarget : AIState
	{
		protected override void StateEnter(AIController controller, AIBehaviour behaviour)
		{
			if(behaviour.target == null)
			{
				behaviour.target = behaviour.others[Random.Range(0, behaviour.others.Count)].transform;
			}
			controller.SetTarget(behaviour.target);
			controller.LookAtTarget(true);
		}

		protected override void StateUpdate(AIController controller, AIBehaviour behaviour)
		{

		}

		protected override void StateExit(AIController controller, AIBehaviour behaviour)
		{

		}
	}
}
