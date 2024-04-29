public enum TriggerAudio
{
    Play,
    Stop,
    SetParameter,
    None
}

[System.Serializable]
public struct AudioSettings
{
    //public StudioEventEmitter sEmitter;
    public string eTag;
    public TriggerAudio action;
    public string paramName;
    public float paramValue;

    public void Trigger()
    {
        switch (action)
        {
            case TriggerAudio.Play:
                AudioManagerEvents.TriggerAudio(eTag, true);
                break;
            case TriggerAudio.Stop:
                AudioManagerEvents.TriggerAudio(eTag, false);
                break;
            case TriggerAudio.SetParameter:
                AudioManagerEvents.ChangeAudio(eTag, new AudioManagerEvents.AudioParameter(paramName, paramValue));
                break;
        }
    }
}