using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TasksManager : MonoBehaviour
{
    private List<BaseTask> _taskList;

    private BaseTask _currentTask;
    [SerializeField]private NavMeshAgent playerNavMeshAgent;

    private void Awake()
    {
        _taskList = new List<BaseTask>();
        ActionWhenTaskFinished(true);
    }

    private void Update()
    {
        if (_currentTask != null)
        {
            _currentTask.Execute();
        }
        else
        {
            StartNewTask();
        }
    }

    private void ActionWhenTaskFinished(bool isFinishedSuccessful)
    {
        if (isFinishedSuccessful)
        {
            _taskList.Remove(_currentTask);
            StartNewTask();
        }
    }

    private void StartNewTask()
    {
        if (_taskList.Count != 0)
        {
            _currentTask = _taskList[0];
            _currentTask.OnStart();
        }
        else
        {
            _currentTask = null;
        }
    }

    private void OnEnable()
    {
        MouseControl.OnMouseButtonClicked += MouseControlOnOnMouseButtonClicked;
    }

    private void OnDisable()
    {
        MouseControl.OnMouseButtonClicked -= MouseControlOnOnMouseButtonClicked;
    }

    private void MouseControlOnOnMouseButtonClicked(RaycastHit hit)
    {
        if (hit.collider.CompareTag(Tags.Ground))
        {
            AddMoveToPositionAction(hit.point);   
        }
    }
  

    public void AddMoveToPositionAction(Vector3 position)
    {
        if (playerNavMeshAgent)
        {
            MoveInPositionTask moveInPositionTask = 
                new MoveInPositionTask(playerNavMeshAgent, position, ActionWhenTaskFinished);
            _taskList.Add(moveInPositionTask);
        }
    }
    
    
    
    
    
}
