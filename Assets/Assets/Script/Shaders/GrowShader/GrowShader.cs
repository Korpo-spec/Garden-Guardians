using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GrowShader : MonoBehaviour
{
    
    public List<MeshRenderer> GrowMeshes;
    public float timeToGrow = 5;
    public float refreshrate = 0.05f;
    
    public List<Material> GrowMaterials = new List<Material>();
    private bool fullyGrown;
    private static readonly int GrowID = Shader.PropertyToID("_Grow");

    
    private void Start()
    {

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GrowMeshes.Add(gameObject.transform.GetChild(i).GetComponent<MeshRenderer>());
        }
        
        for (int i = 0; i < GrowMeshes.Count; i++)
        {
            for (int j = 0; j < GrowMeshes[i].materials.Length; j++)
            {
                if (GrowMeshes[i].materials[j].HasProperty(GrowID))
                {
                   
                 GrowMaterials.Add(GrowMeshes[i].materials[j]);
                }
                
            } 
        }
    }
    

   
    IEnumerator DeGrow(Material material)
    {
        var growValue = material.GetFloat(GrowID);
        timeToGrow = 3;
        while (growValue>0.2f)
        {
            growValue -= 1 / (timeToGrow / refreshrate);
            material.SetFloat(GrowID,growValue);

            yield return new WaitForSeconds(refreshrate);
        }
        Destroy(gameObject);
        yield return null;
    }

    public void OnDeath()
    {
        StopAllCoroutines();
        for (int i = 0; i < GrowMaterials.Count; i++)
        {
            StartCoroutine(DeGrow(GrowMaterials[i]));
        }
        
    }

    private void OnValidate()
    {
        if (timeToGrow<=0)
        {
            timeToGrow = 0.1f;
            Debug.LogWarning("timeToGrow can't be 0 or else the editor chrases in runtime");
        }
    }
}
