using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PumpkinAudio : MonoBehaviour
{
    public EventReference PumpkinUnborrow;
    public EventReference PumpkinAttack;
    
    
    
    
    public void PlayPumpkinUnborrow()
    {
        RuntimeManager.PlayOneShotAttached(PumpkinUnborrow,gameObject);
    }
    public void PlayPumpkinAttack()
    {
        RuntimeManager.PlayOneShotAttached(PumpkinAttack,gameObject);
    }
}
