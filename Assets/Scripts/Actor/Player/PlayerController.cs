using System;
using System.Collections;
using System.Collections.Generic;
using Actor;
using Actor.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;

//TODO: make this more smart, abstract the calling away
//keep only the controller part here


[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;

    public FirstPersonController _controller;
    
    public ActorCameraMovement cameraMovement;
    public Stare _stare;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();

        _input.currentActionMap["Movement"].performed += OnMovement;

        _input.currentActionMap["Look"].performed += OnLook;

        _input.currentActionMap["Stare"].started += OnStartStare;
        _input.currentActionMap["Stare"].canceled += OnStopStare;
        
        
        _input.currentActionMap["Jump"].started += OnJump;
        
        _input.currentActionMap["Crouch"].started += OnCrouch;

        _input.currentActionMap["Run"].started += OnRunStart;
        _input.currentActionMap["Run"].canceled += OnRunStop;
    }

    private void OnRunStop(InputAction.CallbackContext obj)
    {
        _controller.SetRunning(false);
    }

    private void OnRunStart(InputAction.CallbackContext obj)
    {
        _controller.SetRunning(true);
    }

    private void OnCrouch(InputAction.CallbackContext obj)
    {
        _controller.ToggleCrouch();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        _controller.Jump();
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        cameraMovement.MoveCamera(obj.ReadValue<Vector2>());
    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        var v = obj.ReadValue<Vector2>();
        _controller.SetInputMovement(v);
    }

    private void OnStopStare(InputAction.CallbackContext obj)
    {
        _stare.StopStare();
        _controller.SetStare(false);
    }

    private void OnStartStare(InputAction.CallbackContext obj)
    {
        print(_stare.StartStare());
        _controller.SetStare(true);
    }
}
