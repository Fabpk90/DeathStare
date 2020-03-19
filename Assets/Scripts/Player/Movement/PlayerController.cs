using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;

    private Stare _stare;
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<PlayerInput>();
        _stare = GetComponent<Stare>();
        
        _input.currentActionMap["Stare"].started += OnStare;
        
    }

    private void OnStare(InputAction.CallbackContext obj)
    {
        _stare.StareViolently(transform.position, transform.forward);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
