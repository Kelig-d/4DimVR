using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Represents a mapping between an animation parameter and an input action.
/// </summary>
[System.Serializable]
public class AnimationInput
{
    /// <summary>
    /// The name of the animation parameter to control.
    /// </summary>
    public string animationPropertyName;

    /// <summary>
    /// The input action that controls the animation parameter.
    /// </summary>
    public InputActionProperty action;
}

/// <summary>
/// Updates animator parameters based on input actions.
/// </summary>
public class AnimateOnInput : MonoBehaviour
{
    /// <summary>
    /// A list of animation inputs defining the mapping between input actions and animator parameters.
    /// </summary>
    public List<AnimationInput> animationInputs;

    /// <summary>
    /// The animator to control with the input actions.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Called once per frame to update animator parameters based on the current values of input actions.
    /// </summary>
    void Update()
    {
        // Iterate through each animation input mapping
        foreach (var item in animationInputs)
        {
            // Read the current value of the input action
            float actionValue = item.action.action.ReadValue<float>();

            // Set the corresponding animation parameter to the action's value
            animator.SetFloat(item.animationPropertyName, actionValue);
        }
    }
}