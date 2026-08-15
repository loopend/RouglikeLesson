using Assets.Scripts.GameCore.Pause;
using Assets.Scripts.Menu.Shop;
using Assets.Scripts.Player;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerHealth : ObjectHealth
{
    public Action OnHealthChanged;

    [SerializeField] private float _regenerationValue = 1f;
    [SerializeField] private float _regenerationDelay = 5f;
    [SerializeField] private float _DOTDelay = 1f;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _endGameWindow;
    private WaitForSeconds _regenInterval;
    private WaitForSeconds _damageOverTimeInterval;
    private WaitForSeconds _interval = new WaitForSeconds(1f);
    private GamePause _gamePause;
    private UpgradeLoader _upgradeLoader;

    private void Start()
    {
        _regenInterval = new WaitForSeconds(_regenerationDelay);
        _damageOverTimeInterval = new WaitForSeconds(_DOTDelay);
        StartCoroutine(Regeneration());
        _maxHealth = _upgradeLoader.HealthCurrentLevel.Value;
        _currentHealth = _maxHealth;
        _regenerationValue = _upgradeLoader.RegenCurrentLevel.Value;
    }

    public void Heal(float value)
    {
        TakeHeal(value);
        OnHealthChanged?.Invoke();
    }

    public void FullHeal()
    {
        float missingHealth = MaxHealth - CurrentHealth;
        if (missingHealth > 0)
            TakeHeal(missingHealth);

        OnHealthChanged?.Invoke();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        OnHealthChanged?.Invoke();
        if (CurrentHealth <= 0)
            StartCoroutine(routine: PlayerDied());
    }
    
    public void UpgradeHeath()
    {
        _currentHealth += 10;
        _maxHealth += 10;
    }
    public void UpgradeRegeneration()
    {
        _regenerationValue++;
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
    private IEnumerator PlayerDied()
    {
        _gamePause.SetPause(true);
        _animator.SetTrigger(name:"Die");
        yield return _interval;
        _endGameWindow.SetActive(true);
    }



    [Inject]
    private void Construct(GamePause gamePause, UpgradeLoader upgradeLoader)
    {
        _gamePause = gamePause;
        _upgradeLoader = upgradeLoader;
    }
}
