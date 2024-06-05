using FMODUnity;
using UnityEngine;
using UnityEngine.Events;

public class AudioManagerEvents : MonoBehaviour
{
    [System.Serializable]
    public struct AudioParameter
    {
        public string parameter;
        public float value;

        public AudioParameter(string name, float arg)
        {
            parameter = name;
            value = arg;
        }
    }

    public enum AudioToggle
    {
        Play,
        Stop,
        Force,
    }

    [SerializeField]
    private SerializableDictionary<string, StudioEventEmitter> emitterDictionary;

#if UNITY_EDITOR
    public SerializableDictionary<string, StudioEventEmitter> Emitters => emitterDictionary;
#endif

    public static UnityAction<string, bool> triggerAudio;
    public static UnityAction<bool> triggerAudioGlobal;
    public static UnityAction<string, AudioParameter> changeAudio;
    public static UnityAction<AudioParameter> changeAudioGlobal;

    protected static AudioManagerEvents Instance;

    public static void TriggerAudio(string name, bool play)
    {
        if (string.IsNullOrEmpty(name))
        {
            triggerAudioGlobal?.Invoke(play);
        }
        else
        {
            triggerAudio?.Invoke(name, play);
        }
    }
    public static void ChangeAudio(string name, AudioParameter parameter)
    {
        if (string.IsNullOrEmpty(name))
        {
            changeAudioGlobal?.Invoke(parameter);
        }
        else
        {
            changeAudio?.Invoke(name, parameter);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
        triggerAudio += OnTriggerAudio;
        triggerAudioGlobal += OnTriggerAudioGlobal;
        changeAudio += OnChangeAudio;
        changeAudioGlobal += OnChangeAudioGlobal;
    }

    private void OnChangeAudioGlobal(AudioParameter parameter)
    {
        foreach (var emitter in emitterDictionary.Values)
        {
            emitter.SetParameter(parameter.parameter, parameter.value);
        }
    }

    private void OnChangeAudio(string name, AudioParameter parameter)
    {
        if (emitterDictionary.TryGetValue(name, out var emitter))
        {
            emitter.SetParameter(parameter.parameter, parameter.value);
        }
    }

    private void OnTriggerAudioGlobal(bool play)
    {
        foreach (var emitter in emitterDictionary.Values)
        {
            if (play)
            {
                if (!emitter.IsPlaying())
                {
                    emitter.Play();
                }
            }
            else
            {
                emitter.Stop();
            }
        }
    }

    private void OnTriggerAudio(string name, bool play)
    {
        if (emitterDictionary.TryGetValue(name, out var emitter))
        {
            if (play)
            {
                if (!emitter.IsPlaying())
                {
                    emitter.Play();
                }
            }
            else
            {
                emitter.Stop();
            }
        }
    }

    public void StopBackgrounMusic()
    {
        
    }
}
