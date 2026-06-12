using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : ObjectHealth
{
    public Action OnHealthChanged;

    [SerializeField] private float _regenerationValue = 1f;
    [SerializeField] private float _regenerationDelay = 5f;
    [SerializeField] private float _DOTDelay = 1f;

    private WaitForSeconds _regenInterval;
    private WaitForSeconds _damageOverTimeInterval;

    private void Start()
    {
        _regenInterval = new WaitForSeconds(_regenerationDelay);
        _damageOverTimeInterval = new WaitForSeconds(_DOTDelay);
        StartCoroutine(Regeneration());
    }

    public void Heal(float value)
    {
        TakeHeal(value);
        OnHealthChanged?.Invoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged?.Invoke();
        if (CurrentHealth <= 0)
            Debug.Log("Игрок умер!");
    }


    public void ApplyDamageOverTime(float damageTick, float duration)
    {
        StartCoroutine(DamageOverTime(damageTick, duration));
    }

    private IEnumerator DamageOverTime(float damageTick, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            TakeDamage(damageTick);
            elapsed += _DOTDelay;
            yield return _damageOverTimeInterval;
        }
    }

    private IEnumerator Regeneration()
    {
        while (true)
        {
            if (CurrentHealth < MaxHealth)
            {
                Heal(_regenerationValue);
            }
            yield return _regenInterval;
        }
    }
}
