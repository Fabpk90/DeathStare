using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject leftFoot;
    public GameObject rightFoot;
    private int playerIndex;
    public GameObject camera;
    
    private void Start()
    {
        AudioManager.instance.AddListeners(leftFoot, 4);
        AudioManager.instance.AddListeners(rightFoot, 4);
        playerIndex = player.GetPlayerIndex();
        switch (playerIndex)
        {
            case (0):
                GetComponent<SetWwiseSwitch>().SetSwitch("Cobble", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Cobble", rightFoot);
                Debug.Log("Cobble set to p1");
                break;
            case (1):
                GetComponent<SetWwiseSwitch>().SetSwitch("Dirt", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Dirt", rightFoot);
                Debug.Log("Dirt set to p2");
                break;
            case (2):
                GetComponent<SetWwiseSwitch>().SetSwitch("Grass", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Grass", rightFoot);
                Debug.Log("Grass set to p3");
                break;
            case (3):
                GetComponent<SetWwiseSwitch>().SetSwitch("Wood", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Wood", rightFoot);
                Debug.Log("Wood set to p4");
                break;
        }
    }

    public void PostLeftFootEvent(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, leftFoot);
    }

    public void PostRightFootEvent(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, rightFoot);
    }
}
