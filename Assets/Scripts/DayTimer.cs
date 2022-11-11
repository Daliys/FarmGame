using System.Collections;
using UIS;
using UnityEngine;

public class DayTimer : MonoBehaviour
{

    [SerializeField] private UIClockUpdater uiClockUpdater;

    [SerializeField] private LightingManager lightingManager;
    
    
    // The coefficient is dependence between 1 hour o game world to <n> min in real life 
    // now it's 1.0 because 1 hour of game world = 1 min real life
    private const float timeCoefficient = 1f;
    private const float timeSpeed = 1f;
    
    private const float dayDuration = 24 * 60 * timeCoefficient;
    private const float startDayAt = 7 * 60 * timeCoefficient;
    private const float canSkipDayAt = 22 * 60 * timeCoefficient;
    private const float endDayAt = 01 * 60 * timeCoefficient;

    private float currentTime;

    private bool isButtonSkipSended;


    private void Start()
    {
        SkipDay();
        StartCoroutine(TimeUpdater());
    }

    private void FixedUpdate()
    {
        if(REF.Instance.Game.IsPaused) return;
        
        currentTime += Time.deltaTime * timeSpeed;
        currentTime %= dayDuration;
        lightingManager.UpdateTime(GetCurrentTimeForLightning());
    }

    /// <summary>
    /// Updating time and checking for time events
    /// </summary>
    /// <returns></returns>
    private IEnumerator TimeUpdater()
    {
        while (true)
        {
            if(REF.Instance.Game.IsPaused) continue;

            CheckTimeForEvents();
            uiClockUpdater.UpdateClockTime(GetTimeForClock());
            lightingManager.UpdateTime(GetCurrentTimeForLightning());
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    /// <summary>
    /// Checking current time for time event like "end of day","enable skipp button", etc
    /// </summary>
    private void CheckTimeForEvents()
    {
        if (currentTime >= canSkipDayAt && !isButtonSkipSended)
        {
            uiClockUpdater.SetEnableSkipButton(true);
            isButtonSkipSended = true;
        }
        else if (currentTime >= endDayAt && currentTime < startDayAt)
        {
            SkipDay();
        }
      
    }

    /// <summary>
    /// Skipping day to time startDayAt
    /// </summary>
    public void SkipDay()
    {
        isButtonSkipSended = false;
        uiClockUpdater.SetEnableSkipButton(false);
        currentTime = startDayAt;
    }
    
    /// <summary>
    /// Get Time in percent (0-100%) 
    /// </summary>
    /// <returns>Percent of current time</returns>
    private float GetCurrentTimeForLightning()
    {
        return currentTime / dayDuration;
    }

    /// <summary>
    /// Getting Time for UI textView in format "09:34"
    /// </summary>
    /// <returns>Time now in time format</returns>
    private string GetTimeForClock()
    {
        var hour = (int)(currentTime / 60);
        var min = (int)(currentTime % 60);
        return $"{hour:D2}:{min:D2}";
    }
}
