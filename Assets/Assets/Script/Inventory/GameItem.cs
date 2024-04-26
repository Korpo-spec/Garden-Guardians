using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameItem : MonoBehaviour
{
    //public Item ItemData;

    [FormerlySerializedAs("Stack")] public ItemStack stack;

    [FormerlySerializedAs("_spriteRenderer")] [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Collider _trigger;

    [Header("Throw settings")]
    [FormerlySerializedAs("TimeBeforeTriggerEnable")] [SerializeField] private float timeBeforeTriggerEnable = 1f;
    private Rigidbody _thisrb;
    [SerializeField] private float _throwGravity;
    [SerializeField] private float _minXForce=3;
    [SerializeField] private float _maxXForce=5;
    
    [SerializeField] private float _throwYForce=5;

    private void Awake()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        _trigger = GetComponent<Collider>();
        _thisrb = GetComponent<Rigidbody>();
        _trigger.enabled = false;
    }

    private void Start()
    {
        spriteRenderer.sprite = stack._item.icon;
        StartCoroutine(EnableTrigger(timeBeforeTriggerEnable));
    }

    private IEnumerator EnableTrigger(float time)
    {
        yield return new WaitForSeconds(time);
        _trigger.enabled = true;
    }

    public void Throw(Vector3 direction)
    {
        _thisrb.useGravity = true;
        var throwForce = Random.Range(_minXForce, _maxXForce);
        _thisrb.velocity = new Vector3(direction.x, _throwYForce, direction.z)*throwForce;
        StartCoroutine(DisableGravity(throwForce));
    }

    private IEnumerator DisableGravity(float atYvelocity)
    {
        yield return new WaitUntil(()=>_thisrb.velocity.y<-atYvelocity);
        _thisrb.velocity = Vector3.zero;
        _thisrb.useGravity = false;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0,0.5f,0));
    }
    
    public static event EventHandler ItemPickUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (other.GetComponent<InventoryHolder>().Inventory.TryAddItem(stack))
            {
                ItemPickUp?.Invoke(this,null);
                Destroy(gameObject);
            }
            
        }
    }

    

    private void OnValidate()
    {
        if (spriteRenderer==null)
        {
            
            TryGetComponent<SpriteRenderer>(out spriteRenderer);
        }
        spriteRenderer.sprite = stack._item.icon;
    }
}
