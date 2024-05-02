using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeckPosition : MonoBehaviour
{
    // Start position
    [SerializeField] private float startPosX;
    [SerializeField] private float startPosZ;
    
    // Safe pos 1
    [SerializeField] private float safePos1X = 306.41f;
    [SerializeField] private float safePos1Z = 231.74f;
    
    // Safe pos 2
    [SerializeField] private float safePos2X = 439.95f;
    [SerializeField] private float safePos2Z = 545.46f;
    
    // Safe pos 3
    [SerializeField] private float safePos3X = 90.65f;
    [SerializeField] private float safePos3Z = 107.71f;
    
    
    
    void Start()
    {
        startPosX = transform.position.x;
        startPosZ = transform.position.z;
    }
    

    // On Reload
    // gameObject.transform.position.x = 306.41f;
    // gameObject.transform.position.z = 231.74f;
}
