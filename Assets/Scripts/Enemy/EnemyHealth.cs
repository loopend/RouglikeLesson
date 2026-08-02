using Assets.Scripts.GameCore.ExperienceSystem;
using Assets.Scripts.GameCore.Loot;
using Assets.Scripts.GameCore.UI;
using System.Collections;


using UnityEngine;

using Zenject;

public class EnemyHealth : ObjectHealth
{
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] [Range(0f, 1f)] private float _damageSoundVolume = 0.35f;
    private WaitForSeconds _tick = new WaitForSeconds(1f);
    private DamageTextSpawner _damageTextSpawner;
    private ExperienceSpawner _experienceSpawner;
    private ParticleDamageSpawner _particleDamageSpawner;
    private HeartSpawner _heartSpawner;
    private BombSpawner _bombSpawner;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _damageTextSpawner.Activate(transform, (int)damage);
        PlayDamageSound();
        if (CurrentHealth <= 0)
            HandleDeath();
    }

    protected virtual void HandleDeath()
    {
        SpawnDeathParticles();
        RegisterKillForHeartDrop();
        TryDropBomb();
        gameObject.SetActive(false);
        ChanceToDropExperience();
    }

    protected void TryDropBomb()
    {
        _bombSpawner.TrySpawn(transform.position);
    }

    protected void RegisterKillForHeartDrop()
    {
        _heartSpawner.RegisterKill(transform.position);
    }

    protected void SpawnDeathParticles()
    {
        _particleDamageSpawner.Spawn(transform.position);
    }

    private void PlayDamageSound()
    {
        if (_damageSound != null)
            AudioSource.PlayClipAtPoint(_damageSound, transform.position, _damageSoundVolume);
    }
    public void Burn(float damage) => StartCoroutine(routine:StartBurn(damage));

    private void ChanceToDropExperience()
    {
        if (Random.Range(0f, 100f) <= 33f)
        {
            _experienceSpawner.Spawn(transform.position);
        }

    }

    private IEnumerator StartBurn(float damage)
    {
        if (gameObject.activeSelf == false)
        {
            yield break;
            //float tickDamage = damage / 3f;
            //if (tickDamage < 1f)
            //    tickDamage = 1f;
            //float roundDamage = Mathf.Round(tickDamage);
            //for (int i = 0; i < 5; i++)
            //{
            //    TakeDamage(roundDamage);
            //    yield return _tick;
            //}
        }
    }
    [Inject]
    private void Construct(
        DamageTextSpawner damageTextSpawner,
        ExperienceSpawner experienceSpawner,
        ParticleDamageSpawner particleDamageSpawner,
        HeartSpawner heartSpawner,
        BombSpawner bombSpawner)
    {
        _experienceSpawner = experienceSpawner;
        _damageTextSpawner = damageTextSpawner;
        _particleDamageSpawner = particleDamageSpawner;
        _heartSpawner = heartSpawner;
        _bombSpawner = bombSpawner;
    }
}
