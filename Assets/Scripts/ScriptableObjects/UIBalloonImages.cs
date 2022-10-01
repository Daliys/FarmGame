using UnityEngine;

namespace ScriptableObjects
{
   /// <summary>
   /// Keeping all the Sprites For GardenBalloon
   /// </summary>
   [CreateAssetMenu(fileName = "UIBalloonImages", menuName = "ScriptableObjects/UIBalloonImages")]
   public class UIBalloonImages : ScriptableObject
   {
      public Sprite watering;

      public Sprite harvesting;
   }
}
