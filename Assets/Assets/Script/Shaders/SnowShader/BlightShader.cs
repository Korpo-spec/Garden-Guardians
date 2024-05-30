using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlightShader : MonoBehaviour
{
    private static readonly int SnowAmount = Shader.PropertyToID("_SnowAmount");
    public MeshRenderer meshRenderer;

    private int BlightSources;



    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        smallRandomiseToAmount(meshRenderer.material);
    }

    public void DecreaseBlight()
    {
        BlightSources--;
        if (BlightSources==0)
        {
            StartCoroutine(DecreaseBligt(meshRenderer.material));
        }
        
    }

    private float maxAmount;
    private void smallRandomiseToAmount(Material material)
    {
        var Amount = material.GetFloat(SnowAmount);
        var random = Random.Range(-0.1f, 0.1f);
        maxAmount = Amount + random;
        material.SetFloat(SnowAmount,Amount+random);
    }

    public void increaseBlightScource()
    {
        BlightSources++;
    }

    IEnumerator DecreaseBligt(Material material)
    {
        StopCoroutine(IncreaseBligt(meshRenderer.material));
        
        material.SetFloat(SnowAmount,1);
        
        var AmountValue = material.GetFloat(SnowAmount);
        
        while (AmountValue>0)
        {
            AmountValue -= 1 / (6 / 0.1f);
            material.SetFloat(SnowAmount,AmountValue);

            yield return new WaitForSeconds(0.1f);
        }
        if (gameObject.layer==3)
        {
            gameObject.layer = 0;
        }
       
        yield return new WaitForEndOfFrame();
    }
    IEnumerator IncreaseBligt(Material material)
    {
        material.SetFloat(SnowAmount,0);
        var AmountValue = material.GetFloat(SnowAmount);
        
        while (AmountValue<0.85f)
        {
            AmountValue += 1 / (6 / 0.1f);
            material.SetFloat(SnowAmount,AmountValue+material.GetFloat(SnowAmount));
            Debug.Log(material.GetFloat(SnowAmount));
            yield return new WaitForSeconds(0.1f);
        }
        if (gameObject.layer==3)
        {
            gameObject.layer = 0;
        }
       
        yield return new WaitForEndOfFrame();
    }

    public void SpawnBlightAmount()
    {
        StartCoroutine(IncreaseBligt(meshRenderer.material));
    }
}
