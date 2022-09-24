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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        MouseControl.OnMouseButtonClicked += OnMouseClicked;
    }

    private void OnDisable()
    {
        MouseControl.OnMouseButtonClicked -= OnMouseClicked;
    }
    
    private void OnMouseClicked(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }
}
