using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("Time Settings")]
    [SerializeField] private float playerTimeScale;
    [SerializeField] private bool timeStopped;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    // Method to set the time scale
    public void SetPlayerTimeScale(float timeScale)
    {
        playerTimeScale = timeScale;

        // Tell all objects in the scene to update their time scale
        BroadcastMessage("OnTimeScaleChanged", timeScale, SendMessageOptions.DontRequireReceiver);
    }

    // Method to get the time scale
    public float GetPlayerTimeScale()
    {
        return playerTimeScale;
    }

    // Method to stop/resume time
    public void StopTime(bool state)
    {
        timeStopped = state;
    }

    // Method to get the time stopped state
    public bool IsTimeStopped()
    {
        return timeStopped;
    }
}
