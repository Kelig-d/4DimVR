using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls hand animations based on input actions for pinch and grip gestures in VR or XR environments.
/// </summary>
public class HandAnimatedController : MonoBehaviour
{
    /// <summary>
    /// The input action property for the pinch gesture animation.
    /// </summary>
    public InputActionProperty pinchAnimationAction;

    /// <summary>
    /// The input action property for the grip gesture animation.
    /// </summary>
    public InputActionProperty gripAnimationAction;

    /// <summary>
    /// The Animator component that handles hand animations.
    /// </summary>
    public Animator handAnimator;

    /// <summary>
    /// Updates the hand's animation state based on input values for pinch and grip actions.
    /// </summary>
    void Update()
    {
        // Pinch gesture input value.
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        // Set the "Trigger" parameter of the hand animator based on pinch value.
        handAnimator.SetFloat("Trigger", triggerValue);

        // Grip gesture input value.
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        // Set the "Grip" parameter of the hand animator based on grip value.
        handAnimator.SetFloat("Grip", gripValue);
    }
}