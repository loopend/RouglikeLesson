using Assets.Scripts.Player;
using Assets.Scripts.Save3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Menu.Shop
{
    public class UpgradeLoader : MonoBehaviour
    {
        [SerializeField] private List<ItemShop> _maxHealthLeveles = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _speedLeveles = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _regenLeveles = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _rangeLeveles = new List<ItemShop>();




        public ItemShop HealthCurrentLevel { get; private set; }
        public ItemShop SpeedCurrentLevel { get; private set; }
        public ItemShop RegenCurrentLevel { get; private set; }
        public ItemShop RangeCurrentLevel { get; private set; }
        private PlayerData _playerData;
        private SaveProgress _saveProgress;

        private void Awake()
        {
            _saveProgress.LoadData();
        }

        public void LoadCurrentLevels()
        {
            HealthCurrentLevel = _maxHealthLeveles[_playerData.MaxHealthUpgradeIndex -1];
            SpeedCurrentLevel = _speedLeveles[_playerData.SpeedUpgradeIndex -1];
            RegenCurrentLevel = _regenLeveles[_playerData.RegenerationUpgradeIndex -1];
            RangeCurrentLevel = _rangeLeveles[_playerData.ExpRangeUpgradeIndex -1];
        }

        [Inject]
        private void Construct(PlayerData playerData, SaveProgress saveProgress)
        {
            _playerData = playerData;
            _saveProgress = saveProgress;
        }


    }
}
