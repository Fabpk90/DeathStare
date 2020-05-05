using UnityEngine;
using UnityEngine.AI;

//class to control the movement of the AI
namespace Actor.AI
{
	[RequireComponent(typeof(FirstPersonController))]
	public class AIController : MonoBehaviour
	{
		public ActorCameraMovement cameraMovement;
		public float targetUpdateRate = 0.2f;
		public float rotationInputSmooth = 10;
		public float moveInputSmooth = 10;
		public float uncertainty = 0.1f;
		public float rotationAngleMultiplier = 0.1f;


		private FirstPersonController _controller;
		private NavMeshAgent _agent;
		private int _cornerIndex = 0;
		private Transform _target;
		private Vector3 _nextCorner;
		private Vector3 _direction;
		private Vector2 _moveInput;
		private Vector2 _rotInput;
		private float _lastTargetUpdate;
		private float _speed;
		private Vector3 _lookDirection;
		private bool _lookAtTarget;



		#region Public Method

		public void SetDestination(Vector3 v)
		{
			_target = null;
			UpdateDestination(v);
		}

		public void SetTarget(Transform t)
		{
			_target = t;
			_lastTargetUpdate = -100;
		}

		public void SetSpeed(float s)
		{
			_speed = Mathf.Clamp01(s);
		}

		public void LookToward(Vector3 direction)
		{
			_lookAtTarget = false;
			_lookDirection = direction;
		}

		public void LookAtTarget(bool active)
		{
			_lookAtTarget = active;
		}

		#endregion

		#region Unity methods
		private void Awake()
		{
			_controller = GetComponent<FirstPersonController>();
			_agent = GetComponent<NavMeshAgent>();
			_agent.updatePosition = false;
			_agent.updateRotation = false;
			_agent.updateUpAxis = false;
			_speed = 1;
		}

		private void Update()
		{
			UpdateAgentPosition();
			UpdateTargetdestination();
			UpdateNextCorner();
			UpdateDirection();
			ShouldJump();

			Vector2 rotTarget = DirectionToRotInput(_lookDirection);
			if (_lookAtTarget)
			{
				rotTarget = DirectionToRotInput(_direction);
			}
			_rotInput = Vector2.Lerp(_rotInput, rotTarget, rotationInputSmooth * Time.deltaTime) * _speed;

			Vector2 moveTarget = DirectionToMoveInput(_direction);
			moveTarget = Vector2.Lerp(moveTarget, Random.insideUnitCircle, uncertainty);
			_moveInput = Vector2.Lerp(_moveInput, moveTarget, moveInputSmooth * Time.deltaTime) * _speed;


			SetInputs(_moveInput, _rotInput, true);
		}
		#endregion

		#region internal
		private void UpdateTargetdestination()
		{
			if (_target && Time.time > _lastTargetUpdate + targetUpdateRate)
			{
				UpdateDestination(_target.position);
				_lastTargetUpdate = Time.time;
			}
		}

		private Vector2 DirectionToRotInput(Vector3 dir)
		{
			float angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(dir, Vector3.up), Vector3.ProjectOnPlane(transform.forward, Vector3.up), Vector3.up);
			angle = Mathf.Clamp(angle * rotationAngleMultiplier, -1, 1);
			return new Vector2(-angle, 0);
		}

		private Vector2 DirectionToMoveInput(Vector3 dir)
		{
			Vector3 local = transform.InverseTransformDirection(new Vector3(dir.x, transform.position.y, dir.z));
			return new Vector2(local.x, local.z).normalized;
		}

		private void UpdateDirection()
		{
			Vector3 samplePos = TrySamplePosition(transform.position);
			_direction = _nextCorner - samplePos;
		}

		private void UpdateNextCorner()
		{
			if (_agent.path == null || _agent.path.status == NavMeshPathStatus.PathInvalid)
				return;
			if (_cornerIndex >= _agent.path.corners.Length)
			{
				return; //reach destination
			}
			_nextCorner = _agent.path.corners[_cornerIndex];
			float sqrDelta = (transform.position - new Vector3(_agent.path.corners[_cornerIndex].x, transform.position.y, _agent.path.corners[_cornerIndex].z)).sqrMagnitude;
			if (sqrDelta < 1)
				_cornerIndex++;
			DisplayDebug();
		}

		private void DisplayDebug()
		{
			for (int i = 0; i < _agent.path.corners.Length - 1; i++)
				Debug.DrawLine(_agent.path.corners[i], _agent.path.corners[i + 1]);
		}

		private void ShouldJump()
		{
			if (_agent.isOnOffMeshLink)
				_controller.Jump();
		}

		private void UpdateAgentPosition()
		{
			_agent.nextPosition = transform.position;
			//prevent desync, TO DO use sqrt
			if (Vector3.Distance(_agent.nextPosition, transform.position) > 2f)
			{
				//Debug.Log("teleport");
				_agent.Warp(transform.position);
			}
		}

		//get the closest position on the navmesh (if fail return the source position)
		private Vector3 TrySamplePosition(Vector3 source, float dist = Mathf.Infinity, int areas = NavMesh.AllAreas)
		{
			NavMeshHit hit;
			if (NavMesh.SamplePosition(source, out hit, dist, areas))
			{
				return hit.position;
			}
			else
			{
				return source;
			}
		}

		private void SetInputs(Vector2 move, Vector2 rot, bool run)
		{
			_controller.SetInputMovement(move);
			cameraMovement.MoveCamera(rot);
		}

		private void UpdateDestination(Vector3 v)
		{
			if (!_agent.isOnNavMesh) return;
			_agent.SetDestination(v);
			_cornerIndex = 1;
		}
		#endregion
	}
}
