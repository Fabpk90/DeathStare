using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

//handles Player UI
//bridge between player status and animator

[RequireComponent(typeof(Animator))]
public class PlayerUIManager : MonoBehaviour
{
	[SerializeField,DisplayWithoutEdit]
	private int _playerIndex = 0;
	[Space]
	public StareHandler stare;
	[Space]
	public CanvasScaler canvasScaler;
	public RectTransform score;
	public RectTransform deathStareUpBar;
	public RectTransform deathStareDownBar;
	private Animator animator;


	private bool isStaring;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}


	private void Start()
	{	
		SetPlayerIndex();
		UpdateScorePos();		
	}

	private void OnEnable()
	{
		stare.OnStareStart += OnStareStart;
		stare.OnStareStop += OnStareStop;
	}

	private void OnDisable()
	{
		stare.OnStareStart -= OnStareStart;
		stare.OnStareStop -= OnStareStop;
	}

	//Find the player index
	private void SetPlayerIndex()
	{
		if (transform.root.GetComponent<PlayerInput>())
		{
			_playerIndex = transform.root.GetComponent<PlayerInput>().playerIndex;
		}
	}

	//update the position of the scoreUI
	private void UpdateScorePos()
	{
		switch (_playerIndex)
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

	//Get the height of the stare (in viewport coord)
	public Vector2 GetViewHeight()
	{
		Vector2 v = Vector2.zero;
		v.x = deathStareDownBar.rect.height / canvasScaler.referenceResolution.y;
		v.y = 1 - deathStareUpBar.rect.height / canvasScaler.referenceResolution.y;
		return v;
	}
	

	private void OnStareStart(object sender, System.EventArgs e)
	{
		animator.SetBool("isStaring", true);
	}

	private void OnStareStop(object sender, System.EventArgs e)
	{
		animator.SetBool("isStaring", false);
	}
}
