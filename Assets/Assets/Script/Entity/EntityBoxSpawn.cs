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
    [SerializeField] private bool activateOnPlayerProximity;
    [SerializeField] private float activateDistance;
    
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private int maxSpawnedEntities = 10;
    [SerializeField] private List<SpawnData> spawnData = new List<SpawnData>();
    [SerializeField] private LayerMask terrainLayer;
    private HashSet<GameObject> _aliveObjects = new HashSet<GameObject>();
    
    private float _spawnTimer = 0;

    private bool _active;

    private GameObject _playerObject;
    // Start is called before the first frame update
    void Start()
    {
        _active = !activateOnPlayerProximity;
        _playerObject = GameObject.FindWithTag("Player");

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
        randomPos.y -= 0.5f;
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
        if (activateOnPlayerProximity)
        {
            _active = CheckIfPlayerInRange();
        }
       
        if (_spawnTimer >= spawnRate && _active)
        {
            _spawnTimer = 0;
            SpawnEntity();
        }
    }

    private Vector3 _drawVector;
    [SerializeField]private bool _inRange;
    [SerializeField] private bool activateDebug;
    private bool CheckIfPlayerInRange()
    {
        Vector3 playerToSpawnerVec = _playerObject.transform.position - transform.position;
        playerToSpawnerVec.y = 0;
        playerToSpawnerVec.Normalize();
        playerToSpawnerVec.x = playerToSpawnerVec.x > 0 ? 1 : -1;
        playerToSpawnerVec.z = playerToSpawnerVec.z > 0 ? 1 : -1;

        playerToSpawnerVec.x *= size.x/2;
        playerToSpawnerVec.z *= size.z/2;

        float distance = Vector3.Distance(playerToSpawnerVec + transform.position, _playerObject.transform.position);
        _drawVector = playerToSpawnerVec;
        if (activateDistance > distance)
        {
            _inRange = true;
            return true;
        }
        _inRange = false;

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _inRange ? Color.green : Color.red;
        
        Gizmos.DrawWireCube(transform.position, size);
        if (!activateDebug)
        {
            return;
        }
        Gizmos.DrawSphere(transform.position + _drawVector, 0.5f);

        int segments = 32;
        float angleBetweenPoints = 360 / (float)segments;
        Vector2 originalPoint = Vector2.up;
        Vector3 prevPoint = Vector3.forward*activateDistance;
        Vector3 vec3Point = Vector3.zero;
        float yValue = transform.position.y;
        Vector3 originalWorldPoint = transform.position;
        for (int i = 1; i < segments+1; i++)
        {
            vec3Point.x = originalPoint.RotateVector2(angleBetweenPoints*i* Mathf.Deg2Rad).x * activateDistance;
            vec3Point.z = originalPoint.RotateVector2(angleBetweenPoints*i* Mathf.Deg2Rad).y * activateDistance;
            
            Gizmos.DrawLine(prevPoint + originalWorldPoint + _drawVector, vec3Point +  originalWorldPoint + _drawVector);
            prevPoint = vec3Point;
        }
    }
}

[System.Serializable]
public class SpawnData
{
    public GameObject spawnObject;
    public float spawnChance;
}