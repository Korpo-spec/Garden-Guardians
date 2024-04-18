using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour
{
    //public Item ItemData;

    public ItemStack Stack;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    private Collider trigger;


    private void Awake()
    {
        _spriteRenderer=GetComponent<SpriteRenderer>();
        trigger = GetComponent<Collider>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = Stack._item.icon;
        
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0,0.5f,0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.GetComponent<InventoryHolder>().Inventory.TryAddItem(Stack))
            {
                // if (Stack._item.itemType == ItemType.Equipment)
                // {
                //     other.GetComponent<EntityAttack>().weapon = Stack._item.equipment;
                // }
                Destroy(gameObject);
            }
            
        }
    }

    private void OnValidate()
    {
        if (_spriteRenderer==null)
        {
            
            TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        }
        _spriteRenderer.sprite = Stack._item.icon;
    }
}
