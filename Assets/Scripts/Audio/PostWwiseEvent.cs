using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.AddListeners(this.gameObject, 0, 1);
        AkSoundEngine.PostEvent("Play_Main_Music", this.gameObject);
    }
}
