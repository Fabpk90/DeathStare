using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public string StartEvent;
    public Listener listener;
    public GameObject[] Listeners;

    void Awake()
    {

    }

    private void Start()
    {
        //AkSoundEngine.PostEvent(StartEvent, this.gameObject);
        AudioManager.instance.AddListeners(this.gameObject, 0);
        AkSoundEngine.PostEvent("Play_Test", this.gameObject);
    }

    private void PostStartEvent()
    {

    }
}
