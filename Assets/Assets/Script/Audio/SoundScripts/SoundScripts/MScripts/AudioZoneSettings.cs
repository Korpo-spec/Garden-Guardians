using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioZoneSettings : AudioTrigger
{
    //What is the point of redoing everything AudioTrigger does?
    /*
    public enum TriggerAudio
    {
        Play,
        Stop,
        SetParameter,
        None
    }

    [System.Serializable]
    public class AudioSettings
    {
        public string eTag;
        [HideInInspector]public StudioEventEmitter sEmitter;
        public TriggerAudio action = TriggerAudio.None;
        public string paramName = "";
        public float paramValue = 0;
    }

    [HideInInspector] public AudioManager audioManager;
    [NonReorderable, SerializeField] private AudioSettings[] audioSettings;
    // Start is called before the first frame update
    void Start()
    {
        
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        for (int j = 0; j < audioSettings.Length; j++)
        {
            audioSettings[j].sEmitter = GameObject.FindWithTag(audioSettings[j].eTag).GetComponent<StudioEventEmitter>();
 
        }
        foreach (AudioSettings aSetting in audioSettings)
        {
            switch (aSetting.action)
            {
                case TriggerAudio.Play:
                    audioManager.Play(aSetting.sEmitter);
                    break;
                case TriggerAudio.Stop:
                    audioManager.Stop(aSetting.sEmitter);
                    break;
                case TriggerAudio.SetParameter:
                    audioManager.SetParameter(aSetting.sEmitter, aSetting.paramName, aSetting.paramValue);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private void Start()
    {
        TriggerPlay();
    }
}
