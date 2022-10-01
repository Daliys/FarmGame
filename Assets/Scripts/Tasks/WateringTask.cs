using System;
using UnityEngine;
using UnityEngine.AI;

namespace Tasks
{
    /// <summary>
    /// Task which do watering the garden.
    /// First to move to well, after move to garden and water
    /// </summary>
    public class WateringTask : BaseTask
    {
        // Here 2 Tasks. First to move to well, second to move to garden 
        private readonly MoveInPositionTask _moveToWell;
        private readonly MoveInPositionTask _moveToGarden;

        /// <summary>
        /// If false - updating moveToWell Task
        /// if true - updating moveToGarden Task
        /// </summary>
        private bool _isMoveToWellFinish;

        private readonly Garden _garden;

        public WateringTask(NavMeshAgent navMeshAgent, Garden garden, Vector3 wellPosition,
            Action<bool> actionWhenTaskFinished) : base(actionWhenTaskFinished)
        {
            _garden = garden;
            Vector3 gardenPosition = garden.transform.position;
            _moveToWell = new MoveInPositionTask(navMeshAgent, wellPosition, OnMovingToWellTaskEnd);
            _moveToGarden = new MoveInPositionTask(navMeshAgent, gardenPosition, OnMovingToGardenTaskEnd);
        }

        public override void OnStart()
        {
            _moveToWell.OnStart();    
        }

        public override void Execute()
        {
            if (!_isMoveToWellFinish)
            {
                _moveToWell.Execute();
            }
            else
            {
                _moveToGarden.Execute();
            }
        }

        /// <summary>
        /// Action When NavMeshAgent came to Well
        /// Starting Taking water from well and after starting task to come to Garden
        /// </summary>
        /// <param name="isFinishedSuccessful">Is the Moving task was successful?</param>
        private void OnMovingToWellTaskEnd(bool isFinishedSuccessful)
        {
            if (isFinishedSuccessful)
            {
                _moveToGarden.OnStart();
                _isMoveToWellFinish = true;
            }
            else
            {
                ActionWhenTaskFinished.Invoke(false);
            }
        }

        /// <summary>
        /// Action When NavMeshAgent came to Garden
        /// Starting Watering and finish this Task
        /// </summary>
        /// <param name="isFinishedSuccessful">Is the Moving task was successful?</param>
        private void OnMovingToGardenTaskEnd(bool isFinishedSuccessful)
        {
            _garden.StartWatering();
            ActionWhenTaskFinished.Invoke(isFinishedSuccessful);
        }
        
    }
}