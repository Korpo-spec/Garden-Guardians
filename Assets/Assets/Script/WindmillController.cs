using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindmillController : MonoBehaviour
{

    public VectorVariable windDirection;

    public Transform turnTransform;
    public Transform RotationGroup;
    


    
    
    private void Update()
    {
        RotateTurn();
        RotateRotationGroup();
    }

    private void RotateTurn()
    {
        turnTransform.Rotate(0.5f*windDirection.floatValue,0,0);
    }

    private void RotateRotationGroup()
    {
        RotationGroup.forward = windDirection.VectorValue;
    }
    
    
}
