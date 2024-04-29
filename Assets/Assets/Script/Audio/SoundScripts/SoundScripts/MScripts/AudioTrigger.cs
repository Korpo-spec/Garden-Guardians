using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    /*
    public enum TriggerAudio
    {
        Play,
        Stop,
        SetParameter,
        None
    }
    */

    //Why make this a class if you can make it cheap with a struct
    /*
    [System.Serializable]
    //public class AudioSettings
    public struct AudioSettings
    {
        //public StudioEventEmitter sEmitter;
        public string eTag;
        public TriggerAudio action;
        public string paramName;
        public float paramValue;
    }
    */

    /*
    [SerializeField]
    protected AudioManager audioManager;
    */
    [SerializeField]
    [NonReorderable]
    protected AudioSettings[] audioSettings;

    /*
    private void Start()
    {
        //Now why would you do this? AudioManager can be made into a singleton or better yet directly serialized
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        for (int j = 0; j < audioSettings.Length; j++)
        {
            audioSettings[j].sEmitter = GameObject.FindWithTag(audioSettings[j].eTag).GetComponent<StudioEventEmitter>();
        }
    }
    */

    public void TriggerPlay()
    {
        /*
        //You assign this prior and no null check? Serialization avoids that
        if (audioManager == null)
        {
            return;
        }
        */
        int len = audioSettings.Length;
        if (len != 0)
        {
            for (int j = 0; j < len; ++j)
            {
                audioSettings[j].Trigger();
            }
        }
    }
}
