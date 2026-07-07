using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GameCore.UpgradeSystem;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.ExperienceSystem
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private int _value;
        private ExperienceSystem _experienceSystem;
        private PlayerHealth _playerHealth;
        private PlayerUpgrade _playerUpgrade;
        private float _distanceToPickUp = 1.5f;
        private void OnEnable()
        {
            _distanceToPickUp = _playerUpgrade.RangeExp;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _experienceSystem.OnExpiriencePickUp?.Invoke(_value);
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
        private void Construct(ExperienceSystem experienceSystem, PlayerHealth playerHealth, PlayerUpgrade playerUpgrade)
        {
            _experienceSystem = experienceSystem;
            _playerHealth = playerHealth;   
            _playerUpgrade = playerUpgrade; 
        }


    }
}
