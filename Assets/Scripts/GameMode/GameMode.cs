using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class GameMode : MonoBehaviour
{
    public static GameMode instance;

    public PlayerInput playerPrefab;
    
    public List<PlayerInput> players;
    protected PlayerInputManager _manager;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            _manager = GetComponent<PlayerInputManager>();

            _manager.playerPrefab = playerPrefab.gameObject;

            _manager.onPlayerJoined += OnPlayerJoined;
            
            Init();
        }
    }

    private void OnPlayerJoined(PlayerInput obj)
    {
        PlayerJoined(obj);
    }

    protected virtual void PlayerJoined(PlayerInput obj)
    {
        
    }

    public virtual void Init()
    {
        
    }
}
