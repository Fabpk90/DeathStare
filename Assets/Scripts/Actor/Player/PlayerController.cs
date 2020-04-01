using System.Collections;
using System.Collections.Generic;
using Actor;
using Actor.Player;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: make this more smart, abstract the calling away
//keep only the controller part here


[RequireComponent(typeof(PlayerMovement), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;
    
    
    public ActorCameraMovement cameraMovement;
    public Stare _stare;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _input = GetComponent<PlayerInput>();

        _input.currentActionMap["Movement"].performed += OnMovement;

        _input.currentActionMap["Look"].performed += OnLook;

        _input.currentActionMap["Stare"].started += OnStartStare;
        _input.currentActionMap["Stare"].canceled += OnStopStare;
        
        
        _input.currentActionMap["Jump"].started += OnJump;
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _movement.isJumping = true;
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        cameraMovement.MoveCamera(obj.ReadValue<Vector2>());
    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        var v = obj.ReadValue<Vector2>();
        _movement.SetMovement(new Vector3(v.x, 0, v.y));
    }

    private void OnStopStare(InputAction.CallbackContext obj)
    {
        _stare.StopStare();
    }

    private void OnStartStare(InputAction.CallbackContext obj)
    {
        print(_stare.StartStare());
    }
}
