using UnityEngine;

[CreateAssetMenu(fileName = "AudioTriggerEvent", menuName = "AudioEvent/AudioTriggerToggle")]
public class AudioTriggerToggle : ScriptableObject
{
    [SerializeField]
    protected bool toggle;

    public void Toggle(string emitter)
    {
        AudioManagerEvents.TriggerAudio(emitter, toggle);
    }
}
