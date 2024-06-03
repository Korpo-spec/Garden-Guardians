using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipCinematic : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    private float videoTime;

    private void Start()
    {
        videoTime = (float)videoPlayer.length;
        Invoke("LoadNextScene", videoTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
