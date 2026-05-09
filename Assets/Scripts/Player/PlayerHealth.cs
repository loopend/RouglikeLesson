using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : ObjectHealth
{
    public Action OnHealthChanged;
    private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
    private float _regenerationValue = 1f;

    private void Start() => StartCoroutine(routine: Regeneration());
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
    private IEnumerator Regeneration()
    {
        while (true)
        {
            TakeHeal(_regenerationValue);
            OnHealthChanged?.Invoke();
            yield return _regenerationInterval;
        }
    }
}
