using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSetup : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    // Start is called before the first frame update

    private void Awake()
    {
        
        if (!Camera.main.GetComponent<CinemachineBrain>())
        {
            Camera.main.AddComponent<CinemachineBrain>();
        }
    }

    void Start()
    {
        
        _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        if (FindObjectOfType<PlayerController>())
        {
            var playerTransform =FindObjectOfType<PlayerController>().transform ;
            _virtualCamera.Follow = playerTransform;
            _virtualCamera.LookAt = playerTransform;
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
