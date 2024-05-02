using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using UnityEngine.Serialization;


[CreateAssetMenu(menuName = "Audio/Player_Audio", order = 1)]


public class PlayerAudio : ScriptableObject
{

    public EventReference playerDamage;
    public EventReference playerStep;
    public EventReference playerDash;
    public EventReference playerAttack;
    public EventReference playerDeath;
    public EventReference playerRespawn;
    public EventReference ghostMode;
    public EventReference Unknown_II;
    public EventReference Unknown_III;
    public EventReference Unknown_IV;
    public EventReference Unknown_V;
    public EventReference Unknown_VI;
    public EventReference Unknown_VII;
    public EventReference Unknown_VIII;
    public EventReference Unknown_IX;
    public EventReference Unknown_X;

    public void PlayerStepAudio(GameObject stepObj)
    {
        RuntimeManager.PlayOneShotAttached(playerStep, stepObj);
    }
    
    public void PlayerLandAudio(GameObject landObj)
    {
        RuntimeManager.PlayOneShotAttached(playerAttack, landObj);
    }
    public void PlayerDashAudio(GameObject jumpObj)
    {
        RuntimeManager.PlayOneShotAttached(playerDash, jumpObj);
    }
    
    public void PlayerGhostModeAudio(GameObject ghostObj)
    {
        RuntimeManager.PlayOneShotAttached(ghostMode, ghostObj);
    }
    
    
    
    
}
