using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Manages XR input interaction with an object, adding a dimension to the artifact when it is grabbed.
/// </summary>
public class grabNewFrag : MonoBehaviour
{
    /// <summary>
    /// Enumeration representing the different dimensions that the object can have.
    /// </summary>
    public enum Name { Berceau, ZimmaBlue, Mi7, ChronoPhagos };

    /// <summary>
    /// The current dimension of the object, determined by the <see cref="Name"/> enumeration.
    /// </summary>
    public Name Dimension;

    /// <summary>
    /// Reference to the <see cref="XRGrabInteractable"/> component to manage the grab interaction.
    /// </summary>
    private XRGrabInteractable grabInteractable;

    /// <summary>
    /// Reference to the artifact script to add a dimension to the object.
    /// </summary>
    public ArtefactdeTp script;

    /// <summary>
    /// Initializes references and subscribes to the object's grab events.
    /// </summary>
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Subscribe to the grab events
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable is missing on this object!");
        }
    }

    /// <summary>
    /// Method called when the object is grabbed. Adds a dimension to the artifact based on the <see cref="Dimension"/> enumeration.
    /// </summary>
    /// <param name="args">The event arguments for the grab event.</param>
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} has been grabbed!");
        
        // Add the dimension to the artifact
        script.AddDimension(Dimension.ToString());
    }

    /// <summary>
    /// Method called every frame. Currently unused.
    /// </summary>
    void Update()
    {
        // Empty update, can be used for future interactions or logic.
    }
}
