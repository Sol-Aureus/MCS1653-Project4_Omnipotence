using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float playerHaltTime;
    [SerializeField] private float playerRushTime;
    [SerializeField] private bool isHalt;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isHalt)
            {
                TimeManager.instance.SetPlayerTimeScale(playerHaltTime);
                TimeManager.instance.StopTime(true);
            }
            else
            {
                TimeManager.instance.SetPlayerTimeScale(playerRushTime);
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            TimeManager.instance.SetPlayerTimeScale(1);
            TimeManager.instance.StopTime(false);
        }
    }
}
