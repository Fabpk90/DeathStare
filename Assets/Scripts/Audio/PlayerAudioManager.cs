using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject leftFoot;
    public GameObject rightFoot;
    private int playerIndex;
    public WwiseListener wwiseListener;
    
    private void Start()
    {
        AudioManager.instance.AddListeners(leftFoot, 0, 1, 2, 3);
        AudioManager.instance.AddListeners(rightFoot, 0, 1, 2, 3);
        playerIndex = player.GetPlayerIndex();
        switch (playerIndex)
        {
            case (0):
                GetComponent<SetWwiseSwitch>().SetSwitch("Cobble", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Cobble", rightFoot);
                wwiseListener.VolumesOffset[0] = 0;
                wwiseListener.VolumesOffset[1] = -20;
                Debug.Log("Cobble set to p1");
                break;
            case (1):
                GetComponent<SetWwiseSwitch>().SetSwitch("Dirt", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Dirt", rightFoot);
                wwiseListener.VolumesOffset[0] = -20;
                wwiseListener.VolumesOffset[1] = 0;
                Debug.Log("Dirt set to p2");
                break;
            case (2):
                GetComponent<SetWwiseSwitch>().SetSwitch("Grass", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Grass", rightFoot);
                wwiseListener.VolumesOffset[0] = 0;
                wwiseListener.VolumesOffset[1] = -20;
                Debug.Log("Grass set to p3");
                break;
            case (3):
                GetComponent<SetWwiseSwitch>().SetSwitch("Wood", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("Wood", rightFoot);
                wwiseListener.VolumesOffset[0] = -20;
                wwiseListener.VolumesOffset[1] = 0;
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
