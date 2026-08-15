using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GameCore.UI;
using Assets.Scripts.GameCore.UpgradeSystem;
using Assets.Scripts.Menu.Shop;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.ExperienceSystem
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private AudioClip _pickupSound;
        [SerializeField] [Range(0f, 1f)] private float _pickupSoundVolume = 1f;

        private ExperienceSystem _experienceSystem;
        private PlayerHealth _playerHealth;
        private PlayerUpgrade _playerUpgrade;
        private ParticleEXPSpawner _particleEXPSpawner;
        private float _distanceToPickUp = 1.5f;
        private UpgradeLoader _upgradeLoader;
        private void Start()
        {
            _distanceToPickUp = _upgradeLoader.RangeCurrentLevel.Value;
        }
        private void OnEnable()
        {
            _distanceToPickUp = _playerUpgrade.RangeExp;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                if (_pickupSound != null)
                    AudioSource.PlayClipAtPoint(_pickupSound, transform.position, _pickupSoundVolume);

                _experienceSystem.PickUpExperience(_value);
                _particleEXPSpawner.Spawn(playerHealth.transform.position);
                gameObject.SetActive(false);    
            }
        }
        private void Update()
        {
            if (Vector3.Distance(transform.position, _playerHealth.transform.position) <= _distanceToPickUp)
            {
                transform.position = Vector3.MoveTowards(current: transform.position, 
                    target: _playerHealth.transform.position, maxDistanceDelta: 2f * Time.deltaTime);

            }
        }

        [Inject]
        private void Construct(
            ExperienceSystem experienceSystem,
            PlayerHealth playerHealth,
            PlayerUpgrade playerUpgrade,
            ParticleEXPSpawner particleEXPSpawner, UpgradeLoader upgradeLoader)
        {
            _experienceSystem = experienceSystem;
            _playerHealth = playerHealth;   
            _playerUpgrade = playerUpgrade;
            _particleEXPSpawner = particleEXPSpawner;
            _upgradeLoader = upgradeLoader;
        }


    }
}
