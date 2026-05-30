using Assets.Scripts.GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        private const float SlowSpeedMultiplier = 0.5f;
        private const int SlowMinLevel = 5;

        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _collider;
        private List<EnemyHealth> _enemyInZone = new List<EnemyHealth>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _auraCoroutine;
        private float _range;


        protected override void Start()
        {
            SetStats(0);
            Activate();
            LevelUp();

            LevelUp();

            LevelUp();

            LevelUp();

            LevelUp();

            LevelUp();

            LevelUp();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                if (!_enemyInZone.Contains(enemy))
                    _enemyInZone.Add(enemy);

                ApplySlowIfNeeded(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemyInZone.Remove(enemy);
                RemoveSlow(other.gameObject);
            }
        }

        public void Activate()
        {
            SetStats(0);
            _auraCoroutine = StartCoroutine(routine: CheckZone());
        }

        public void Deactivate()
        {
            if (_auraCoroutine != null)
            {
                StopCoroutine(_auraCoroutine);
            }

            for (int i = 0; i < _enemyInZone.Count; i++)
            {
                if (_enemyInZone[i] != null)
                    RemoveSlow(_enemyInZone[i].gameObject);
            }
        }

        public override void LevelUp()
        {
            base.LevelUp();
            RefreshSlowInZone();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel -1].Range;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _collider.radius = _range / 3f;
            RefreshSlowInZone();
        }

        private IEnumerator CheckZone()
        {
            while (true)
            {
                for (int i = _enemyInZone.Count - 1; i >= 0; i--)
                {
                    if (_enemyInZone[i] == null)
                    {
                        _enemyInZone.RemoveAt(i);
                        continue;
                    }

                    _enemyInZone[i].TakeDamage(_damage);
                }
                yield return _timeBetweenAttack;
            }
        }

        private bool ShouldSlowEnemies() => CurrentLevel >= SlowMinLevel;

        private void ApplySlowIfNeeded(GameObject enemyObject)
        {
            if (!ShouldSlowEnemies())
                return;

            if (enemyObject.TryGetComponent(out EnemyMove enemyMove))
                enemyMove.SetMovementSpeedMultiplier(SlowSpeedMultiplier);
        }

        private void RemoveSlow(GameObject enemyObject)
        {
            if (enemyObject.TryGetComponent(out EnemyMove enemyMove))
                enemyMove.SetMovementSpeedMultiplier(1f);
        }

        private void RefreshSlowInZone()
        {
            for (int i = 0; i < _enemyInZone.Count; i++)
            {
                if (_enemyInZone[i] == null)
                    continue;

                if (ShouldSlowEnemies())
                    ApplySlowIfNeeded(_enemyInZone[i].gameObject);
                else
                    RemoveSlow(_enemyInZone[i].gameObject);
            }
        }
    }
}
