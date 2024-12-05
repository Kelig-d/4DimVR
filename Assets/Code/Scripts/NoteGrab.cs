using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This script allows interaction with an object using XR (Virtual Reality) grab functionality.
/// When the object is grabbed, a TextMeshPro text and a Canvas are displayed. When the object is released, 
/// the text and canvas are hidden.
/// </summary>
public class NoteGrab : MonoBehaviour
{
    public TextMeshPro txt; ///< Reference to the TextMeshPro component to display text.
    public Canvas canvas; ///< Reference to the Canvas that holds the UI elements.
    private XRGrabInteractable grabInteractable; ///< Reference to the XRGrabInteractable component used for grabbing interactions.

    /// <summary>
    /// Initializes the script by setting up event listeners for the grab and release actions.
    /// </summary>
    void Start()
    {
        // Get the XRGrabInteractable component attached to this GameObject
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Subscribe to the grab and release events
            grabInteractable.selectEntered.AddListener(OnGrab);
            grabInteractable.selectExited.AddListener(OnRelease);
        }
        else
        {
            // Log an error if the XRGrabInteractable component is missing
            Debug.LogError("XRGrabInteractable missing on this object!");
        }
    }

    /// <summary>
    /// Method called when the object is grabbed.
    /// Enables the display of the text and canvas.
    /// </summary>
    /// <param name="args">Event arguments containing details about the grab action.</param>
    private void OnGrab(SelectEnterEventArgs args)
    {
        // Log the event of grabbing the object
        Debug.Log($"{gameObject.name} has been grabbed!");
        
        // Enable the TextMeshPro and Canvas
        txt.gameObject.SetActive(true);
        canvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// Method called when the object is released.
    /// Disables the display of the text and canvas.
    /// </summary>
    /// <param name="args">Event arguments containing details about the release action.</param>
    private void OnRelease(SelectExitEventArgs args)
    {
        // Log the event of releasing the object
        Debug.Log($"{gameObject.name} has been released!");

        // Disable the TextMeshPro and Canvas
        txt.gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates every frame (currently not used in this script).
    /// </summary>
    void Update()
    {
        // Logic for updating the object can be added here if needed
    }
}
