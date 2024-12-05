using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is responsible for destroying the light when the collider enters a trigger. It scales down the light to zero when the trigger event occurs.
/// </summary>
public class DestroyLightOnContact : MonoBehaviour
{
    /// <summary>
    /// Reference to the light object that will be scaled down when the trigger event occurs.
    /// </summary>
    public Light light;

    /// <summary>
    /// Called when another collider enters the trigger collider attached to this GameObject.
    /// This method scales the light's transform to zero, effectively "destroying" it visually.
    /// </summary>
    /// <param name="other">The collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Scale the light's transform to zero, effectively "destroying" it.
        this.light.transform.localScale = Vector3.zero;
    }
}