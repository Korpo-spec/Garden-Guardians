using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioTriggerEvent", menuName = "AudioEvent/AudioTriggerEvent")]
public class AudioTriggerEvent : ScriptableObject
{
    [SerializeField]
    protected string emitter;

    public void Toggle(bool toggle)
    {
        AudioManagerEvents.TriggerAudio(emitter, toggle);
    }
}
