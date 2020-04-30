using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class CameraSplitScreen : MonoBehaviour
{
	public Camera mainCam;
	public PlayerInput playerInput;

	private void Awake()
	{
		SetCam(playerInput.playerIndex);
	}

	private void SetCam(int playerIndex)
	{
		switch (playerIndex)
		{
			case 0:
				mainCam.rect = new Rect(0.5f, 0, 0.5f, 0.5f);
				break;
			case 1:
				mainCam.rect = new Rect(0, 0, 0.5f, 0.5f);
				break;
			case 2:
				mainCam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
				break;
			case 3:
				mainCam.rect = new Rect(0, 0.5f, 0.5f, 0.5f);
				break;
		}
	}
}
