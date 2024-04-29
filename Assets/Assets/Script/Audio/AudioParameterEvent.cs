using UnityEngine;

[CreateAssetMenu(fileName = "AudioParameterEvent", menuName = "AudioEvent/AudioParameterEvent")]
public class AudioParameterEvent : AudioTriggerEvent
{
    [SerializeField]
    protected string parameter;
    [SerializeField]
    protected float value;

    public void SetParameter(string newEmitter)
    {
        AudioManagerEvents.ChangeAudio(newEmitter, new AudioManagerEvents.AudioParameter(parameter, value));
    }

    public void SetValue(float newValue)
    {
        AudioManagerEvents.ChangeAudio(emitter, new AudioManagerEvents.AudioParameter(parameter, newValue));
    }
}
