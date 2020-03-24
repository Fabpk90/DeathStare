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
        
        _input.currentActionMap["Stare"].started += OnStare;
    }

    private void OnStare(InputAction.CallbackContext obj)
    {
        List<Transform> points = new List<Transform>();

        foreach (PlayerInput player in GameMode.instance.players)
        {
            if(player != _input)
                points.AddRange(player.GetComponent<PlayerHealth>().hittablePoints);
        }
        
        _stare.StareViolently( points.ToArray(),transform.position, _camera);
    }
}
