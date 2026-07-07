using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Assets.Scripts.GameCore.ExperienceSystem
{
    public class ExperienceUIUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Image _expBar;
        private ExperienceSystem _experienceSystem;


        private void OnEnable()
        {
            _experienceSystem.OnExpiriencePickUp += UpdateExperienceBar;
        }
        private void OnDisable()
        {
            _experienceSystem.OnExpiriencePickUp -= UpdateExperienceBar;
        }
        private void Start()
        {
            _expBar.fillAmount = 0f;
            _expText.text = "1 LVL";
        }

        private void UpdateExperienceBar(float experience)
        {
            _expBar.fillAmount = _experienceSystem.CurrentExperience / _experienceSystem.ExpirienceToUp;
            _expBar.fillAmount = Mathf.Clamp01(_expBar.fillAmount);
            _expText.text = $"{_experienceSystem.CurrentLevel} LVL";
        }

        [Inject]
        private void Consruct(ExperienceSystem experienceSystem)
        {
            _experienceSystem = experienceSystem;
        }
    }
}
