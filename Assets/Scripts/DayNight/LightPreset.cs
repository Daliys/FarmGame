using UnityEngine;

[CreateAssetMenu(fileName = "LightPreset", menuName = "ScriptableObjects/LightPreset")]
public class LightPreset : ScriptableObject
{
   public Gradient ambientColor;
   public Gradient directionalColor;
   public Gradient fogColor;
}
