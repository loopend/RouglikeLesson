using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GameCore.ExperienceSystem
{
    public class ExperienceSystem : MonoBehaviour
    {
        public Action<float> OnExpiriencePickUp;
        [SerializeField] private GameObject _upgradeWindow;
        private float _currentExperience, _experienceToUp = 5; 
        private int _currentLevel = 1;

        public float CurrentExperience => _currentExperience;
        public float ExpirienceToUp => _experienceToUp;
        public int CurrentLevel => _currentLevel;

        public void PickUpExperience(float value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _currentExperience += value;
            if (_currentExperience >= _experienceToUp)
            {
                LevelUp();
            }

            OnExpiriencePickUp?.Invoke(value);
        }
        private void LevelUp()
        {
            _currentExperience = 0;
            _currentLevel++;
            switch (_currentLevel)
            {
                case <= 20:
                    _experienceToUp += 10;
                    break;
                case <= 40:
                    _experienceToUp += 13;
                    break;
                case <= 60:
                    _experienceToUp += 16;
                    break;
            }
        }

    }
}
