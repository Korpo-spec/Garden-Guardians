using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(menuName = "Audio/Dialogue_Audio", order = 8)]

public class DialogueAudio : ScriptableObject
{

    public EventReference dialogue;

    public void dialogueAudio(GameObject dialogueObj)
    {
        RuntimeManager.PlayOneShotAttached(dialogue, dialogueObj);
    }
}
