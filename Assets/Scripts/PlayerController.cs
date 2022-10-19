using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    
    [SerializeField]private Animator animator;

    private const string IsMoving = "IsMoving";
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
       
    }

    private void Update()
    {
      animator.SetBool(IsMoving, _navMeshAgent.velocity.magnitude > 0.01f);
      
      InstantlyRotation(_navMeshAgent.destination);
    }


    private void InstantlyRotation(Vector3 destination)
    {
        if ((destination - transform.position).magnitude < 0.1f) return; 
     
        Vector3 direction = (destination - transform.position).normalized;
        Quaternion  qDir= Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 20);

    }
}
