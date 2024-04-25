using System;
using System.Collections;
using System.Collections.Generic;
using Script.Entity;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EntityBoxSpawn : MonoBehaviour
{
    [SerializeField] private Vector3 size;
    
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int maxSpawnedEntities = 10;
    [SerializeField] private List<SpawnData> spawnData = new List<SpawnData>();
    [SerializeField] private LayerMask terrainLayer;
    private HashSet<GameObject> _aliveObjects = new HashSet<GameObject>();
    
    private float _spawnTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    private void NormalizeSpawnChance()
    {
        float totalChance = 0;
        foreach (var data in spawnData)
        {
            totalChance += data.spawnChance;
        }

        for (int i = 0; i < spawnData.Count; i++)
        {
            spawnData[i].spawnChance /= totalChance;
        }
    }
    
    private GameObject GetRandomSpawnObject()
    {
        float random = Random.Range(0f, 1f);
        float current = 0;
        for (int i = 0; i < spawnData.Count; i++)
        {
            current += spawnData[i].spawnChance;
            if (random <= current)
            {
                return spawnData[i].spawnObject;
            }
        }

        return null;
    }
    
    private void SpawnEntity()
    {
        if (_aliveObjects.Count >= maxSpawnedEntities)
        {
            return;
        }

        GameObject spawnObject = GetRandomSpawnObject();
        if (spawnObject == null)
        {
            return;
        }
        Vector3 randomPos = new Vector3(Random.Range(-size.x/2, size.x/2), transform.position.y, Random.Range(-size.z/2, size.z/2)) + transform.position;
        NavMesh.SamplePosition(randomPos, out var hit, 1000, NavMesh.AllAreas);
        //Physics.Raycast(randomPos, Vector3.down, out var hit, 1000, terrainLayer);
        

        randomPos = hit.position;
        GameObject entity = Instantiate(spawnObject, randomPos, Quaternion.identity);
        SubscribeToDeathEvent(entity);
        _aliveObjects.Add(entity);
    }

    
    
    private void SubscribeToDeathEvent(GameObject entity)
    {
        EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
        if (entityHealth)
        {
            entityHealth.OnDeath += OnEntityDeath;
        }
    }
    
    private void UnsubscribeToDeathEvent(GameObject entity)
    {
        EntityHealth entityHealth = entity.GetComponent<EntityHealth>();
        if (entityHealth)
        {
            entityHealth.OnDeath -= OnEntityDeath;
        }
    }
    
    private void OnEntityDeath(GameObject entity)
    {
        _aliveObjects.Remove(entity);
        UnsubscribeToDeathEvent(entity);
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= spawnRate)
        {
            _spawnTimer = 0;
            SpawnEntity();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    
    }
}

[System.Serializable]
public class SpawnData
{
    public GameObject spawnObject;
    public float spawnChance;
}