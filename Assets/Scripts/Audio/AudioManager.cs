using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject[] Listeners;
    public string[] Soundbanks;
    public bool combatMusicOnStart;

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
            AkSoundEngine.LoadBank(Soundbanks[i], out uint bankID);
        }

        //Listener setting
        AddListeners(gameObject, 4);
    }

    private void Start()
    {
        AkSoundEngine.SetState("STATE_Music_DuelState_Stan", "False");
        AkSoundEngine.SetState("STATE_Music_DuelState_Marta", "False");
        AkSoundEngine.SetState("STATE_Music_DuelState_Medusa", "False");
        AkSoundEngine.SetState("STATE_Music_DuelState_Don", "False");
        if (combatMusicOnStart)
            AkSoundEngine.SetState("STATE_Music_Main", "Fight_End");
        else
            AkSoundEngine.SetState("STATE_Music_Main", "Fight_Silence");
        AkSoundEngine.PostEvent("SEGMENTS_DuelMusic", gameObject);
        AkSoundEngine.PostEvent("SEGMENTS_MainMusic", gameObject);
        AkSoundEngine.PostEvent("AMB_Bed_Sea", gameObject);
        AkSoundEngine.PostEvent("AMB_Bed_Waves", gameObject);
        AkSoundEngine.PostEvent("AMB_Bed_Wind", gameObject);
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

    public void PostEvent(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, gameObject);
    }
}
