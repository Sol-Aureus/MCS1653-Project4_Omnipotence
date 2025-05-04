using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerPower : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Volume playerVolume;
    [SerializeField] private VolumeProfile defaultProfile;

    [Header("Attributes")]
    [SerializeField] private float playerHaltTime;
    [SerializeField] private float playerRushTime;
    [SerializeField] private int powerType;

    // Update is called once per frame
    void Update()
    {
        // Activates the player's power when the player presses the jump button
        if (Input.GetButtonDown("Jump"))
        {
            // Checks if there is enough duration left
            if (TimeManager.instance.GetCurrentDuration() > 0)
            {
                // Checks the player's power type
                powerType = TimeManager.instance.GetPower();

                // Sets the player's time scale based on the power type
                switch (powerType)
                {
                    case 0: // Halt
                        TimeManager.instance.SetPlayerTimeScale(playerHaltTime);
                        TimeManager.instance.StopTime(true);
                        playerVolume.profile = TimeManager.instance.GetVolumeProfile();
                        break;
                    case 1: // Rush
                        TimeManager.instance.SetPlayerTimeScale(playerRushTime);
                        playerVolume.profile = TimeManager.instance.GetVolumeProfile();
                        break;
                    case 2: // Omit
                        playerCollider.enabled = false;
                        playerVolume.profile = TimeManager.instance.GetVolumeProfile();
                        break;
                }

                TimeManager.instance.StartCountdown(true);
            }
        }

        // Reset the time scale and collider when the player releases the jump button
        if (Input.GetButtonUp("Jump") || TimeManager.instance.GetCurrentDuration() <= 0)
        {
            TimeManager.instance.SetPlayerTimeScale(1);
            TimeManager.instance.StopTime(false);
            playerCollider.enabled = true;
            TimeManager.instance.StartCountdown(false);
            playerVolume.profile = defaultProfile;
        }
    }
}
