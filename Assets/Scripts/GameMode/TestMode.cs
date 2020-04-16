using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Player;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets.Characters.FirstPerson;

public class FinalScoreHandler : IComparable<FinalScoreHandler>
{
    public uint score;
    public PlayerInput player;


    public FinalScoreHandler(uint score, PlayerInput player)
    {
        this.score = score;
        this.player = player;
    }
    
    public int CompareTo(FinalScoreHandler other)
    {
        if (score >= other.score)
            return 1;

        return -1;
    }
}


[RequireComponent(typeof(PlayerInputManager))]
public class TestMode : GameMode
{

    public Color[] playersColor;

    [Tooltip("Permet de tester avec une seule manette")]
    public bool debugPlayers;

    public Transform[] spawnPoints;

    private CooldownTimer _roundTimer;
    public TextMeshProUGUI textTimerRound;
    public float secondsInRound;

    public float invulnerableTime;

    public uint killsToWin;

    public GameObject scoreUI;
    public List<TextMeshProUGUI> scoreTexts;

    public override void Init()
    {
        base.Init();

        instance = this;
        
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
        
        _roundTimer = new CooldownTimer(secondsInRound);
        _roundTimer.TimerCompleteEvent += OnEndTimeOfRound;
        
        _roundTimer.Start();
        
        OnKillEvent += OnKill;
    }

    private void OnKill(object sender, Tuple<int, int> e)
    {
        var player = players[e.Item2];
        var ct = player.GetComponent<CharacterController>();
        
        ct.enabled = false;
        player.transform.position = spawnPoints[player.playerIndex].position;
        player.transform.rotation = spawnPoints[player.playerIndex].rotation;
        ct.enabled = true;
        
        player.GetComponent<PlayerHealth>().ActivateInvicibility(invulnerableTime);
    }

    private void Update()
    {
        _roundTimer.Update(Time.deltaTime);
        //TODO: optimize this
        textTimerRound.text = ((int)_roundTimer.TimeRemaining / 60) + ":" + (int)_roundTimer.TimeRemaining % 60;
    }

    private void OnEndTimeOfRound()
    {
        print("End of round !");
    }

    public override void Win(List<int> winners)
    {
        base.Win(winners);
        
        _roundTimer.Pause();

        List<FinalScoreHandler> playerScores = new List<FinalScoreHandler>(4);

        foreach (PlayerInput player in players)
        {
            playerScores.Add(new FinalScoreHandler(player.GetComponentInChildren<ScoreManager>().GetScore(), player));
        }
        
        playerScores.Sort();
        
        scoreUI.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            scoreTexts[i].text = i + ". Player " + playerScores[i].player.playerIndex + " wins with a score of " +
                                 playerScores[i].score;
        }
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
        
        //Sound
        AudioManager.instance.Listeners.SetValue(obj.camera.gameObject, obj.playerIndex);
        //Sound

        if (players.Count == 4)
        {
            OnStartMatch();
        }
            
    }

    public override void OnStartMatch()
    {
        base.OnStartMatch();
        
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
