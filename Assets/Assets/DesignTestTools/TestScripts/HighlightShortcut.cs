using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighlightShortcut : MonoBehaviour
{
    [SerializeField] private GameObject shortcut;
    [SerializeField] private GameObject fence1;
    [SerializeField] private GameObject fence2;
    
    // Start is called before the first frame update
    void Start()
    {
        fence1.SetActive(false);
        fence2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (shortcut.IsDestroyed())
        {
            fence1.SetActive(true);
            fence2.SetActive(true);
        }
    }
}
