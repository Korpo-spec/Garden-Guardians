/*This script created by using docs.unity3d.com/ScriptReference/MonoBehaviour.OnParticleCollision.html*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Script.Entity;

public class HS_ParticleCollisionInstance : MonoBehaviour
{
    public GameObject[] EffectsOnCollision;
    public float DestroyTimeDelay = 5;
    public bool UseWorldSpacePosition;
    public float Offset = 0;
    public Vector3 rotationOffset = new Vector3(0,0,0);
    public bool useOnlyRotationOffset = true;
    public bool UseFirePointRotation;
    public bool DestoyMainEffect = false;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private ParticleSystem ps;

    [SerializeField] private AttackComboSO attackModule;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }
    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);     
        for (int i = 0; i < numCollisionEvents; i++)
        {
            foreach (var effect in EffectsOnCollision)
            {
                EntityFaction faction;
                
                var instance = Instantiate(effect, collisionEvents[i].intersection + collisionEvents[i].normal * Offset, new Quaternion()) as GameObject;
                
                if (other.TryGetComponent(out faction))
                {
                    if (faction.faction==Faction.Enemy)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    if (faction.faction != Faction.Enemy)
                    {
                        if (!UseWorldSpacePosition) instance.transform.parent = transform;
                        if (UseFirePointRotation) { instance.transform.LookAt(transform.position); }
                        else if (rotationOffset != Vector3.zero && useOnlyRotationOffset) { instance.transform.rotation = Quaternion.Euler(rotationOffset); }
                        else
                        {
                            instance.transform.LookAt(collisionEvents[i].intersection + collisionEvents[i].normal);
                            instance.transform.rotation *= Quaternion.Euler(rotationOffset);
                        }
                        
                        var entityAttack = other.gameObject.GetComponent<EntityAttack>();
                        if (entityAttack)
                        {
                            
                            entityAttack._health.DamageUnit(attackModule.attackInfos[0].damage,gameObject.transform.forward*attackModule.attackInfos[0].knockBack,false,entityAttack);
                        }
                    }
                }
               
                Destroy(instance, DestroyTimeDelay);
            }
        }

        if (DestoyMainEffect != true) return;
        {
            EntityFaction faction;
            if (!other.TryGetComponent(out faction)) return;
            if (faction.faction != Faction.Enemy)
            {
                Destroy(gameObject);
            }
        }
    }
    
}
