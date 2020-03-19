using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class TestMode : MonoBehaviour
{
    public static TestMode instance;

    public PlayerInput playerPrefab;
    
    public List<PlayerInput> players;

    public bool debugPlayers;
    
    private PlayerInputManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            
            _manager = GetComponent<PlayerInputManager>();

            _manager.playerPrefab = playerPrefab.gameObject;
            
            _manager.onPlayerJoined += OnPlayerJoined;

            if (!debugPlayers)
            {
                print(Gamepad.all.Count);

                //we add the 4 players, or less depending on connected gamepads
                for (int i = 0; i < Gamepad.all.Count && i < 4; i++)
                {
                    _manager.JoinPlayer(i, i, "GamePads", Gamepad.all[i]);
                }
            }
            
            for (int i = 0; i < 4; i++)
            {
                _manager.JoinPlayer(i, i, "GamePads", Gamepad.all[0]);
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnPlayerJoined(PlayerInput obj)
    {
        Debug.Log("Player " + obj.playerIndex + " joined !");
        
        players.Add(obj);

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


    }
}
