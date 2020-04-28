using System;
using UnityEngine;

namespace Audio
{
    public class PostOnMatchStartWwiseEvent : MonoBehaviour
    {
        public string eventName;
        public int[] listeners;

        private void Awake()
        {
            GameMode.OnStartOfMatch += OnMatchStart;
        }

        private void OnMatchStart(object sender, EventArgs eventArgs)
        {
            for(int i=0; i<listeners.Length; i++)
            {
                AudioManager.instance.AddListeners(gameObject, listeners[i]);
            }
            AkSoundEngine.PostEvent(eventName, gameObject);
        }
    }
}