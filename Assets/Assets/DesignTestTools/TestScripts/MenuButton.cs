using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject panel2;
    
    // Start is called before the first frame update
    void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void GameplayPressed()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void MenusPressed()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }
}
