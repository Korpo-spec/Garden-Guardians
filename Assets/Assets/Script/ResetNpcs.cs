using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class ResetNpcs : MonoBehaviour
{
    [SerializeField]private List<Character> characterList;

    private Character[] tempList;
    // Start is called before the first frame update
    void Start()
    {
        tempList = GetComponentsInChildren<Character>();
        for (int i = 0; i < tempList.Length; i++)
        {
            if (!tempList[i].NameMatch("Cory"))
            {
                characterList.Add(tempList[i]);   
            }
        }
    }
    

    public void ResetNpc()
    {
        for (int i = 0; i < characterList.Count; i++)
        {
            characterList[i].gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
