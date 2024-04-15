using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockShortcutScript : MonoBehaviour
{
    void OnTriggerEnter()
    {
        Destroy(gameObject);
    }
}
