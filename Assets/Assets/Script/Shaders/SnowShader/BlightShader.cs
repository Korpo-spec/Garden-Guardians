using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlightShader : MonoBehaviour
{
    private static readonly int SnowAmount = Shader.PropertyToID("_SnowAmount");
    private MeshRenderer meshRenderer;

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

    private void smallRandomiseToAmount(Material material)
    {
        var Amount = material.GetFloat(SnowAmount);
        var random = Random.Range(-0.1f, 0.1f);
        material.SetFloat(SnowAmount,Amount+random);
    }

    public void increaseBlightScource()
    {
        BlightSources++;
    }

    IEnumerator DecreaseBligt(Material material)
    {
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
}
