using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWwiseSwitch : MonoBehaviour
{
    public string switchGroupName;
    public string startingSwitchName;
    public GameObject switchGameObject;

    private void Awake()
    {
        if (switchGroupName != null && startingSwitchName != null)
        {
            AkSoundEngine.SetSwitch(switchGroupName, startingSwitchName, switchGameObject);
            Debug.Log("WWISE: Switch set to " + startingSwitchName + " on gameObject " + switchGameObject.name);
        }
    }

    public void SetSwitch(string switchToSet)
    {
        AkSoundEngine.SetSwitch(switchGroupName, switchToSet, switchGameObject);
        Debug.Log("WWISE: Switch set to " + switchToSet + " on gameObject " + switchGameObject.name);
    }

    public void SetSwitch(string switchToSet, GameObject gameObject)
    {
        AkSoundEngine.SetSwitch(switchGroupName, switchToSet, gameObject);
        Debug.Log("WWISE: Switch set to " + switchToSet + " on gameObject " + gameObject.name);
    }
}
