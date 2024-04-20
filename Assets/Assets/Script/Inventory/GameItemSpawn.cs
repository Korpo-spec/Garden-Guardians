using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _itemBasePrefab;


    public void SpawnItem(ItemStack itemStack)
    {
        if (_itemBasePrefab==null)
        {
            return;
        }

        var item = PrefabUtility.InstantiatePrefab(_itemBasePrefab) as GameObject;
        item.transform.position = transform.position;
        var gameItemScript = item.GetComponent<GameItem>();
        
        gameItemScript.stack = new ItemStack();
        gameItemScript.stack._item = itemStack._item;
        gameItemScript.stack.numberOfItemsInStack = itemStack.numberOfItemsInStack;
        gameItemScript.stack.maxStackSize = itemStack.maxStackSize;
        
        gameItemScript.Throw(transform.forward);
    }
}
