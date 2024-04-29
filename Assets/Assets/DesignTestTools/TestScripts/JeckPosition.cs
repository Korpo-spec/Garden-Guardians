using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeckPosition : MonoBehaviour
{
    // Start position
    [SerializeField] private float startPosX;
    [SerializeField] private float startPosZ;
    
    // New position
    [SerializeField] private float newPosX = 306.41f;
    [SerializeField] private float newPosZ = 231.74f;
    
    
    void Start()
    {
        startPosX = transform.position.x;
        startPosZ = transform.position.z;
    }
    

    // On Reload
    // gameObject.transform.position.x = 306.41f;
    // gameObject.transform.position.z = 231.74f;
}
