using System.Collections;
using Assets.Scripts.GameCore.ExperienceSystem;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class BossHealth : EnemyHealth
    {
        [SerializeField] private float _bossExperienceReward = 100f;
        [SerializeField] private float _deathAnimationDuration = 1f;

        private BossController _bossController;
        private PlayerHealth _playerHealth;
        private ExperienceSystem _experienceSystem;
        private bool _isDying;

        private void Awake() => _bossController = GetComponent<BossController>();

        public override void TakeDamage(float damage)
        {
            if (_isDying)
                return;

            base.TakeDamage(damage);
        }

        protected override void HandleDeath()
        {
            if (_isDying)
                return;

            _isDying = true;
            _bossController.PlayDeath();
            StartCoroutine(BossDeathSequence());
        }

        private IEnumerator BossDeathSequence()
        {
            yield return new WaitForSeconds(_deathAnimationDuration);
            _playerHealth.FullHeal();
            _experienceSystem.PickUpExperience(_bossExperienceReward);
            gameObject.SetActive(false);
            _isDying = false;
        }

        [Inject]
        private void Construct(PlayerHealth playerHealth, ExperienceSystem experienceSystem)
        {
            _playerHealth = playerHealth;
            _experienceSystem = experienceSystem;
        }
    }
}
