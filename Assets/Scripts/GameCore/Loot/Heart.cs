using Assets.Scripts.GameCore.UI;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Heart : Loot
    {
        private const float HealPercent = 0.25f;
        private PlayerHealth _playerHealth;
        private ParticleHPSpawner _particleHPSpawner;

        protected override void Pickup()
        {
            _playerHealth.Heal(_playerHealth.MaxHealth * HealPercent);
            _particleHPSpawner.Spawn(_playerHealth.transform.position);
            base.Pickup();
        }

        [Inject]
        private void Construct(PlayerHealth playerHealth, ParticleHPSpawner particleHPSpawner)
        {
            _playerHealth = playerHealth;
            _particleHPSpawner = particleHPSpawner;
        }
    }
}
