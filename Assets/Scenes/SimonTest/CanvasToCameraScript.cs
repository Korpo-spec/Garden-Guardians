using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToCameraScript : MonoBehaviour
{
    public GameObject camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Instantiate(camera) as GameObject;
        //camera.renderMode = RenderMode.ScreenSpaceCamera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
