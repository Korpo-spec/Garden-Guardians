using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class BlightConnectionHandler : MonoBehaviour
{
    private List<BlightShader> connectedCorruption;
    
    public UnityEvent Triggercleanse;


    private void Awake()
    {
        connectedCorruption = new List<BlightShader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        getInfectedGameObejcts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void getInfectedGameObejcts()
    {
        var allobejcts=Physics.OverlapSphere(transform.localPosition, 10);
        
        foreach (var obeject in allobejcts)
        {
            if (obeject.gameObject.TryGetComponent<BlightShader>(out BlightShader blight))
            {
                connectedCorruption.Add(blight);
            }
        }
    }

    public void TriggerDecreaseblight()
    {
        Triggercleanse.Invoke();
        
        foreach (var VARIABLE in connectedCorruption)
        {
            VARIABLE.DecreaseBlight();
        }
    }
}
