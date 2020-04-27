using UnityEngine;

//base class for all AI state (take care of referencing behaviour and controller)
namespace Actor.AI
{
	public abstract class AIState : StateMachineBehaviour
	{
		private AIController _controller;
		private AIBehaviour _behaviour;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			_controller = animator.GetComponent<AIController>();
			_behaviour = animator.GetComponent<AIBehaviour>();
			if (_controller == null)
			{
				Debug.LogError("No AIController on : " + animator.gameObject.name); return;
			}
			if (_behaviour == null)
			{
				Debug.LogError("No AIBehaviour on : " + animator.gameObject.name); return;
			}
			StateEnter(_controller, _behaviour);
		}

		public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!_controller || !_behaviour) return;
			StateUpdate(_controller, _behaviour);
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!_controller || !_behaviour) return;
			StateEnter(_controller, _behaviour);
		}


		protected virtual void StateEnter(AIController controller, AIBehaviour behaviour)
		{

		}

		protected virtual void StateUpdate(AIController controller, AIBehaviour behaviour)
		{

		}

		protected virtual void StateExit(AIController controller, AIBehaviour behaviour)
		{

		}
	}
}
