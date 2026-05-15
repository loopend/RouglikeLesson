using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : ObjectHealth
{
    public Action OnHealthChanged;

    [SerializeField] private float _regenerationValue = 1f;
    [SerializeField] private float _regenerationDelay = 5f;

    private WaitForSeconds _regenInterval;
    private WaitForSeconds _damageOverTimeInterval = new WaitForSeconds(1f);

    private void Start()
    {
        _regenInterval = new WaitForSeconds(_regenerationDelay);
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
            Debug.Log("Игрок умер");
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
            elapsed += 1f;
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
    // Тут предыдущий код. Сохранил на всякий
    //public Action OnHealthChanged;

    //private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
    //private float _regenerationValue = 1f;

    //private void Start() => StartCoroutine(routine: Regeneration());

    //public void Heal(float value)
    //{
    //    TakeHeal(value);
    //    OnHealthChanged?.Invoke();
    //}

    //public override void TakeDamage(float damage)
    //{
    //    base.TakeDamage(damage);
    //    OnHealthChanged?.Invoke();
    //    if (CurrentHealth <= 0)
    //        Debug.Log("Игрок умер");
    //}
    //public void ApplyDamageOverTime(float damagePerTick, float duratin)
    //{
    //    StartCoroutine(ApplyDamageOverTime(damagePerTickmagePerTick, duratin)
    //}
    //private IEnumerator DamageOverTime(float damagePerTick, float duration)
    //{
    //    float elapsed = 0f;
    //    while (elapsed < duration)
    //    {
    //        TakeDamage(damagePerTick);
    //        elapsed += 1f;
    //        yield return _damageOverTimeInterval;
    //    }

    //}
    //private IEnumerator Regeneration()
    //{
    //    while (true)
    //    {
    //        TakeHeal(_regenerationValue);
    //        OnHealthChanged?.Invoke();
    //        yield return _regenerationInterval;
    //    }
    //}
}
