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

    public Collider collider;

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
        
        for (int i = 0; i < GrowMaterials.Count; i++)
        {
            StartCoroutine(GrowMesh(GrowMaterials[i]));
        }
    }

    private void Update()
    {
        LerpYScale();
        
    }

    private float lerpTimer = 0;
    private float lerpTimerMax = 3;
    private bool increaselerpTimer;
    private void LerpYScale()
    {

        if (increaselerpTimer)
        {
            lerpTimer += Time.deltaTime;
        }
        else
        {
            lerpTimer -= Time.deltaTime;
        }

        increaselerpTimer = lerpTimer switch
        {
            < 0 => true,
            > 3 => false,
            _ => increaselerpTimer
        };

        var t = lerpTimer / lerpTimerMax;
        transform.localScale = new Vector3(transform.localScale.x,Mathf.Lerp(0.5f, 2.5f, t),transform.localScale.z);
    }

    private bool repeat=true;
    IEnumerator GrowMesh(Material material)
    {
        float growValue = material.GetFloat(GrowID);

        while (repeat)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            repeat = false;
            for (int i = 0; i < GrowMaterials.Count; i++)
            {
                StartCoroutine(changeScale(GrowMaterials[i]));
            }
        }
    }

    IEnumerator changeScale(Material material)
    {
        material.SetFloat("_Scale",8);
        yield return null;
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
