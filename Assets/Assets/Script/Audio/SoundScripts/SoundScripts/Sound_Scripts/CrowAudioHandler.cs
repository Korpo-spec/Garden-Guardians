using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class CrowAudioHandler : MonoBehaviour
{
    public EventReference CrowSlash;
    public EventReference CrowShoot;
    public EventReference CrowGrunt;


    public void PlayCrowSlash()
    {
        RuntimeManager.PlayOneShotAttached(CrowSlash,gameObject);
    }
    public void PlayCrowShoot()
    {
        RuntimeManager.PlayOneShotAttached(CrowShoot,gameObject);
    }public void PlayCrowGrunt()
    {
        RuntimeManager.PlayOneShotAttached(CrowGrunt,gameObject);
    }
}
