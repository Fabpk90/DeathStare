using System;
using System.Collections;
using System.Collections.Generic;
using Actor;
using Actor.Player;
using Actor.Player.Stare;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;

//TODO: make this more smart, abstract the calling away
//keep only the controller part here


[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _input;

    public FirstPersonController controller;
    
    public ActorCameraMovement cameraMovement;
    public StareHandler stareHandler;

    public event EventHandler OnRespawn;
    
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

        //SOUND
        AudioManager.instance.AddListeners(gameObject, 4);
        //SOUND
    }

    public int GetPlayerIndex()
    {
        return _input.playerIndex;
    }

    public void Respawn()
    {
        OnRespawn?.Invoke(this, null);
    }

    private void OnRunStop(InputAction.CallbackContext obj)
    {
        controller.SetRunning(false);
    }

    private void OnRunStart(InputAction.CallbackContext obj)
    {
        if(!stareHandler.isStaring)
            controller.SetRunning(true);
    }

    private void OnCrouch(InputAction.CallbackContext obj)
    {
        controller.ToggleCrouch();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if(controller.canJump())
            controller.Jump();
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        cameraMovement.MoveCamera(obj.ReadValue<Vector2>());
    }

    private void OnMovement(InputAction.CallbackContext obj)
    {
        var v = obj.ReadValue<Vector2>();
        controller.SetInputMovement(v);
    }

    private void OnStopStare(InputAction.CallbackContext obj)
    {
        stareHandler.StopStare();
        controller.SetStare(false);
    }

    private void OnStartStare(InputAction.CallbackContext obj)
    {
        if (controller.canStare())
        {
            stareHandler.StartStare();
            controller.SetStare(true);
            //SOUND
            int playerIndex = GetPlayerIndex();
            switch (playerIndex)
            {
                case (0):
                    AkSoundEngine.PostEvent("STINGERS_DS_Stan_L", gameObject);
                    break;
                case (1):
                    AkSoundEngine.PostEvent("STINGERS_DS_Marta_R", gameObject);
                    break;
                case (2):
                    AkSoundEngine.PostEvent("STINGERS_DS_Medusa_L", gameObject);
                    break;
                case (3):
                    AkSoundEngine.PostEvent("STINGERS_DS_Don_R", gameObject);
                    break;
            }
            //SOUND
        }
    }
}
