using Assets.Scripts.GameCore.Health;
using System;


using UnityEngine;

public abstract class ObjectHealth : MonoBehaviour, IDamageable
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
     
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    private void OnEnable() => _currentHealth = _maxHealth;

    public virtual void TakeDamage(float damage)
    {
        if(damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(damage));
        _currentHealth -= damage;
    }

    public void TakeHeal(float value)
    {
        if (value <= 0)
            throw new ArgumentOutOfRangeException(nameof(value));
        _currentHealth += value;
        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }




}
