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

    public delegate void OnKill(int playerIndexKiller, int playerIndexKilled);
    public static event EventHandler<Tuple<int, int>> OnKillEvent;
    public static event EventHandler OnEndOfMatch;
    public static event EventHandler OnStartOfMatch;

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

    public void PlayerKilled(int playerIndexKiller, int playerIndexKilled)
    {
        OnKillEvent?.Invoke(this, new Tuple<int, int>(playerIndexKiller, playerIndexKilled));
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

    public virtual void Win(List<int> winners)
    {
        OnEndOfMatch?.Invoke(this, null);
    }

    public virtual void OnStartMatch()
    {
        OnStartOfMatch?.Invoke(this, null);
    }
}
