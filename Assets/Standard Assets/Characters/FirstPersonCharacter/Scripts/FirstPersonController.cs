using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;

        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        public Vector2 m_Input;
        [SerializeField] private bool m_IsWalking;
        private bool m_Jump;
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        private bool m_Jumping;
        [SerializeField] private float m_JumpSpeed;
        private Vector3 m_MoveDir = Vector3.zero;
        private float m_NextStep;
        private Vector3 m_OriginalCameraPosition;
        private bool m_PreviouslyGrounded;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        private float m_StepCycle;
        [SerializeField] private float m_StepInterval;
        [SerializeField] private float m_StickToGroundForce;

        [SerializeField] private float m_WalkSpeed;
        private float m_YRotation;

        private bool isCrouching;
        public float crouchingSpeed;
        
        private bool isRunning;
        
        private bool isStaring;
        public float stareWalkingSpeed;
        public float stareAerialSpeed;

        public float aerialSpeed;

        private float startingHeightCollider;
        public float crouchingHeightCollider;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();

            startingHeightCollider = m_CharacterController.height;

            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            isCrouching = false;
            isRunning = false;
            isStaring = false;
        }
        
        private void Update()
        {
            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }

            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed = GetSpeedFromState();
            
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }

            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
        }

        private float GetSpeedFromState()
        {
            if (isCrouching)
                return crouchingSpeed;
            if (isRunning)
                return m_RunSpeed;
            if (isStaring)
            {
                if (m_CharacterController.isGrounded)
                    return stareWalkingSpeed;
                
                return stareAerialSpeed;
            }
            if (!m_CharacterController.isGrounded)
                return aerialSpeed;

            return m_WalkSpeed;
        }

        public bool GetIsStarePossible()
        {
            return true;
        }

        private void PlayJumpSound()
        {
            //insert here the audio
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude +
                                (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                               Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            
            //insert here the audio
        }
        

        public void Jump()
        {
            if (m_CharacterController.isGrounded)
                m_Jump = true;
        }

        public void ToggleCrouch()
        {
            isCrouching = !isCrouching;

            m_CharacterController.height = isCrouching ? crouchingHeightCollider : startingHeightCollider;
        }

        public void SetCrouch(bool isCrouching)
        {
            this.isCrouching = isCrouching;

            if (!isCrouching)
            {
                m_CharacterController.height = startingHeightCollider;
            }
        }

        public void SetRunning(bool isRunning)
        {
            this.isRunning = isRunning;

            if (isRunning)
            {
                SetCrouch(false);
            }
        }
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }

            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }

        public void SetStare(bool isStaring)
        {
            
        }
    }
}