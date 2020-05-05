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

        var wwiseSwitch = GetComponent<SetWwiseSwitch>();
        
        switch (player.GetPlayerIndex())
        {
            case (0):
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Cobble", leftFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Cobble", rightFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Cobble", chest);
                wwiseSwitch.SetSwitch("SWITCHES_General_Character", "Stanislas", gameObject);
                wwiseListener.VolumesOffset[0] = 0;
                wwiseListener.VolumesOffset[1] = -20;
                Debug.Log("Cobble set to p1");
                break;
            case (1):
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Dirt", leftFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Dirt", rightFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Dirt", chest);
                wwiseSwitch.SetSwitch("SWITCHES_General_Character", "Marta", gameObject);
                wwiseListener.VolumesOffset[0] = -20;
                wwiseListener.VolumesOffset[1] = 0;
                Debug.Log("Dirt set to p2");
                break;
            case (2):
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Grass", leftFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Grass", rightFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Grass", chest);
                wwiseSwitch.SetSwitch("SWITCHES_General_Character", "Medusa", gameObject);
                wwiseListener.VolumesOffset[0] = 0;
                wwiseListener.VolumesOffset[1] = -20;
                Debug.Log("Grass set to p3");
                break;
            case (3):
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Wood", leftFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Wood", rightFoot);
                wwiseSwitch.SetSwitch("SWITCHES_Foleys_Surfaces", "Wood", chest);
                wwiseSwitch.SetSwitch("SWITCHES_General_Character", "Don", gameObject);
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
