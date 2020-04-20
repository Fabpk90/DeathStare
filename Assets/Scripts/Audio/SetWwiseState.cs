using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWwiseState : MonoBehaviour
{
    public string stateGroupName;
    public string startingStateName;

    private void Awake()
    {
        if (stateGroupName != null && startingStateName != null)
        {
            AkSoundEngine.SetState(stateGroupName, startingStateName);
            Debug.Log("WWISE: State set to " + startingStateName);
        }
    }

    public void Setstate(string stateToSet)
    {
        AkSoundEngine.SetState(stateGroupName, stateToSet);
        Debug.Log("WWISE: State set to " + stateToSet);
    }
}
