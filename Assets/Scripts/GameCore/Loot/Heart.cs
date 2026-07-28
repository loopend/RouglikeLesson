using Assets.Scripts.GameCore.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Heart : Loot
    {
        private const float HealPercent = 0.25f;

        [SerializeField] private AudioClip _pickupSound;
        [SerializeField] [Range(0f, 1f)] private float _pickupSoundVolume = 1f;

        private PlayerHealth _playerHealth;
        private ParticleHPSpawner _particleHPSpawner;

        protected override void Pickup()
        {
            PlayPickupSound();
            _playerHealth.Heal(_playerHealth.MaxHealth * HealPercent);
            _particleHPSpawner.Spawn(_playerHealth.transform.position);
            base.Pickup();
        }

        private void PlayPickupSound()
        {
            if (_pickupSound != null)
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position, _pickupSoundVolume);
        }

        [Inject]
        private void Construct(PlayerHealth playerHealth, ParticleHPSpawner particleHPSpawner)
        {
            _playerHealth = playerHealth;
            _particleHPSpawner = particleHPSpawner;
        }
    }
}
