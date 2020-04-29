using System.Collections.Generic;
using Actor.Player.Movement;
using UnityEngine;
using UnityStandardAssets.Utility;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    private static readonly int Jumping = Animator.StringToHash("Jumping");
    private static readonly int Crouching = Animator.StringToHash("Crouching");
    private static readonly int Running = Animator.StringToHash("Running");
    private static readonly int Walking = Animator.StringToHash("Walking");
    private static readonly int Staring = Animator.StringToHash("Staring");

    private bool _isCrouching;

    private bool _isRunning;

    private bool _isStaring;
    private float _mYRotation;

    private float _startingHeightCollider;

    private CharacterController m_CharacterController;
    private CollisionFlags m_CollisionFlags;

    [SerializeField] private FOVKick m_FovKick = new FOVKick();
    
    [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
    private Vector2 m_Input;
    private bool m_IsWalking;
    private bool m_Jump;
    [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
    private bool m_Jumping;
    private Vector3 m_MoveDir = Vector3.zero;
    private float m_NextStep;
    private Vector3 m_OriginalCameraPosition;
    private bool m_PreviouslyGrounded;
    
    private float m_StepCycle;

    public MovementSettings settings;
    private MovementSettingsHelper _settingsData;

    public Animator animator;

    private CooldownTimer jumpCooldown;
    private CooldownTimer stareCooldown;
    private static readonly int Velocity = Animator.StringToHash("Velocity");

    public GameObject feet;
    private static readonly int Backwards = Animator.StringToHash("Backwards");

    // Use this for initialization
    private void Start()
    {
        m_CharacterController = GetComponent<CharacterController>();

        _startingHeightCollider = m_CharacterController.height;
        
        InitFromSO();

        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        m_Jumping = false;
        _isCrouching = false;
        _isRunning = false;
        _isStaring = false;
    }

    /// <summary>
    /// Loads all the params from a scriptable object
    /// </summary>
    private void InitFromSO()
    {
        _settingsData = settings.parameters;
        
        jumpCooldown = new CooldownTimer(_settingsData.jumpCooldown);
        jumpCooldown.Start();
        jumpCooldown.Update(_settingsData.jumpCooldown);
        
        stareCooldown = new CooldownTimer(_settingsData.stareCooldown);
        stareCooldown.Start();
        stareCooldown.Update(_settingsData.stareCooldown);
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
        
        jumpCooldown.Update(Time.deltaTime);
        stareCooldown.Update(Time.deltaTime);
    }


    private void PlayLandingSound()
    {
        AkSoundEngine.PostEvent("FOLEYS_Char_Jump_Landing", feet);
        m_NextStep = m_StepCycle + .5f;
    }

    private void FixedUpdate()
    {
        UpdateMovementState();
        UpdateAnimationSpeed();
        float speed = GetSpeedFromState() * m_Input.magnitude;
        

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
            m_MoveDir.y = -_settingsData.stickToGroundForce;

            if (m_Jump)
            {
                m_MoveDir.y = _settingsData.jumpSpeed;
                PlayJumpSound();
                m_Jump = false;
                m_Jumping = true;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * (_settingsData.gravityMultiplier * Time.fixedDeltaTime);
        }

        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
    }

    private void UpdateAnimationSpeed()
    {
        float magnitude = m_Input.magnitude;

        if (!_isStaring && m_CharacterController.isGrounded && magnitude > 0)
        {
            animator.SetFloat(Backwards, m_Input.y > 0 ? 1 : -1);
            animator.speed = Mathf.Clamp01(magnitude + 0.2f);
        }
        else
            animator.speed = 1.0f;

        //todo reverse the animation if going backwards
    }

    private void UpdateMovementState()
    {
        if (_isStaring)
        {
            _isRunning = false;
            animator.SetBool(Running, false);
        }
        else if(m_Input.magnitude > 0.4f)
        {
            _isRunning = true;
            animator.SetBool(Running, true);
        }
    }

    public void SetInputMovement(Vector2 input)
    {
        m_Input = input;

        m_IsWalking = input != Vector2.zero;
        animator.SetBool(Walking, m_IsWalking);
        animator.SetFloat(Velocity, m_Input.magnitude);
    }

    private float GetSpeedFromState()
    {
        if (_isStaring)
        {
            if (m_CharacterController.isGrounded)
                return m_Input.y > 0 ?_settingsData.stareWalkingSpeedForward : _settingsData.stareWalkingSpeedBackwards;

            return m_Input.y > 0 ? _settingsData.stareAerialSpeedForward : _settingsData.stareAerialSpeedBackwards;
        }
        if (!m_CharacterController.isGrounded)
            return m_Input.y > 0 ? _settingsData.aerialSpeedForward : _settingsData.aerialSpeedBackwards;
        if (_isCrouching)
            return m_Input.y > 0 ? _settingsData.crouchingSpeedForward : _settingsData.crouchingSpeedBackwards;
        if (_isRunning)
            return m_Input.y > 0 ? _settingsData.runSpeedForward : _settingsData.runSpeedBackwards;

        return m_Input.y > 0 ? _settingsData.walkSpeedForward : _settingsData.walkSpeedBackwards;
    }

    private void PlayJumpSound()
    {
        AkSoundEngine.PostEvent("FOLEYS_Char_Jump_TakeOff", feet);
    }


    private void ProgressStepCycle(float speed)
    {
        if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
        {
            m_StepCycle += (m_CharacterController.velocity.magnitude +
                            (speed * (m_IsWalking ? 1f : _settingsData.runstepLenghten))) *
                           Time.fixedDeltaTime;
        }

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + _settingsData.stepInterval;

        PlayFootStepAudio();
    }


    private void PlayFootStepAudio()
    {
        if (!m_CharacterController.isGrounded)
        {
        }

        //insert here the audio
    }


    public void Jump()
    {
        if (m_CharacterController.isGrounded)
        {
            if(_isCrouching)
                SetCrouch(false);
            
            m_Jump = true;
            animator.SetTrigger(Jumping);
            PlayJumpSound();
        }
    }

    public bool canStare()
    {
        if (stareCooldown.IsCompleted)
        {
            stareCooldown.Start();
            return true;
        }

        return false;
    }

    public bool canJump()
    {
        if (jumpCooldown.IsCompleted)
        {
            jumpCooldown.Start();
            return true;
        }

        return false;
    }

    public void ToggleCrouch()
    {
        if(!m_CharacterController.isGrounded) return;
        _isCrouching = !_isCrouching;

        animator.SetBool(Crouching, _isCrouching);

        if (_isCrouching)
        {
            m_CharacterController.height = _settingsData.crouchingHeightCollider;
            var transform1 = animator.transform;
            
            var v = transform1.localPosition;
            v.y /= 2;
            transform1.localPosition = v;
        }
        else
        {
            m_CharacterController.height = _startingHeightCollider;
            var transform1 = animator.transform;
            
            var v = transform1.localPosition;
            v.y *= 2;
            transform1.localPosition = v;
        }
    }

    public void SetCrouch(bool isCrouching)
    {
        _isCrouching = isCrouching;

        animator.SetBool(Crouching, isCrouching);

        if (_isCrouching)
        {
            m_CharacterController.height = _settingsData.crouchingHeightCollider;
            var transform1 = animator.transform;
            
            var v = transform1.localPosition;
            v.y /= 2;
            transform1.localPosition = v;
        }
        else
        {
            m_CharacterController.height = _startingHeightCollider;
            var transform1 = animator.transform;
            
            var v = transform1.localPosition;
            v.y *= 2;
            transform1.localPosition = v;
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
        _isStaring = isStaring;
        animator.SetBool(Staring, isStaring);
    }
}