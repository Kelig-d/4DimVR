using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Handles the interaction with a ball, applying an upward force when the object is activated.
/// </summary>
public class BallInteraction : MonoBehaviour
{
    /// <summary>
    /// The rigidbody of the ball that will receive the upward force.
    /// </summary>
    public Rigidbody bounceBall;

    /// <summary>
    /// The amount of upward force to apply to the ball.
    /// </summary>
    public float upwardForce = 500f;

    /// <summary>
    /// Initializes the component and subscribes to the activation event.
    /// </summary>
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

        // Add a listener to the activation event to trigger the interaction
        grabbable.activated.AddListener(Interaction);
    }

    /// <summary>
    /// Called every frame. Currently not used.
    /// </summary>
    void Update()
    {
    }

    /// <summary>
    /// Applies an upward force to the specified ball when the object is activated.
    /// </summary>
    /// <param name="args">Event arguments for the activation.</param>
    void Interaction(ActivateEventArgs args)
    {
        if (bounceBall != null)
        {
            // Apply an upward force to the target object
            bounceBall.AddForce(Vector3.up * upwardForce);
        }
        else
        {
            Debug.LogWarning("No target object assigned!");
        }
    }
}