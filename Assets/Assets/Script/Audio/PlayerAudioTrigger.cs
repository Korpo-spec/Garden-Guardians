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

   public void PlayDashSound()
   {
      RuntimeManager.PlayOneShotAttached(playerAudio.playerDash,gameObject);
   }
   public void PlayAttackSound()
   {
      RuntimeManager.PlayOneShotAttached(playerAudio.playerAttack,gameObject);
   }


   
   public void PlayFootStep()
   {
      
      var instance = FMODUnity.RuntimeManager.CreateInstance(playerAudio.playerStep);
      if (!Physics.Raycast(transform.position,Vector3.down,2,LayerMask.NameToLayer("Infected")))
      {
         instance.setParameterByNameWithLabel("Footsteps_Type", "Gravel");
         Debug.Log("Infected Ground");
      }
      else
      {
         instance.setParameterByNameWithLabel("Footsteps_Type", "Grass");
      }
      
      instance.start();
   }
}
