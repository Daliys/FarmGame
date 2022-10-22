using System;
using Inventories;
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
        // Here Task. First to move and harvest the garden and add to Player Inventory
        private readonly MoveInPositionTask _moveToGarden;


        /// <summary>
        /// If false - updating moveToWell Task
        /// if true - updating moveToGarden Task
        /// </summary>
        private bool _isMoveToGardenFinish;

        //ref
        private readonly Garden _garden;
        private readonly PlayerInventory _inventory;

        public HarvestingTask(NavMeshAgent navMeshAgent, Garden garden, PlayerInventory inventory,
            Action<bool> actionWhenTaskFinished) : base(actionWhenTaskFinished)
        {
            _garden = garden;
            _inventory = inventory;
            Vector3 gardenPosition = garden.transform.position;
            _moveToGarden = new MoveInPositionTask(navMeshAgent, gardenPosition, OnMovingToGardenTaskEnd);
        }

        public override void OnStart()
        {
            _moveToGarden.OnStart();
        }

        public override void Execute()
        {
            _moveToGarden.Execute();
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
                _garden.StartHarvesting(_inventory);
            }

            ActionWhenTaskFinished.Invoke(isFinishedSuccessful);
        }

    }
}