using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class PorteTeleportation : MonoBehaviour
{
    /// <summary>
    /// The other portal object that this portal will teleport to.
    /// </summary>
    public GameObject otherPortal;

    /// <summary>
    /// Called when another object collides with the portal.
    /// Transforms the position and scale of the colliding object and teleports it to the other portal.
    /// Additionally, it stops time briefly by setting the time scale to 0.
    /// </summary>
    /// <param name="collision">The collision details.</param>
    void OnCollisionEnter(Collision collision)
    {
        // Check if the parent scale of the portals is different
        if (otherPortal.transform.parent.transform.localScale != transform.parent.transform.localScale)
        {
            // Adjust the colliding object's scale based on the scale difference between portals
            collision.gameObject.transform.parent.transform.localScale *= otherPortal.transform.parent.transform.localScale.x / transform.parent.transform.localScale.x;

            // Pause the game briefly by setting time scale to 0
            Time.timeScale = 0;
        }

        // Teleport the colliding object to the other portal's position, adjusting the position based on the other portal's rotation
        collision.gameObject.transform.position = new Vector3(
            otherPortal.transform.position.x + Mathf.Clamp(otherPortal.transform.parent.rotation.y, 0, 1.5f),
            otherPortal.transform.position.y - 1.5f,
            otherPortal.transform.position.z + Mathf.Clamp(otherPortal.transform.parent.rotation.y, 1.5f, 0));

        // Adjust the colliding object's rotation to match the other portal's rotation
        collision.gameObject.transform.rotation = new Quaternion(0, otherPortal.transform.rotation.y, 0, otherPortal.transform.rotation.w);
    }
}