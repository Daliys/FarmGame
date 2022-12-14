using System.Collections.Generic;
using Tasks;
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


    private void MouseControlOnOnMouseButtonClicked(RaycastHit hit)
    {
        if (hit.collider.CompareTag(Tags.Ground))
        {
            AddMoveToPositionAction(hit.point);   
        }
    }

    public void AddWateringAction(Garden garden)
    {
        if (playerNavMeshAgent)
        {
            Vector3 wellPosition = GameObject.FindGameObjectWithTag(Tags.Well).transform.position;
            
            WateringTask wateringTask = new WateringTask(playerNavMeshAgent, garden, wellPosition, ActionWhenTaskFinished);
            _taskList.Add(wateringTask);
        }
    }

    public void AddHarvestingAction(Garden garden)
    {
        if (playerNavMeshAgent)
        {
            HarvestingTask harvestingTask = new HarvestingTask(playerNavMeshAgent, garden, REF.Instance.PlayerInventory, ActionWhenTaskFinished);
            _taskList.Add(harvestingTask);
        }
    }

    public void AddMoveToPositionAction(Vector3 position)
    {
        if (!playerNavMeshAgent) return;

        if (_taskList.Count > 0 && _taskList[^1] is MoveInPositionTask )
        { 
           _taskList.RemoveAt(_taskList.Count-1);
           if (_taskList.Count == 0) _currentTask = null;
        }
        
        
        MoveInPositionTask moveInPositionTask = new MoveInPositionTask(playerNavMeshAgent, position, ActionWhenTaskFinished);
        _taskList.Add(moveInPositionTask);

    }

    public void CancelAllTask()
    {
        _taskList.Clear();
        _currentTask = null;
        playerNavMeshAgent.SetDestination(playerNavMeshAgent.transform.position);
    }

    private void OnEnable()
    {
        InputManagerController.OnMouseButtonClicked += MouseControlOnOnMouseButtonClicked;
    }

    private void OnDisable()
    {
        InputManagerController.OnMouseButtonClicked -= MouseControlOnOnMouseButtonClicked;
    }

}
