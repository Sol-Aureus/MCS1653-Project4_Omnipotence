using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("References")]
    [SerializeField] private Slider timeSlider;
    [SerializeField] private Image timeSliderFill;
    [SerializeField] private VolumeProfile[] volumeProfiles;

    [Header("Time Settings")]
    [SerializeField] private float playerTimeScale;
    [SerializeField] private bool timeStopped;
    [SerializeField] private int powerType;
    [SerializeField] private float haltDuration;
    [SerializeField] private float rushDuration;
    [SerializeField] private float omitDuration;

    private bool usingPower;
    private float currentDuration;
    private float maxDuration;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }

        // Initialize the time slider
        currentDuration = haltDuration;
        maxDuration = haltDuration;
        timeSliderFill.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingPower)
        {
            currentDuration -= Time.deltaTime;
            timeSlider.value = currentDuration / maxDuration;
        }
    }

    // Method to set the time scale
    public void SetPlayerTimeScale(float timeScale)
    {
        playerTimeScale = timeScale;
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

    // Method to get the current duration of the power
    public int GetPower()
    {
        return powerType;
    }

    // Method to change the power type
    public void SwitchPower(int power)
    {
        powerType = power;

        // Set the current duration and max duration based on the power type
        switch (powerType)
        {
            case 0: // Halt
                currentDuration = haltDuration;
                maxDuration = haltDuration;
                timeSliderFill.color = Color.red; // Change color to red for Halt
                break;
            case 1: // Rush
                currentDuration = rushDuration;
                maxDuration = rushDuration;
                timeSliderFill.color = Color.blue; // Change color to blue for Rush
                break;
            case 2: // Omit
                currentDuration = omitDuration;
                maxDuration = omitDuration;
                timeSliderFill.color = Color.green; // Change color to green for Omit
                break;
            default:
                Debug.LogError("Invalid power type");
                break;
        }

        // Update the slider value
        timeSlider.value = currentDuration / maxDuration;
    }

    // Method to get start and stop counting down the power duration
    public void StartCountdown(bool isUsing)
    {
        usingPower = isUsing;
    }

    // Method to get the current duration of the power
    public float GetCurrentDuration()
    {
        return currentDuration;
    }

    // Method to get the volume profile based on the power type
    public VolumeProfile GetVolumeProfile()
    {
        return volumeProfiles[powerType];
    }
}
