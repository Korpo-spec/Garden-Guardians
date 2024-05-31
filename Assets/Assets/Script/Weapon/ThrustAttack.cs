using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrustAttack", menuName = "Custom/SpecialAttack/ThrustAttack")]
public class ThrustAttack : SpecialAttack
{
    private Animator _animator;
    private GameObject _activeParticle;
    private EntityMovement _entityMovement;
    private Transform startpoint;
    [SerializeField] private float radius = 1.4f;
    [SerializeField] private float length = 1.4f;
    [SerializeField] private float attackSpeed = 0.5f;
    public override void OnPressed(Animator animator)
    {
        _animator = animator;
        animator.SetBool("SpecialAttack", true);
        _entityMovement = animator.GetComponent<EntityMovement>();
        _entityMovement.canMove = false;
        startpoint = animator.transform;
        //_activeParticle = Instantiate(attackInfo.slashEffect, animator.rootPosition + new Vector3(0,1.5f,0),Quaternion.identity, animator.transform);
    }

    public override void OnHeld()
    {
        _animator.SetBool("SpecialAttack", true);
    }
    
    public override void OnRelease()
    {
        _animator.SetBool("SpecialAttack", false);
    }

    public override void OnSpecialAttack()
    {
        Matrix4x4 rotColliderMatrix = startpoint.localToWorldMatrix;
        Vector3 correctionVec = new Vector3(1, 1, 1);
        //Debug.Log(Vector3.Scale(rotColliderMatrix.MultiplyPoint(_weapon.attackInfos[comboIndex].colliderInfo.center), correctionVec));
        Collider[] colliders = Physics.OverlapBox(
            rotColliderMatrix.MultiplyPoint(Vector3.Scale((Vector3.forward+ new Vector3(0,0.1f,0)) * (length/2), correctionVec))
            , new Vector3(radius/2, 0.5f, length/2),
            rotColliderMatrix.rotation);
        transformHealthDictionary.TryGetHealth(_animator.transform, out EntityHealth thisHealth);
        Debug.Log("collider count: " + colliders.Length);
        foreach (var collider in colliders)
        {
            
            Debug.Log("collider Name: " +collider.transform.gameObject.name);
            if (collider.transform == startpoint)
            {
                continue;
            }

            if (transformHealthDictionary.TryGetHealth(collider.transform, out var health))
            {
                if (health.faction.faction == thisHealth.faction.faction)
                {
                    continue;
                }

                Vector3 knockBackVec = collider.transform.position - startpoint.position;
                knockBackVec = knockBackVec.RemoveY();
                health.DamageUnit(attackInfo.damage, attackInfo.knockBack* knockBackVec,false,null);
                if (attackInfo.hitEffect != null)
                {
                    Instantiate(attackInfo.hitEffect, collider.transform.position, Quaternion.identity);
                }
            }
        }
        _animator.SetBool("SpecialAttack", false);

    }

    public override void OnGizmos(GameObject obj)
    {
        Gizmos.matrix = obj.transform.localToWorldMatrix;
        Vector3 correctionVec = new Vector3(1, 1, 1);
        Gizmos.DrawCube(Vector3.Scale((Vector3.forward+ new Vector3(0,0.1f,0)) * (length/2), correctionVec), new Vector3(radius, 0.5f, length));
    }
}
