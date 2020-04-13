using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//need camera ref + inputDirection + animator with Ik active
[RequireComponent(typeof(Animator))]
public class PlayerHeadBodyController : MonoBehaviour
{
	//private variables
	private Animator _animator;
	private bool _isMovingBackward;

	//accessors
	/// <summary>
	/// does the character direction is going backward relative to camera direction
	/// </summary>
	public bool isMovingBackward => _isMovingBackward; 

	//inspector variables
	public Transform characterCamera;
	public float bodyUpdateRotationSpeed = 500;
	//public Vector3 worldDirectionInput = Vector3.forward; //the direction of the character (world)
	public CharacterController controller;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
	}

	private void Update()
	{
		if (!characterCamera || !controller) return;

		Vector3 worldDirectionInput = controller.velocity.normalized;
		//Projection of direction on the X,Z plane
		Vector3 projectHeadDirection = Vector3.ProjectOnPlane(characterCamera.forward, Vector3.up).normalized;
		Vector3 projectBodyDirection = Vector3.ProjectOnPlane(worldDirectionInput, Vector3.up).normalized;
		
		//if no direction, make the body face the camera direction
		if (worldDirectionInput == Vector3.zero)
		{
			projectBodyDirection = projectHeadDirection;
		}

		//compute the rotation of the body
		Quaternion orientation = Quaternion.LookRotation(projectBodyDirection, Vector3.up);
		//get the angle between body direction and camera direction
		float signedAngle = Vector3.SignedAngle(projectBodyDirection, projectHeadDirection,Vector3.up);

		//inverse the body rotation in the right direction 
		_isMovingBackward = false;
		if(signedAngle > 90)
		{
			orientation *= Quaternion.Euler(0, -180, 0);
			_isMovingBackward = true;
		}
		if (signedAngle < -90)
		{
			orientation *= Quaternion.Euler(0, 180, 0);
			_isMovingBackward = true;
		}
		
		//apply the body rotation over time
		transform.rotation = Quaternion.RotateTowards(transform.rotation, orientation, bodyUpdateRotationSpeed * Time.deltaTime);
	}

	private void OnAnimatorIK(int layerIndex)
	{
		//apply the camera rotation with the weird world->local rotation conversion
		Transform head = _animator.GetBoneTransform(HumanBodyBones.Neck);
		Quaternion prevRot = head.rotation;
		head.rotation = characterCamera.rotation;
		Quaternion local = head.localRotation;
		head.rotation = prevRot;
		_animator.SetBoneLocalRotation(HumanBodyBones.Neck, local );//little rotation because of the orientation of the bones
	}
}
