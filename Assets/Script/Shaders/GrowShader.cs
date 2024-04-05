using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowShader : MonoBehaviour
{
    public List<MeshRenderer> GrowMeshes;
    public float timeToGrow = 5;
    public float refreshrate = 0.05f;

    [Range(0, 1)] public float minGrow = 0.2f;
    [Range(0, 1)] public float maxGrow = 0.97f;

    private List<Material> GrowMaterials = new List<Material>();
    private bool fullyGrown;
    private static readonly int GrowID = Shader.PropertyToID("_Grow");

    private void Start()
    {
        for (int i = 0; i < GrowMeshes.Count; i++)
        {
            for (int j = 0; j < GrowMeshes[i].materials.Length; j++)
            {
                if (GrowMeshes[i].materials[j].HasProperty(GrowID))
                {
                 GrowMeshes[i].materials[j].SetFloat(GrowID,minGrow);   
                 GrowMaterials.Add(GrowMeshes[i].materials[j]);
                 Debug.Log("added");
                }
                
            } 
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Grow");
            for (int i = 0; i < GrowMaterials.Count; i++)
            {
                StartCoroutine(GrowMesh(GrowMaterials[i]));
            }
        }
    }

    IEnumerator GrowMesh(Material material)
    {
        float growValue = material.GetFloat(GrowID);

        if (!fullyGrown)
        {
            while (growValue<maxGrow)
            {
                growValue += 1 / (timeToGrow / refreshrate);
                material.SetFloat(GrowID,growValue);

                yield return new WaitForSeconds(refreshrate);
            }
        }
        else
        {
            while (growValue>minGrow)
            {
                growValue -= 1 / (timeToGrow / refreshrate);
                material.SetFloat(GrowID,growValue);

                yield return new WaitForSeconds(refreshrate);
            }
        }

        if (growValue>=maxGrow)
        {
            fullyGrown = true;
        }
        else
        {
            fullyGrown = false;
        }
    }
}
