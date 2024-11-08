using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float _maxHealth;
    private float _currentHealth;
    
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private float _fillSpeed;
    [SerializeField] private Gradient _colorGradient;   

    public void UpdateHealth(float health)
    {
        Debug.Log(health);
        if(_maxHealth == 0) _maxHealth = health;
        _currentHealth = Mathf.Clamp(health, 0f, _maxHealth);

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float targetFillAmount = _currentHealth / _maxHealth;
        //_healthBarFill.fillAmount = targetFillAmount;
        //_healthBarFill.DOFillAmount(targetFillAmount, _fillSpeed);
        _healthBarFill.color = _colorGradient.Evaluate(targetFillAmount);
    }
}
