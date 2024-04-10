using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blighthandler : MonoBehaviour
{
    private static readonly int SnowAmount = Shader.PropertyToID("_SnowAmount");
    public MeshRenderer meshRenderer;

    public List<MeshRenderer> connectedCorruption;


    public void DecreaseBlight()
    {
        StartCoroutine(DecreaseBligt(meshRenderer.material));
    }

    IEnumerator DecreaseBligt(Material material)
    {
        var AmountValue = material.GetFloat(SnowAmount);
        while (AmountValue>0)
        {
            AmountValue -= 1 / (4 / 0.1f);
            material.SetFloat(SnowAmount,AmountValue);

            yield return new WaitForSeconds(0.1f);
        }
       
        yield return new WaitForEndOfFrame();
    }
}
