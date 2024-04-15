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

    [Range(0, 1)] public float minGrow = 0.2f;
    [Range(0, 1)] public float maxGrow = 0.97f;

    public List<Material> GrowMaterials = new List<Material>();
    private bool fullyGrown;
    private static readonly int GrowID = Shader.PropertyToID("_Grow");

    
    private void Start()
    {
        float RandomGrowStart = Random.Range(0f, 0.9f);
        
        
        //GrowMeshes.Add(gameObject.GetComponent<MeshRenderer>());
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
                 GrowMeshes[i].materials[j].SetFloat(GrowID,RandomGrowStart);   
                 GrowMaterials.Add(GrowMeshes[i].materials[j]);
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
        transform.localScale = new Vector3(transform.localScale.x,Mathf.Lerp(1f, 1.5f, t),transform.localScale.z);
    }

    private bool repeat=true;
    IEnumerator GrowMesh(Material material)
    {
        float growValue = material.GetFloat(GrowID);

        while (repeat)
        {
            if (!fullyGrown)
            {
                if (!repeat)
                {
                    yield break;
                }
                while (growValue<maxGrow)
                {
                    growValue += 1 / (timeToGrow / refreshrate);
                    material.SetFloat(GrowID,growValue);

                    yield return new WaitForSeconds(refreshrate);
                }
            }
            else
            {
                if (!repeat)
                {
                    
                }
                while (growValue>minGrow)
                {
                    growValue -= 1 / (timeToGrow / refreshrate);
                    material.SetFloat(GrowID,growValue);

                    yield return new WaitForSeconds(refreshrate);
                }
            }


            if (repeat)
            {
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
