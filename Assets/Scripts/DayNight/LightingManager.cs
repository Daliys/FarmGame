using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] private LightPreset lightPreset;
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private float daySpeed;
    
    private void FixedUpdate()
    {
        if(lightPreset == null) return;
        
        timeOfDay += Time.deltaTime * daySpeed;
        timeOfDay %= 24;
        UpdateLighting(timeOfDay/24);
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
