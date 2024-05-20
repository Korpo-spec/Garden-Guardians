using System;
using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    public Slider healthSlider;
    public Slider healthEaseSlider;
    public EntityHealth EntityHealth;
    private float lerpSpeed=0.05f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        healthSlider.value = EntityHealth.health / EntityHealth.maxHealth;
    }

    private void LateUpdate()
    {
        
        healthEaseSlider.value = Mathf.Lerp(healthEaseSlider.value, healthSlider.value, lerpSpeed);
        transform.LookAt(transform.position+mainCamera.transform.forward);
        
    }


    public void UpdateSliderValue()
    {
        Debug.Break();
        healthEaseSlider.value = healthSlider.value;
        healthSlider.value = EntityHealth.health / EntityHealth.maxHealth;
        
    }

    
}
