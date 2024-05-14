using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float DestroyAfter;

    private void Start()
    {
        StartCoroutine(DestroyObject(DestroyAfter));
    }

    private IEnumerator DestroyObject(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
