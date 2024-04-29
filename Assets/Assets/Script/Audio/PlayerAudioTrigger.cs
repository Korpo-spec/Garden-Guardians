using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerAudioTrigger : MonoBehaviour
{
   public PlayerAudio playerAudio;


   public void PlayTakeDamage()
   {
      RuntimeManager.PlayOneShotAttached(playerAudio.playerDamage,gameObject);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.C))
      {
         PlayTakeDamage();
      }
   }
}
