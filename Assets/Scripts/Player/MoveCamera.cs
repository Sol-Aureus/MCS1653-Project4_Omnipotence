using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform cameraPos;

    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        transform.position = cameraPos.position;
    }
}
