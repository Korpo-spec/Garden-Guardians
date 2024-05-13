using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTimer : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private float DestroyafterSec=0.4f;
    
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        StartCoroutine(DestroyAfter());
    }
    
    private void Update()
    {
        RotateTowardCamera();
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(DestroyafterSec);
        Destroy(gameObject);
    }
    
    private void RotateTowardCamera()
    {
        var RotDirection = transform.position - cam.transform.position;
        transform.forward = RotDirection.normalized;
    }
}
