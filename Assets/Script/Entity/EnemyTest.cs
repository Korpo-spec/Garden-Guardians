using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    private EntityMovement _movement;
    private NavMeshAgent _agent;

    private NavMeshPath _path;

    private int _currentCorner = 1;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        _movement = GetComponent<EntityMovement>();
        _agent.destination = new Vector3(15, 0, 5);
        _path = new NavMeshPath();
        _agent.CalculatePath(new Vector3(15, 0, 5), _path);
        _agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _agent.destination = new Vector3(-15, 0, -5);
            _agent.isStopped = true;
        }
        
    }

    private void FixedUpdate()
    {
        if (_agent.path.corners.Length <= 1)
        {
            return;
        }
        
        Vector3 movDir = _agent.path.corners[_currentCorner] - transform.position;
        movDir.y = 0;

        _movement.Move(movDir, _movement.movementStats.speed * Time.deltaTime);
    }
}
