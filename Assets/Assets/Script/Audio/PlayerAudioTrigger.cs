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
      
      var instance = RuntimeManager.CreateInstance(playerAudio.playerStep);
      int mask = 1 << 3;
      RaycastHit hit;
      if (Physics.Raycast(transform.position, Vector3.down, out hit, 2, mask))
      {
         //instance.setParameterByNameWithLabel("Footsteps_Type", "Blight");
         instance.setParameterByNameWithLabel("Footsteps_Type", "Gravel");
         
      }
      else
      {
         instance.setParameterByNameWithLabel("Footsteps_Type", "Grass");
      }
      
      instance.start();
   }
}
