using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Manages the interaction of the object with an XR input system, allowing the dimension of the artifact to be changed and resetting its position when released.
/// </summary>
public class grabFrag : MonoBehaviour
{
    /// <summary>
    /// Enumeration representing the different dimensions that the object can have.
    /// </summary>
    public enum Name { Berceau, ZimmaBlue, Mi7, ChronoPhagos, BerceauMI7TEST, Mi7MI7TEST };

    /// <summary>
    /// The current dimension of the object, determined by the <see cref="Name"/> enumeration.
    /// </summary>
    public Name Dimension;

    /// <summary>
    /// The anchor to which the object will be reset when released.
    /// </summary>
    public GameObject anchor;

    /// <summary>
    /// Reference to the <see cref="XRGrabInteractable"/> component for managing the grab interaction.
    /// </summary>
    private XRGrabInteractable grabInteractable;

    /// <summary>
    /// Reference to the artifact script for changing the object's dimension.
    /// </summary>
    public ArtefactdeTp script;

    /// <summary>
    /// Initializes references and subscribes to the grab and release events.
    /// </summary>
    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Subscribe to the grab and release events
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            Debug.LogError("XRGrabInteractable is missing on this object!");
        }
    }

    /// <summary>
    /// Method called when the object is grabbed. Changes the dimension of the artifact based on the <see cref="Dimension"/> enumeration.
    /// </summary>
    /// <param name="args">The event arguments for the grab event.</param>
    private void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log($"{gameObject.name} has been grabbed!");
        
        // Change the dimension of the artifact
        script.ChangeDimension(Dimension.ToString());
    }

    /// <summary>
    /// Method called when the object is released. Resets the object's position to the anchor's position.
    /// </summary>
    /// <param name="args">The event arguments for the release event.</param>
    private void OnRelease(SelectExitEventArgs args)
    { 
        // Reset the object's position to the anchor position
        this.transform.position = anchor.transform.position;
    }

    /// <summary>
    /// Method called every frame. Currently unused.
    /// </summary>
    void Update()
    {
        // Empty update, can be used for future interactions or logic.
    }
}
