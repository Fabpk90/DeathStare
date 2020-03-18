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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _manager.JoinPlayer(5, 5, "GamePads", Gamepad.all[0]);
        }
    }
}
