using Assets.Scripts.GameCore.UpgradeSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.GameCore.ExperienceSystem
{
    public class ExperienceSystem : MonoBehaviour
    {
        public Action<float> OnExpiriencePickUp;
        public Action<int> OnLevelUp;

        [SerializeField] private GameObject _upgradeWindow;
        [SerializeField] private AudioClip _levelUpSound;
        [SerializeField] [Range(0f, 1f)] private float _levelUpSoundVolume = 1f;
        [SerializeField] private float _baseExperienceToUp = 5f;
        [SerializeField] private float _earlyGameIncrement = 10f;
        [SerializeField] private float _midGameIncrement = 13f;
        [SerializeField] private float _lateGameIncrement = 16f;

        private float _currentExperience;
        private float _experienceToUp;
        private int _currentLevel = 1;

        public float CurrentExperience => _currentExperience;
        public float ExpirienceToUp => _experienceToUp;
        public int CurrentLevel => _currentLevel;

        private void Awake()
        {
            _experienceToUp = _baseExperienceToUp;
        }

        public void PickUpExperience(float value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _currentExperience += value;

            while (_currentExperience >= _experienceToUp)
            {
                LevelUp();
            }

            OnExpiriencePickUp?.Invoke(value);
        }

        private void LevelUp()
        {
            _currentExperience -= _experienceToUp;
            _currentLevel++;
            _upgradeWindow.SetActive(true);
            _upgradeWindow.GetComponent<UpgradeWindow>().GetRandomCard();
            _experienceToUp += GetExperienceIncrement(_currentLevel);

            OnLevelUp?.Invoke(_currentLevel);
            PlayLevelUpSound();

            if (_upgradeWindow != null)
            {
                _upgradeWindow.SetActive(true);
            }
        }

        private float GetExperienceIncrement(int level)
        {
            if (level <= 20)
            {
                return _earlyGameIncrement;
            }

            if (level <= 40)
            {
                return _midGameIncrement;
            }

            return _lateGameIncrement;
        }

        private void PlayLevelUpSound()
        {
            if (_levelUpSound != null)
            {
                AudioSource.PlayClipAtPoint(_levelUpSound, transform.position, _levelUpSoundVolume);
            }
        }
    }
}
