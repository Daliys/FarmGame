using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Task which moves NavMesh to set position
/// </summary>
public class MoveInPositionTask : BaseTask
{
    /// <summary>
    /// Final position which navMeshAgent have to reach
    /// </summary>
    private readonly Vector3 _positionToMove;
    
    /// <summary>
    /// NavMeshAgent of which we are moving
    /// </summary>
    private readonly NavMeshAgent _navMeshAgent;

    public MoveInPositionTask(NavMeshAgent navMeshAgent, Vector3 positionToMove, Action<bool> actionWhenTaskFinished) :
        base(actionWhenTaskFinished)
    {
        _positionToMove = positionToMove;
        _navMeshAgent = navMeshAgent;
    }

    public override void OnStart()
    {
        _navMeshAgent.SetDestination(_positionToMove);
    }

    public override void Execute()
    {
        if (_navMeshAgent.remainingDistance <= 1)
        {
            ActionWhenTaskFinished.Invoke(true);
        }
    }
}
