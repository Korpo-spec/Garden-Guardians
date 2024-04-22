using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleSpawner : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private int amountPoints;

    [SerializeField]private float _angleBetweenPoints;
    [SerializeField]private Vector3[] _points;
    
    private void OnValidate()
    {
        if (amountPoints  <= 0)
        {
            return;
        }
        _angleBetweenPoints = 360 / (float)amountPoints;

        _points = new Vector3[amountPoints];
        Vector2 originalPoint = Vector2.up;
        Vector3 vec3Point = Vector3.zero;
        for (int i = 0; i < amountPoints; i++)
        {
            
            vec3Point.x = originalPoint.RotateVector2(_angleBetweenPoints*i* Mathf.Deg2Rad).x * radius;
            vec3Point.z = originalPoint.RotateVector2(_angleBetweenPoints*i* Mathf.Deg2Rad).y * radius;
            _points[i] = vec3Point;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        if (_points == null)
        {
            return;
        }
        for (int i = 0; i < _points.Length; i++)
        {
            
            Gizmos.DrawLine(_points[i]+ transform.position,_points[(i+1) % _points.Length]+transform.position);
        }
    }
}
