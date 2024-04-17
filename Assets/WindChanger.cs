using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindChanger : MonoBehaviour
{
   public VectorVariable windDirection;
   
   public float frequency = 1.0f; // Frequency of the sine wave
   public float amplitude = 1.0f; // Amplitude of the sine wave
   public float angleScale=30f;

   private void Update()
   {
      updateWindDirection();
   }

   private void updateWindDirection()
   {
      float angleOffset = Mathf.Sin(Time.time * frequency) * amplitude;
        
      // Rotate the base wind direction around the y-axis
      windDirection.VectorValue = Quaternion.AngleAxis(angleOffset*angleScale, Vector3.up)*Vector3.forward;

      windDirection.floatValue= angleOffset;
   }
}
