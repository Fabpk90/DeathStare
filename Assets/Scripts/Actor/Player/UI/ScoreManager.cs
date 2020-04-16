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

    private void Start()
    {
        _controller = GetComponentInParent<PlayerController>();
        GameMode.OnKillEvent += OnKill;
        scoreText.text = "Kills: " + _score;
    }

    private void OnKill(object sender, Tuple<int, int> e)
    {
        if (e.Item1 == _controller.GetPlayerIndex())
        {
            _score++;

            scoreText.text = "Kills: " + _score;

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
