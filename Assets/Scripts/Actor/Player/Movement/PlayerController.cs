using System.Collections;
using System.Collections.Generic;
using Actor.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;

    private Camera _camera;
    private Stare _stare;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _stare = GetComponent<Stare>();
        _camera = GetComponentInChildren<Camera>();
        
        _input.currentActionMap["Stare"].started += OnStartStare;
        _input.currentActionMap["Stare"].canceled += OnStopStare;
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
