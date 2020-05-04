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

    public FirstPersonController controller;
    
    public ActorCameraMovement cameraMovement;
    public StareHandler stareHandler;

    public event EventHandler OnRespawn;

    public PlayerAudioManager playerAudioManager;

    private LayerMask layer;
    
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

        //SOUND
        AudioManager.instance.AddListeners(gameObject, 0, 1, 2, 3);
        //SOUND

        layer = LayerMask.GetMask("J" + (GetPlayerIndex() + 1) + "Body");
        gameObject.layer = layer;
        stareHandler.layerRay = layer;
    }

    public int GetPlayerIndex()
    {
        return _input.playerIndex;
    }

    public void Respawn()
    {
        OnRespawn?.Invoke(this, null);
    }

    private void OnCrouch(InputAction.CallbackContext obj)
    {
        if(!stareHandler.isStaring)
            controller.ToggleCrouch();
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if(controller.canJump() && !stareHandler.isStaring)
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
        //Sound
        playerAudioManager.PostEvent("Stop_EFFECTS_Char_Staring");
        playerAudioManager.PostEvent("EFFECTS_Char_ExitStaring");
        switch (GetPlayerIndex())
        {
            case (0):
                AkSoundEngine.SetState("STATE_Music_DuelState_Stan", "False");
                break;
            case (1):
                AkSoundEngine.SetState("STATE_Music_DuelState_Marta", "False");
                break;
            case (2):
                AkSoundEngine.SetState("STATE_Music_DuelState_Medusa", "False");
                break;
            case (3):
                AkSoundEngine.SetState("STATE_Music_DuelState_Don", "False");
                break;
        }
        //Sound
    }

    private void OnStartStare(InputAction.CallbackContext obj)
    {
        if (controller.canStare())
        {
            stareHandler.StartStare();
            controller.SetStare(true);
            //SOUND
            playerAudioManager.PostEvent("EFFECTS_Char_Staring");
            int playerIndex = GetPlayerIndex();
            switch (playerIndex)
            {
                case (0):
                    AudioManager.instance.PostEvent("STINGERS_DS_Stan_L");
                    break;
                case (1):
                    AudioManager.instance.PostEvent("STINGERS_DS_Marta_R");
                    break;
                case (2):
                    AudioManager.instance.PostEvent("STINGERS_DS_Medusa_L");
                    break;
                case (3):
                    AudioManager.instance.PostEvent("STINGERS_DS_Don_R");
                    break;
            }
            //SOUND
        }
    }
}
