using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent(typeof(PlayerInputManager))]
public class TestMode : GameMode
{

    public Color[] playersColor;

    [Tooltip("Permet de tester avec une manette")]
    public bool debugPlayers;

    public Transform[] spawnPoints;

    private CooldownTimer _timer;
    public TextMeshPro textTimerRound;
    public float secondsInRound;

    public override void Init()
    {
        base.Init();
        
        if (!debugPlayers)
        {
            //we add the 4 players, or less depending on connected gamepads
            for (int i = 0; i < Gamepad.all.Count && i < 4; i++)
            {
                _manager.JoinPlayer(i, i, "GamePads", Gamepad.all[i]);
            }
        }
        
        if(Gamepad.all.Count == 0)
            print("Attention pas de manette connectée !");
            
        for (int i = 0; i < 4; i++)
        {
            _manager.JoinPlayer(i, i, "GamePads", Gamepad.all[0]);
        }
        
        _timer = new CooldownTimer(secondsInRound);
        _timer.TimerCompleteEvent += OnEndTimeOfRound;
    }

    private void Update()
    {
        _timer.Update(Time.deltaTime);
        //TODO: optimize this
        textTimerRound.text = (_timer.TimeRemaining / 60) + ":" + _timer.TimeRemaining % 60;
    }

    private void OnEndTimeOfRound()
    {
        throw new System.NotImplementedException();
    }

    protected override void PlayerJoined(PlayerInput obj)
    {
        base.PlayerJoined(obj);
        
        Debug.Log("Player " + obj.playerIndex + " joined !");

        //to see clearly in the inspector which player it is
        obj.gameObject.name = "Player " + obj.playerIndex;
        
        players.Add(obj);

        var ct = obj.GetComponent<CharacterController>();
        
        ct.enabled = false;
        obj.transform.position = spawnPoints[obj.playerIndex].position;
        obj.transform.rotation = spawnPoints[obj.playerIndex].rotation;
        ct.enabled = true;

        if (players.Count == 4)
            InitPlayersCamera();
    }

    //Creating this because the unity's system doesn't work
    private void InitPlayersCamera()
    {
        var playerCam = players[0].GetComponentInChildren<Camera>();

        Rect camRect = playerCam.rect;
        camRect.y = .5f;
        camRect.width = 0.5f;
        camRect.height = 0.5f;

        playerCam.rect = camRect;

        camRect.x = 0.5f;

        players[1].GetComponentInChildren<Camera>().rect = camRect;

        camRect.y = 0.0f;
        
        players[2].GetComponentInChildren<Camera>().rect = camRect;

        camRect.x = camRect.y = 0.0f;
        
        players[3].GetComponentInChildren<Camera>().rect = camRect;

        for (int i = 0; i < players.Count; i++)
        {
            //players[i].GetComponentInChildren<MeshRenderer>().material.color = playersColor[i];
        }
        
    }
}
