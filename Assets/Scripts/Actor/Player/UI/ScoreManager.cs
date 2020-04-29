using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private PlayerController _controller;
    public PlayerAudioManager playerAudioManager;
    
    private uint _score;

    private void OnEnable()
    {
        GameMode.OnKillEvent += OnKill;
    }

    private void OnDisable()
    {
        GameMode.OnKillEvent -= OnKill;
    }

    private void Start()
    {
        _controller = GetComponentInParent<PlayerController>();
        
        scoreText.text = "Kills: " + _score;

        //Sound
        AudioManager.instance.AddListeners(gameObject, 4);
        //Sound
    }

    private void OnKill(object sender, Tuple<int, int> e)
    {
        print(e.Item1);
        if (e.Item1 == _controller.GetPlayerIndex())
        {
            _score++;

            scoreText.text = "Kills: " + _score;

            //Sound
            switch (e.Item1){
                case (0):
                    //AkSoundEngine.PostEvent("STINGERS_Kill_Stan_L", gameObject);
                    AkSoundEngine.PostTrigger("TRIGGER_Kill_Stan_L", AudioManager.instance.gameObject);
                    playerAudioManager.PostEvent("VO_Char_Punchline_Kill");
                    break;
                case (1):
                    //AkSoundEngine.PostEvent("STINGERS_Kill_Marta_R", gameObject);
                    AkSoundEngine.PostTrigger("TRIGGER_Kill_Marta_R", AudioManager.instance.gameObject);
                    playerAudioManager.PostEvent("VO_Char_Punchline_Kill");
                    break;
                case (2):
                    //AkSoundEngine.PostEvent("STINGERS_Kill_Medusa_L", gameObject);
                    AkSoundEngine.PostTrigger("TRIGGER_Kill_Medusa_L", AudioManager.instance.gameObject);
                    playerAudioManager.PostEvent("VO_Char_Punchline_Kill");
                    break;
                case (3):
                    //AkSoundEngine.PostEvent("STINGERS_Kill_Don_R", gameObject);
                    AkSoundEngine.PostTrigger("TRIGGER_Kill_Don_R", AudioManager.instance.gameObject);
                    playerAudioManager.PostEvent("VO_Char_Punchline_Kill");
                    break;
            }
            //Sound

            //TODO: fix this ugly hax to be more abstract
            if (((TestMode) TestMode.instance).killsToWin <= _score)
                TestMode.instance.Win(new List<int>() {_controller.GetPlayerIndex()});
        }
    }

    public uint GetScore()
    {
        return _score;
    }
}
