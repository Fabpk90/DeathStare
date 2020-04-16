using System;
using UnityEngine;

namespace Audio
{
    public class PostEventOnBeginMatch : MonoBehaviour
    {
        public string eventName;

        private void Awake()
        {
            GameMode.OnStartOfMatch += OnMatchStart;
        }

        private void OnMatchStart(object sender, EventArgs eventArgs)
        {
            AkSoundEngine.PostEvent(eventName, gameObject);
        }
    }
}