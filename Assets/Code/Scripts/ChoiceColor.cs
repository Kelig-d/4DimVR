using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles the player's interaction with the Zimmablue artifact to select and set a color.
/// </summary>
public class ChoiceColor : MonoBehaviour
{
    /// <summary>
    /// The projectile represented as a colored line.
    /// </summary>
    public LineRenderer projectile;

    /// <summary>
    /// The color selected by the player.
    /// </summary>
    public Color color;

    /// <summary>
    /// Called when another collider enters the color button's collision area.
    /// Triggers the process of applying the selected color.
    /// </summary>
    /// <param name="other">The collider that entered the trigger area.</param>
    public void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");
        Pressed();
    }

    /// <summary>
    /// Applies the selected color to the projectile's line renderer.
    /// </summary>
    public void Pressed()
    { 
        LineRenderer spawProjectile = projectile;
        SetSingleColor2(spawProjectile, color);
    }

    /// <summary>
    /// Sets the start and end colors of a line renderer to the specified color.
    /// </summary>
    /// <param name="lineRenderer">The line renderer to modify.</param>
    /// <param name="newcolor">The color to apply to the line renderer.</param>
    public void SetSingleColor2(LineRenderer lineRenderer, Color newcolor) {
        lineRenderer.startColor = newcolor;
        lineRenderer.endColor = newcolor;
    }
}