using Inventories;
using UnityEngine;

public class REF : MonoBehaviour
{
   public static REF Instance;
 
   
   [SerializeField] private Game game;
   [SerializeField] private TasksManager tasksManager;
   [SerializeField] private PlayerInventory playerInventory;
   [SerializeField] private StoreHouseInventory storeHouseInventory;
   [SerializeField] private PurchaseProcessing purchaseProcessing;
   [SerializeField] private UI ui;
   [SerializeField] private DayTimer dayTimer;

   [SerializeField] private GameObject worldObjectsCanvas;

   
   
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(this);
      }
   }
   
   
   
   
   public Game Game => game;

   public TasksManager TasksManager => tasksManager;

   public PlayerInventory PlayerInventory => playerInventory;

   public StoreHouseInventory StoreHouseInventory => storeHouseInventory;

   public GameObject WorldObjectsCanvas => worldObjectsCanvas;

   public PurchaseProcessing PurchaseProcessing => purchaseProcessing;
   
   public UI UI => ui;
   
   public DayTimer DayTimer => dayTimer;

}
