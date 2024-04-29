using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu(menuName = "Audio/Ambient_Audio", order = 3)]

public class AmbientAudio : ScriptableObject
{

    public EventReference Goop_Loop;
    public EventReference Goop_Fall;
    public EventReference Conveyor;
    public EventReference Final_Gate;
    public EventReference Unknown_V;
    public EventReference Unknown_VI;
    public EventReference Unknown_VII;
    public EventReference Unknown_VIII;
    public EventReference Unknown_IX;
    public EventReference Unknown_X;

    public void GoopLoopAudio(GameObject loopObj)
    {
        RuntimeManager.PlayOneShotAttached(Goop_Loop, loopObj);
    }
    
    public void GoopFallAudio(GameObject fallObj)
    {
        RuntimeManager.PlayOneShotAttached(Goop_Fall, fallObj);
    }
    
    public void ConveyorAudio(GameObject conObj)
    {
        RuntimeManager.PlayOneShotAttached(Conveyor, conObj);
    }
    
    public void FinalGateAudio(GameObject gateObj)
    {
        RuntimeManager.PlayOneShotAttached(Final_Gate, gateObj);
    }
    /*
    public void GoopLoopAudio(GameObject loopObj)
    {
        RuntimeManager.PlayOneShotAttached(Goop_Loop, loopObj);
    }
    */
    
}
