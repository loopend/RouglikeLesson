using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.GameCore.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private float _explosionDelay = 3f;
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private AudioClip _explosionSound;
        [SerializeField] [Range(0f, 1f)] private float _explosionSoundVolume = 1f;

        private ParticleBombSpawner _particleBombSpawner;
        private Coroutine _explosionRoutine;

        private void OnEnable()
        {
            _explosionRoutine = StartCoroutine(ExplosionRoutine());
        }

        private void OnDisable()
        {
            if (_explosionRoutine != null)
            {
                StopCoroutine(_explosionRoutine);
                _explosionRoutine = null;
            }
        }

        private IEnumerator ExplosionRoutine()
        {
            yield return new WaitForSeconds(_explosionDelay);
            Explode();
        }

        private void Explode()
        {
            PlayExplosionSound();
            _particleBombSpawner.Spawn(transform.position);
            KillEnemiesInRadius();
            gameObject.SetActive(false);
        }

        private void PlayExplosionSound()
        {
            if (_explosionSound != null)
                AudioSource.PlayClipAtPoint(_explosionSound, transform.position, _explosionSoundVolume);
        }

        private void KillEnemiesInRadius()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
            var damagedEnemies = new HashSet<EnemyHealth>();

            for (int i = 0; i < hits.Length; i++)
            {
                EnemyHealth enemy = hits[i].GetComponentInParent<EnemyHealth>();
                if (enemy == null || !enemy.gameObject.activeInHierarchy)
                    continue;

                if (!damagedEnemies.Add(enemy))
                    continue;

                enemy.TakeDamage(enemy.CurrentHealth);
            }
        }

        [Inject]
        private void Construct(ParticleBombSpawner particleBombSpawner)
        {
            _particleBombSpawner = particleBombSpawner;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
#endif
    }
}
