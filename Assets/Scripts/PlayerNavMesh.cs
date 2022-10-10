using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
}
