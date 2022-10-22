using UnityEngine;

namespace Inventories
{
    public class StoreHouseInventory:Inventory
    {
        [SerializeField] private GameObject storeHouseLocation;

        /// <summary>
        /// Get Vector3 Position of the Store House location 
        /// </summary>
        public Vector3 GetStoreHouseLocation()
        {
            return storeHouseLocation.transform.position;
        }
    }
}