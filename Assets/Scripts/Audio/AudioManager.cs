using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject[] Listeners;
    public string[] Soundbanks;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        
        //SoundBanks Loading
        for (int i=0; i<Soundbanks.Length; i++)
        {
            AkSoundEngine.LoadAndDecodeBank(Soundbanks[i], true, out uint bankID);
        }
    } 

    public void AddListeners(GameObject in_emitter, int listener1)
    {
        AkSoundEngine.AddListener(in_emitter, Listeners[listener1]);
    }
    public void AddListeners(GameObject in_emitter, int listener1, int listener2)
    {
        AkSoundEngine.AddListener(in_emitter, Listeners[listener1]);
        AkSoundEngine.AddListener(in_emitter, Listeners[listener2]);
    }
    public void AddListeners(GameObject in_emitter, int listener1, int listener2, int listener3)
    {
        AkSoundEngine.AddListener(in_emitter, Listeners[listener1]);
        AkSoundEngine.AddListener(in_emitter, Listeners[listener2]);
        AkSoundEngine.AddListener(in_emitter, Listeners[listener3]);
    }
    public void AddListeners(GameObject in_emitter, int listener1, int listener2, int listener3, int listener4)
    {
        AkSoundEngine.AddListener(in_emitter, Listeners[listener1]);
        AkSoundEngine.AddListener(in_emitter, Listeners[listener2]);
        AkSoundEngine.AddListener(in_emitter, Listeners[listener3]);
        AkSoundEngine.AddListener(in_emitter, Listeners[listener4]);
    }
}
