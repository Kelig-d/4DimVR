using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Custom XRGrabInteractable class to apply position and rotation offsets 
/// when the object is grabbed or released.
/// </summary>
public class GrabbableOffset : XRGrabInteractable
{
    /// <summary>
    /// Position offset applied when the object is grabbed.
    /// </summary>
    public Vector3 positionOffset;

    /// <summary>
    /// Rotation offset applied when the object is grabbed, in degrees (X, Y, Z).
    /// </summary>
    public Vector3 rotationOffset;

    /// <summary>
    /// Called when the grab interaction begins.
    /// Applies the position and rotation offsets to the object.
    /// </summary>
    /// <param name="args">Event arguments related to the grab interaction.</param>
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Call the base logic for the event
        base.OnSelectEntered(args);

        // Apply the position offset
        transform.localPosition += positionOffset;

        // Apply the rotation offset
        transform.localEulerAngles += rotationOffset;
    }

    /// <summary>
    /// Called when the grab interaction ends.
    /// Resets the object's position and rotation by removing the offsets.
    /// </summary>
    /// <param name="args">Event arguments related to the release interaction.</param>
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        // Call the base logic for the event
        base.OnSelectExited(args);

        // Reset the position
        transform.localPosition -= positionOffset;

        // Reset the rotation
        transform.localEulerAngles -= rotationOffset;
    }
}