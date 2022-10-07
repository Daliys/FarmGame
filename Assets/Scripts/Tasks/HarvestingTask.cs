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

        /// <summary>
        /// Information about what we harvest (its what plank and amount of it)
        /// </summary>
        private InventoryItem _inventoryItem;
        
        //ref
        private readonly Garden _garden;
        private readonly Inventory _inventory;
        
        public HarvestingTask(NavMeshAgent navMeshAgent, Garden garden, Inventory inventory,
            Action<bool> actionWhenTaskFinished) : base(actionWhenTaskFinished)
        {
            _garden = garden;
            _inventory = inventory;
            Vector3 gardenPosition = garden.transform.position;
            _moveToGarden = new MoveInPositionTask(navMeshAgent, gardenPosition, OnMovingToGardenTaskEnd);
            _moveToStoreHouse = new MoveInPositionTask(navMeshAgent, inventory.GetStoreHouseLocation(), OnMovingToStoreHouseTaskEnd);
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
            if(_inventoryItem != null) _inventory.AddItem(_inventoryItem);
            
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
                _inventoryItem = _garden.StartHarvesting();
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