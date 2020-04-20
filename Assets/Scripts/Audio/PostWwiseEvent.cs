using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public int[] listeners;
    private bool listenersInit = false;
    public GameObject defaultGameObject;

    private void Start()
    {
        if (defaultGameObject == null)
        {
            defaultGameObject = this.gameObject;
        }
    }

    private void PostEvent(string eventName)
    {
        if (!listenersInit)
        {
            for (int i = 0; i < listeners.Length; i++)
            {
                AudioManager.instance.AddListeners(defaultGameObject, listeners[i]);
            }
        }

        AkSoundEngine.PostEvent(eventName, this.gameObject);
        Debug.Log("WWISE: Event " + eventName + " posted on " + this.gameObject.name);
    }

    private void PostEvent(string eventName, GameObject in_gameObject)
    {
        if (!listenersInit)
        {
            for (int i = 0; i < listeners.Length; i++)
            {
                AudioManager.instance.AddListeners(in_gameObject, listeners[i]);
            }
        }

        AkSoundEngine.PostEvent(eventName, gameObject);
        Debug.Log("WWISE: Event " + eventName + " posted on " + in_gameObject.name);
    }
}
