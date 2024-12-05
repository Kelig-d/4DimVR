using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script creates a light effect when a collision occurs. It instantiates a 'BouleAFacette' object at the item's position and adjusts the light's intensity based on the impact speed.
/// </summary>
public class CreatelightOnContact : MonoBehaviour
{
    /// <summary>
    /// Reference to the item that will be used as a reference position for the created light.
    /// </summary>
    public GameObject item;

    /// <summary>
    /// Reference to the prefab of the 'BouleAFacette' object, which will be instantiated on collision.
    /// </summary>
    public GameObject BouleAFacette;

    /// <summary>
    /// Reference to the Mi7Light component, which will be used to update the light's intensity based on the collision speed.
    /// </summary>
    private Mi7Light mi7Light;

    /// <summary>
    /// This method is called when a collision is detected with another object.
    /// It instantiates the 'BouleAFacette' object at the item's position and updates the light's intensity based on the collision's impact speed.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    void OnCollisionEnter(Collision collision)
    {
        // Get the relative velocity at the moment of impact.
        Vector3 relativeVelocity = collision.relativeVelocity;

        // Calculate the magnitude (scalar speed) of the impact.
        float impactSpeed = relativeVelocity.magnitude;

        // Set the position of the 'BouleAFacette' to be at the item's position.
        BouleAFacette.transform.position = item.transform.position;

        // Instantiate the 'BouleAFacette' object at the item's position.
        GameObject init = Instantiate(BouleAFacette);

        // Get the Mi7Light component from the instantiated object.
        mi7Light = init.GetComponent<Mi7Light>();

        // Update the light's intensity based on the impact speed.
        mi7Light.updateVitesse(impactSpeed);
    }
}
