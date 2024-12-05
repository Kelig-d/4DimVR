using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// This class handles the interaction with a button in a VR environment. It changes the button's color when idle, plays a sound when grabbed, and handles activation states.
/// </summary>
public class MessagereiBtn : MonoBehaviour
{
    /// <summary>
    /// Reference to the XRGrabInteractable component for handling VR grab interactions.
    /// </summary>
    private XRGrabInteractable grabInteractable;

    /// <summary>
    /// The GameObject representing the button to be interacted with.
    /// </summary>
    public GameObject Touche;

    /// <summary>
    /// The base color of the button when idle.
    /// </summary>
    public Color colorBase;

    /// <summary>
    /// The color the button changes to when it is notified.
    /// </summary>
    public Color colorNotif;

    /// <summary>
    /// The audio clip that plays when the button is grabbed.
    /// </summary>
    public AudioClip song;

    /// <summary>
    /// The AudioSource component that plays the audio clip.
    /// </summary>
    public AudioSource tel;

    /// <summary>
    /// Flag indicating whether the button is active or not.
    /// </summary>
    bool Active = true;

    /// <summary>
    /// Counter for time tracking for the button color change.
    /// </summary>
    int time = 0;

    /// <summary>
    /// Initializes the XR interaction and subscribes to the grab event if the button is active.
    /// </summary>
    void Start()
    {   
        // Retrieve the XRGrabInteractable component
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (grabInteractable != null && Active)
        {
            // Subscribe to the grab event
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable missing on this object!");
        }
    }

    /// <summary>
    /// Method called when the object is grabbed. It changes the button's color and plays a sound.
    /// </summary>
    /// <param name="args">The event arguments for the grab interaction.</param>
    private void OnGrab(SelectEnterEventArgs args)
    {
        // Change the button color to the base color
        Touche.GetComponent<Renderer>().material.color = colorBase;

        // Deactivate the button to prevent further interactions
        Active = false;

        // Play the assigned audio clip
        tel.clip = song;
        tel.Play();
    }

    /// <summary>
    /// Updates the button's color periodically when it's active.
    /// </summary>
    void Update()
    {
        if (Active)
        {
            // Increment the time tracker
            time += 1;

            // Change the button color every 80 frames
            if (time % 80 == 0)
            {
                if (Touche.GetComponent<Renderer>().material.color == colorBase)
                {
                    Touche.GetComponent<Renderer>().material.color = colorNotif;
                }
                else
                {
                    Touche.GetComponent<Renderer>().material.color = colorBase;
                }
            }
        }
    }
}
