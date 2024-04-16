using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    public Item ItemData;

    private SpriteRenderer _spriteRenderer;

    private Collider trigger;


    private void Awake()
    {
        _spriteRenderer=GetComponent<SpriteRenderer>();
        trigger = GetComponent<Collider>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = ItemData.icon;
        
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0,0.5f,0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.GetComponent<InventoryHolder>().Inventory.TryAddItem(ItemData))
            {
                Destroy(gameObject);
            }
            
        }
    }
}
