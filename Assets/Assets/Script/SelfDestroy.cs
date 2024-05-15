using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float DestroyAfter;

    [SerializeField]
    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        StartCoroutine(DestroyObject(DestroyAfter));
    }

    private IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        _lineRenderer.positionCount = 0;
        Destroy(gameObject);
        
    }
}
