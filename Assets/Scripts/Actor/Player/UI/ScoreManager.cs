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
        
        scoreText.text = ""+ _score;

        //Sound
        AudioManager.instance.AddListeners(gameObject, 4);
        //Sound
    }

    private void OnKill(object sender, Tuple<int, int> e)
    {
        if (e.Item1 == _controller.GetPlayerIndex())
        {
            _score++;

            scoreText.text = "" + _score;

            //Sound
            switch (e.Item1){
                case (0):
                    AkSoundEngine.PostEvent("STINGERS_Kill_Stan_L", gameObject);
                    break;
                case (1):
                    AkSoundEngine.PostEvent("STINGERS_Kill_Marta_R", gameObject);
                    break;
                case (2):
                    AkSoundEngine.PostEvent("STINGERS_Kill_Medusa_L", gameObject);
                    break;
                case (3):
                    AkSoundEngine.PostEvent("STINGERS_Kill_Don_R", gameObject);
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
