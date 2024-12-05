using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the health bar UI, updating its fill amount and color based on the current health value.
/// </summary>
public class HealthBar : MonoBehaviour
{
    /// <summary>
    /// The maximum health value that the health bar can represent.
    /// </summary>
    private float _maxHealth;

    /// <summary>
    /// The current health value of the player or entity, used to determine the health bar's fill.
    /// </summary>
    private float _currentHealth;

    /// <summary>
    /// The Image component that displays the health bar fill.
    /// </summary>
    [SerializeField] private Image _healthBarFill;

    /// <summary>
    /// The speed at which the health bar fill updates.
    /// </summary>
    [SerializeField] private float _fillSpeed;

    /// <summary>
    /// A gradient used to change the color of the health bar as it depletes.
    /// </summary>
    [SerializeField] private Gradient _colorGradient;   

    /// <summary>
    /// Updates the health value and adjusts the health bar accordingly.
    /// </summary>
    /// <param name="health">The new health value to set, which can be less than or equal to the max health.</param>
    public void UpdateHealth(float health)
    {
        // Set the max health value if it hasn't been set yet
        if (_maxHealth == 0) _maxHealth = health;

        // Clamp the current health to ensure it's between 0 and the max health
        _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);

        // Update the health bar display
        UpdateHealthBar();
    }

    /// <summary>
    /// Updates the health bar's fill amount and color based on the current health value.
    /// </summary>
    private void UpdateHealthBar()
    {
        // Calculate the target fill amount as a fraction of current health over max health
        float targetFillAmount = _currentHealth / _maxHealth;

        // Animate the fill amount of the health bar with a smooth transition
        //_healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);

        // Set the color of the health bar fill based on the target fill amount
        _healthBarFill.color = _colorGradient.Evaluate(targetFillAmount);
    }
}
