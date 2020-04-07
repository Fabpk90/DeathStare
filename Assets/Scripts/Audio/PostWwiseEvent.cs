using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.AddListeners(this.gameObject, 0);
        AkSoundEngine.PostEvent("Play_Main_Music", this.gameObject);
    }

    public void Play_Foleys_Character1_Run_Footsteps()
    {
        AkSoundEngine.PostEvent("Play_Foleys_Character1_Run_Footsteps", this.gameObject);
    }

    public void Play_Foleys_Character1_Walk_Footsteps()
    {
        AkSoundEngine.PostEvent("Play_Foleys_Character1_Walk_Footsteps", this.gameObject);
    }
}
