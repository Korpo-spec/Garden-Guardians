using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHayStack : MonoBehaviour
{
    [SerializeField] private bool used = false;
    
    void OnTriggerStay()
    {
        if (!used && Input.GetKeyDown(KeyCode.E))
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);

            used = true;

            GetComponent<Collider>().enabled = false;
        }
    }
}
