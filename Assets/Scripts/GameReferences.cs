using UnityEngine;

public class GameReferences : MonoBehaviour
{
   public static GameReferences Instance;
 
   
   [SerializeField] private Game game;
   [SerializeField] private TasksManager tasksManager;
   [SerializeField] private Inventory inventory;
   
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

   public Inventory Inventory => inventory;

   public GameObject WorldObjectsCanvas => worldObjectsCanvas;

}
