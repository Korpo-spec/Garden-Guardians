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

    public void ChangePositionJeck()
    {
        
        
        int pos = Random.Range(0, 4);

        if (pos == 1 && safePos1X != transform.position.x)
        {
            transform.SetLocalPositionAndRotation(new Vector3(safePos1X, transform.position.y, safePos1Z), transform.localRotation);
        }
        else if (pos == 2 && safePos2X != transform.position.x)
        {
            transform.SetLocalPositionAndRotation(new Vector3(safePos2X, transform.position.y, safePos2Z), transform.localRotation);
        }
        else if(pos == 3 && safePos3X != transform.position.x)
        {
            transform.SetLocalPositionAndRotation(new Vector3(safePos3X, transform.position.y, safePos3Z), transform.localRotation);
        }
        else
        {
            ChangePositionJeck();
        }
    }

    // On Reload
    // gameObject.transform.position.x = 306.41f;
    // gameObject.transform.position.z = 231.74f;
}
