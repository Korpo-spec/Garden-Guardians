using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator Animator;


    private void Start()
    {
        TeleportController.PlayerTeleport += PlayFade;
        Animator.SetBool("Fade",true);
    }
    
    
    public void PlayFade(object o,Vector3 pos)
    {
        Debug.Log("true");
        Animator.SetBool("Fade",true);
        
    }

    public void setFadeFalse()
    {
        Debug.Log("False");
        Animator.SetBool("Fade",false);
    }

    private void OnDestroy()
    {
        TeleportController.PlayerTeleport -= PlayFade;
    }
}
