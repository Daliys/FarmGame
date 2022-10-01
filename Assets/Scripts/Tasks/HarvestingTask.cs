using System;
using UnityEngine;
using UnityEngine.AI;

namespace Tasks
{
    /// <summary>
    /// Task which do harvesting the garden.
    /// First to move to garden and harvest the crop, after move to storeHouse
    /// </summary>
    public class HarvestingTask : BaseTask
    {
        // Here 2 Tasks. First to move and harvest the garden, second to bring the crop to storeHouse 
        private readonly MoveInPositionTask _moveToGarden;
        private readonly MoveInPositionTask _moveToStoreHouse;

        /// <summary>
        /// If false - updating moveToWell Task
        /// if true - updating moveToGarden Task
        /// </summary>
        private bool _isMoveToGardenFinish;

        private readonly Garden _garden;

        public HarvestingTask(NavMeshAgent navMeshAgent, Garden garden, Vector3 storeHousePosition,
            Action<bool> actionWhenTaskFinished) : base(actionWhenTaskFinished)
        {
            _garden = garden;
            Vector3 gardenPosition = garden.transform.position;
            _moveToGarden = new MoveInPositionTask(navMeshAgent, gardenPosition, OnMovingToGardenTaskEnd);
            _moveToStoreHouse = new MoveInPositionTask(navMeshAgent, storeHousePosition, OnMovingToStoreHouseTaskEnd);
        }

        public override void OnStart()
        {
            _moveToGarden.OnStart();    
        }

        public override void Execute()
        {
            if (!_isMoveToGardenFinish)
            {
                _moveToGarden.Execute();
            }
            else
            {
                _moveToStoreHouse.Execute();
            }
        }

        /// <summary>
        /// Action When NavMeshAgent came to storeHouse
        /// Starting place crop to storeHouse and after finish this task
        /// </summary>
        /// <param name="isFinishedSuccessful">Is the Moving task was successful?</param>
        private void OnMovingToStoreHouseTaskEnd(bool isFinishedSuccessful)
        {
            
            //_garden.StartWatering();
            ActionWhenTaskFinished.Invoke(isFinishedSuccessful);
          
        }
        
        /// <summary>
        /// Action When NavMeshAgent came to garden and harvest the crop
        /// Starting task bringing the crop to StoreHouse
        /// </summary>
        /// <param name="isFinishedSuccessful">Is the Moving task was successful?</param>
        private void OnMovingToGardenTaskEnd(bool isFinishedSuccessful)
        {
            if (isFinishedSuccessful)
            {
                _garden.StartHarvesting();
                _moveToStoreHouse.OnStart();
                _isMoveToGardenFinish = true;
            }
            else
            {
                ActionWhenTaskFinished.Invoke(false);
            }
        }
        
    }
}