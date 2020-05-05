using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public GameObject chest;
    private int playerIndex;
    public WwiseListener wwiseListener;

    private void Start()
    {
        AudioManager.instance.AddListeners(leftFoot, 0, 1, 2, 3);
        AudioManager.instance.AddListeners(rightFoot, 0, 1, 2, 3);
        AudioManager.instance.AddListeners(gameObject, player.GetPlayerIndex());
        switch (player.GetPlayerIndex())
        {
            case (0):
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Cobble", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Cobble", rightFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Cobble", chest);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_General_Character", "Stanislas", gameObject);
                wwiseListener.VolumesOffset[0] = 0;
                wwiseListener.VolumesOffset[1] = -14;
                Debug.Log("Cobble set to p1");
                break;
            case (1):
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Sand", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Sand", rightFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Sand", chest);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_General_Character", "Stanislas", gameObject);
                wwiseListener.VolumesOffset[0] = -14;
                wwiseListener.VolumesOffset[1] = 0;
                Debug.Log("Dirt set to p2");
                break;
            case (2):
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Grass", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Grass", rightFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Grass", chest);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_General_Character", "Stanislas", gameObject);
                wwiseListener.VolumesOffset[0] = 0;
                wwiseListener.VolumesOffset[1] = -14;
                Debug.Log("Grass set to p3");
                break;
            case (3):
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Wood", leftFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Wood", rightFoot);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_Foleys_Surfaces", "Wood", chest);
                GetComponent<SetWwiseSwitch>().SetSwitch("SWITCHES_General_Character", "Stanislas", gameObject);
                wwiseListener.VolumesOffset[0] = -14;
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

    public void PostChestEvent(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, chest);
    }

    public void PostEvent(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, gameObject);
    }

    public void PostEvent(string eventName, GameObject in_gameObject)
    {
        AkSoundEngine.PostEvent(eventName, in_gameObject);
    }

    public void SetRTPCValue(string parameterName, float value)
    {
        AkSoundEngine.SetRTPCValue(parameterName, value, gameObject);
    }
}
