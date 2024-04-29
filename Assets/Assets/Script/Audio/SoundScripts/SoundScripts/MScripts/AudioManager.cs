using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ghost")]
    [SerializeField] 
    private StudioEventEmitter ghostForm;
    public GameObject playerGhost;
    private bool isGhost;
    
    [Header("Combat")]
    private bool inCombat = false;
    public float combatWaitTime = 3f;
    public EventReference combatReference;
    private EventInstance combatInstance;

    [SerializeField] private string combatParameterName;
    [SerializeField] private float combatParameterValue = 1f;
    [SerializeField] private string paramName = "";
    [SerializeField] private float paramValue = 0f;

    [Header("Boss")]
    [SerializeField] private StudioEventEmitter bossMusic;
    [SerializeField] private string bossParamName;
    [SerializeField] private float bossParamValue = 0f;

    [Header("PlayerDeath")]
    [SerializeField] private StudioEventEmitter playerDeath;

    [BankRef] public string masterBank; //här kan man lägga till flera banker om de används.
    public bool loadSampleData;


    public float timer = 0f;
    public float stopTime = 6f;


    /*public void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }*/

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Update()
    {
        /*if(inCombat)
        {
            timer += Time.deltaTime;
            
        }*/

        //No null checks and constant lookups? That is slow
        /*
        if (playerGhost == null)
        {
            playerGhost = GameObject.Find("GhostPlayer");
        }
        
        if (playerGhost.activeSelf)
        {
            SetParameter(ghostForm, combatParameterName, combatParameterValue);
        } 
        
        else if (!playerGhost.activeSelf)
        {
            SetParameter(ghostForm, combatParameterName, 0);
        }
        */
    }

    public void Ghost()
    {
        if (isGhost)
        {
            //Debug.Log("Value: " + combatParameterValue);
            RuntimeManager.StudioSystem.setParameterByName(combatParameterName, combatParameterValue);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(combatParameterName, 0f);
        }
        
    }
    
    public void Combat()
    {
        Debug.Log("Hi!");
        if(!inCombat)
        {
            //combatInstance = RuntimeManager.CreateInstance(combatReference);
            //combatInstance.start();   //för att använda snapshot
            inCombat = true;
            //combatEmitter.Play();
            RuntimeManager.StudioSystem.setParameterByName(combatParameterName, combatParameterValue);
            StartCoroutine(ExitCombatTimer());
                        
            Debug.Log("Combat has started");

        }

        if(inCombat)
        {
            StopAllCoroutines();
            StartCoroutine(ExitCombatTimer());
        }


    }

    private IEnumerator ExitCombatTimer()
    {
        yield return new WaitForSeconds(combatWaitTime);
        RuntimeManager.StudioSystem.setParameterByName(combatParameterName, 0f);
        //combatInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //combatInstance.release();
        inCombat = false;
        Debug.Log("Combat has stopped");
    }


    public void PlayBoss()
    {
        if (bossMusic.IsActive == false)
        {
            bossMusic.Play();
        }
        bossMusic.SetParameter(bossParamName, bossParamValue);
    }

    public void PlayBoss(float value)
    {
        bossParamValue = value;
        if (bossMusic.IsActive == false)
        {
            bossMusic.Play();
        }
        bossMusic.SetParameter(bossParamName, bossParamValue);
    }

    public void Play(StudioEventEmitter emitter)
    {
        if (emitter != null && emitter.IsActive == false)
        {
            emitter.Play();
            Debug.Log("Music is playing!");
        }
    }
    public void Stop(StudioEventEmitter emitter)
    {
        emitter.Stop();
        Debug.Log("Music stopped!");
    }

    public void SetParameter(StudioEventEmitter emitter, string paramName, float paramValue)
    {
        emitter.SetParameter(paramName, paramValue);
    }

    public void PlayerDeath()
    {
        playerDeath.Play();
    }
}
