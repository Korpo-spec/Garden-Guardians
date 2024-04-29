using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;


public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;

    public PlayerAudio playerAudio;
    public InteractablesAudio interactablesAudio;
    public AmbientAudio ambientAudio;
    public EnemiesAudio enemiesAudio;
    public AnimationAudio animationAudio;
    public MusicAudio musicAudio;
    public SnapshotAudio snapshotAudio;
    public DialogueAudio DialogueAudio;
    // public 

   [Header("Dialogue Sound Option: ")] 
   //public StudioEventEmitter DialogueManager;

   public EventInstance DialogueInstance;
   
    //Why set this to a singleton if it exists in every scene?
   private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //Removing this and putting to dontdestroy? bad combo!
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
/*
   public void DialogueUpdate(InteractableDialogueWindow.DialogueOpt opt, InteractableDialogueWindow.NPC npc)
   {
        DialogueInstance = RuntimeManager.CreateInstance(DialogueAudio.dialogue);
        //DialogueManager.SetParameter("DialogueOpt", (int)opt);
        //DialogueManager.SetParameter("NPC", (int)npc);
        DialogueInstance.stop(STOP_MODE.IMMEDIATE);
        DialogueInstance.setParameterByName("DialogueOpt", (int) opt);
        DialogueInstance.setParameterByName("NPC", (int) npc);
        DialogueInstance.start();
        DialogueInstance.release();
   }
    */     
}
