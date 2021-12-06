using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [Header("Velocity Settings")]
    [Range(0f,1f)]
    public float fowardVelocity = 1;
    [Range(0f, 1f)]
    public float horizontalVelocity = 1;

    void Update()
    {
        KeyboardMovement();
    }

    private void KeyboardMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        transform.localPosition += transform.forward * vertical * fowardVelocity;
        transform.localPosition += transform.right * horizontal * horizontalVelocity;
    }
}
