using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to control the rotation of a GameObject.
/// Allows rotation along the X, Y, and Z axes based on specified values.
/// </summary>
public class RotateSystem : MonoBehaviour
{
    /// <summary>
    /// The GameObject to rotate.
    /// </summary>
    public GameObject objectToRotate;

    /// <summary>
    /// Rotation increment on the X-axis.
    /// </summary>
    public float rotationX = 0;

    /// <summary>
    /// Rotation increment on the Y-axis.
    /// </summary>
    public float rotationY = 0;

    /// <summary>
    /// Rotation increment on the Z-axis.
    /// </summary>
    public float rotationZ = 0;

    private Quaternion targetRotation;

    /// <summary>
    /// Called once per frame to update the rotation of the object.
    /// </summary>
    private void Update()
    {
        CheckRotation();
    }

    /// <summary>
    /// Rotates the specified object based on the provided X, Y, and Z rotation values.
    /// </summary>
    void CheckRotation()
    {
        // Apply rotation based on the provided increments for X, Y, and Z
        objectToRotate.transform.Rotate(rotationX, rotationY, rotationZ);

        // Optional: Alternate method (commented out) to smoothly update the rotation to a target value
        /*
        targetRotation = Quaternion.Euler(objectToRotate.transform.eulerAngles.x + 0.1F, objectToRotate.transform.eulerAngles.y + 0.1F, objectToRotate.transform.eulerAngles.z);
        objectToRotate.transform.rotation = targetRotation;
        */
    }
}