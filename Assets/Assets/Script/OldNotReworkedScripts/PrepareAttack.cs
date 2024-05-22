using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PrepareAttack : MonoBehaviour
{
    [SerializeField] private float _attackWindup;
    [SerializeField] private GameObject meshParent;
    
    private Material _material;
    private Animator _animator;
    private NavMeshAgent _agent;
    private float _time;
    private Texture emissionTexture;
    private void Start()
    {
        _material = GetCommonMaterial();
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        emissionTexture = _material.GetTexture("_EmissionMap");
        
    }

    private Material GetCommonMaterial()
    {
        Renderer[] renderers = meshParent.GetComponentsInChildren<Renderer>();
        Material mat = renderers[0].material;
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = mat;
        }

        return mat;
    }
    
    public void StartPrepareAttack()
    {
        StartCoroutine(WindUpAttack());
    }

    private IEnumerator WindUpAttack()
    {
        _time = 0;
        _animator.speed = 0;
        _agent.isStopped = true;
        
        _material.SetTexture("_EmissionMap", Texture2D.whiteTexture);
        _material.SetColor("_EmissionColor", Color.black);
        _material.SetColor("_Emissive_Color", Color.black);

        while (1 > _time)
        {
            _time += Time.deltaTime * (1/_attackWindup);
            Color color = Color.Lerp(Color.black, Color.white, _time);
            _material.SetColor("_EmissionColor", color);
            _material.SetColor("_Emissive_Color", color);
            _material.EnableKeyword("_EMISSION");
            yield return new WaitForEndOfFrame();
        }
        _animator.speed = 1;
         _material.SetTexture("_EmissionMap", emissionTexture);
        _material.SetColor("_EmissionColor", Color.black);
        _material.SetColor("_Emissive_Color", Color.black);

    }
    
    public void StartHurtFlash()
    {
        StartCoroutine(HurtFlash());
    }

    private IEnumerator HurtFlash()
    {
        _time = 0;
        
        
        _material.SetTexture("_EmissionMap", Texture2D.whiteTexture);
        _material.SetColor("_EmissionColor", Color.black);
        while (1 > _time)
        {
            _time += Time.deltaTime * (1/_attackWindup);
            Color color;
            if (_time > 0.5f)
            {
                color = Color.Lerp(Color.red,Color.black, (_time-0.5f)*2);
            }
            else
            {
                color = Color.Lerp(Color.black, Color.red, _time*2);
            }
            
            _material.SetColor("_EmissionColor", color);
            _material.EnableKeyword("_EMISSION");
            yield return new WaitForEndOfFrame();
        }
        
        _material.SetTexture("_EmissionMap", emissionTexture);
        _material.SetColor("_EmissionColor", Color.black);
    }
}
