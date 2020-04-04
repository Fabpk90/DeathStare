using System.Collections;
using System.Collections.Generic;
using Actor.Player;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;
    private PlayerMovement _movement;

    private Camera _camera;
    public Stare _stare;
    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponent<PlayerMovement>();
        _input = GetComponent<PlayerInput>();
        _camera = GetComponentInChildren<Camera>();
        
        _input.currentActionMap["Movement"].performed += OnMovement;

        _input.currentActionMap["Look"].started += OnLookStarted;
        _input.currentActionMap["Look"].canceled += OnLookStopped;
        
        _input.currentActionMap["Stare"].started += OnStartStare;
        _input.currentActionMap["Stare"].canceled += OnStopStare;
    }

    private void OnLookStopped(InputAction.CallbackContext obj)
    {
        
    }

    private void OnLookStarted(InputAction.CallbackContext obj)
    {
        
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
