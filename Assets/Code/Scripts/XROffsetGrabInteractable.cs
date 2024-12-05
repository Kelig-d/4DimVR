using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    /// <summary>
    /// The initial local position of the attach point when the object is not being grabbed.
    /// </summary>
    private Vector3 initialAttachLocalPos;

    /// <summary>
    /// The initial local rotation of the attach point when the object is not being grabbed.
    /// </summary>
    private Quaternion initialAttachLocalRot;

    // Start is called before the first frame update
    /// <summary>
    /// Initializes the attach transform and stores its initial position and rotation.
    /// </summary>
    void Start()
    {
        // Create an attach point if one doesn't exist
        if (!attachTransform)
        {
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform, false);
            attachTransform = grab.transform;
        }

        // Store the initial local position and rotation of the attach transform
        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    /// <summary>
    /// Called when the object is selected by an interactor.
    /// Adjusts the attach transform position and rotation based on the type of interactor.
    /// </summary>
    /// <param name="interactor">The interactor that selects the object.</param>
    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        if (interactor.interactorObject is XRDirectInteractor)
        {
            // If the interactor is of type XRDirectInteractor, adjust the attach point position and rotation
            attachTransform.position = GetAttachTransform(interactor.interactorObject).position;
            attachTransform.rotation = GetAttachTransform(interactor.interactorObject).rotation;
        }
        else
        {
            // For other types of interactors, revert to the initial local position and rotation
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }

        // Call the base class implementation of OnSelectEntered
        base.OnSelectEntered(interactor);
    }
}
