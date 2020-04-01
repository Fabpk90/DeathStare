using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class TestMode : GameMode
{

    public Color[] playersColor;

    [Tooltip("Permet de tester avec une manette")]
    public bool debugPlayers;

    public Transform[] spawnPoints;

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
    }

    protected override void PlayerJoined(PlayerInput obj)
    {
        base.PlayerJoined(obj);
        
        Debug.Log("Player " + obj.playerIndex + " joined !");

        //to see clearly in the inspector which player it is
        obj.gameObject.name = "Player " + obj.playerIndex;
        
        players.Add(obj);

        obj.transform.position = spawnPoints[obj.playerIndex].position;

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
            players[i].GetComponent<MeshRenderer>().material.color = playersColor[i];
        }
        
    }
}
