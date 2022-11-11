using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightPreset lightPreset;

    public void UpdateTime(float timePercent)
    {
        UpdateLighting(timePercent);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightPreset.ambientColor.Evaluate(timePercent);
        RenderSettings.fogColor = lightPreset.fogColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = lightPreset.directionalColor.Evaluate(timePercent);
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3((timePercent * 360) -90f, -170f, 0));
        }
        
    }
}
