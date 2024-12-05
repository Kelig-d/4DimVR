using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// This class calculates the total light intensity at a given item's position based on the distance to all light sources in the scene.
/// </summary>
public class CheckLight : MonoBehaviour
{
    /// <summary>
    /// Reference to the item whose position will be checked for light intensity.
    /// </summary>
    public GameObject item;

    /// <summary>
    /// This method is called when the script is first initialized.
    /// </summary>
    void Start()
    {
        // Initialization code can go here if needed.
    }

    /// <summary>
    /// This method is called once per frame and calculates the total light intensity from all light sources within range of the item.
    /// </summary>
    void Update()
    {
        // Find all the Light objects in the scene.
        Light[] lights = FindObjectsOfType<Light>();

        // Variable to hold the total intensity calculated from all lights.
        float totalIntensity = 0f;

        // Loop through all lights to calculate their contribution to the total intensity.
        foreach (Light light in lights)
        {
            // Calculate the distance between the item and the light source.
            float distance = Vector3.Distance(item.transform.position, light.transform.position);

            // Check if the light source's range includes the item.
            if (distance <= light.range)
            {
                // Calculate the intensity contribution of this light, considering the inverse square law.
                totalIntensity += light.intensity / (distance * distance);
            }
        }

        // Optionally, do something with totalIntensity, such as updating a visual effect or property.
    }
}