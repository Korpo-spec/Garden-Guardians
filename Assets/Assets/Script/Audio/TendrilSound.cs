using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class TendrilSound : MonoBehaviour
{

    public EventReference damageSound;
    public void PlayDamageSound()
    {
        RuntimeManager.PlayOneShotAttached(damageSound,gameObject);
    }
}
