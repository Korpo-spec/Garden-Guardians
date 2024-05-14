using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photographmode : MonoBehaviour
{
    Camera mainCamera;
    Camera photographCamera;
    bool photographMode = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        photographCamera = GetComponent<Camera>();
        photographCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (photographMode)
            {
                photographMode = false;
                FindObjectOfType<PlayerController>().enabled = true;
                mainCamera.enabled = true;
                photographCamera.enabled = false;
                Debug.Log("Photograph mode deactivated");
            }
            else
            {
                Debug.Log("Photograph mode activated");
                FindObjectOfType<PlayerController>().enabled = false;
                mainCamera.enabled = false;
                photographCamera.enabled = true;
                photographMode = true;
            }
            
        }
    }
}
