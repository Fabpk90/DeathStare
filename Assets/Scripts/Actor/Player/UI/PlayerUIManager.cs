﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class PlayerUIManager : MonoBehaviour
{
	[SerializeField,DisplayWithoutEdit]
	private int playerIndex = 0;
	public RectTransform score;

	
	private void Start()
	{
		SetPlayerIndex();
		UpdateScorePos();
	}

	private void SetPlayerIndex()
	{
		if (transform.root.GetComponent<PlayerInput>())
		{
			playerIndex = transform.root.GetComponent<PlayerInput>().playerIndex;
		}
	}

	private void UpdateScorePos()
	{
		
		switch (playerIndex)
		{
			case 0:
				score.anchoredPosition = new Vector2(score.sizeDelta.x / 2, -score.sizeDelta.y / 2);
				break;
			case 1:
				score.anchoredPosition = new Vector2(-score.sizeDelta.x / 2, -score.sizeDelta.y / 2);
				break;
			case 2:
				score.anchoredPosition = new Vector2(score.sizeDelta.x / 2, score.sizeDelta.y / 2);
				break;
			case 3:
				score.anchoredPosition = new Vector2(-score.sizeDelta.x / 2, score.sizeDelta.y / 2);
				break;
		}
	}
}
