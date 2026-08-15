using Assets.Scripts.Player;
using Assets.Scripts.Save3;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Menu.Shop
{
    public class GameShop : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthCostText;
        [SerializeField] private TMP_Text _speedCostText;
        [SerializeField] private TMP_Text _regenCostText;
        [SerializeField] private TMP_Text _rangeCostText;

        [SerializeField] private Button _healthButton;
        [SerializeField] private Button _speedButton;
        [SerializeField] private Button _regenButton;
        [SerializeField] private Button _rangeButton;

        private UpgradeLoader _upgradeLoader;
        private PlayerData _playerData;
        private MenuUIUpdater _menuUIUpdater;
        private SaveProgress _saveProgress;


        public void ShowPrice()
        {
            _healthCostText.text = "Cost" + _upgradeLoader.HealthCurrentLevel.Cost;
            _speedCostText.text = "Cost" + _upgradeLoader.SpeedCurrentLevel.Cost;
            _regenCostText.text = "Cost" + _upgradeLoader.RegenCurrentLevel.Cost;
            _rangeCostText.text = "Cost" + _upgradeLoader.RangeCurrentLevel.Cost;
            _menuUIUpdater.UpdateUI();
        }

        public void CheckButtons()
        {
            _healthButton.interactable = _playerData.Coins >= _upgradeLoader.HealthCurrentLevel.Cost && _playerData.MaxHealthUpgradeIndex < 5;
            _speedButton.interactable = _playerData.Coins >= _upgradeLoader.SpeedCurrentLevel.Cost && _playerData.SpeedUpgradeIndex < 5;
            _regenButton.interactable = _playerData.Coins >= _upgradeLoader.RegenCurrentLevel.Cost && _playerData.RegenerationUpgradeIndex < 5;
            _rangeButton.interactable = _playerData.Coins >= _upgradeLoader.RangeCurrentLevel.Cost && _playerData.ExpRangeUpgradeIndex < 5;
        }

        public void TryUpgrade(int id)
        {
            switch (id)
            {
                case 1:
                    TrySpendCoins(_upgradeLoader.HealthCurrentLevel);
                    if (_playerData.MaxHealthUpgradeIndex < 5) 
                        _playerData.SetUpgradeIndex(_playerData.MaxHealthUpgradeIndex + 1, id: 1);
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;

                case 2:
                    TrySpendCoins(_upgradeLoader.SpeedCurrentLevel);
                    if (_playerData.SpeedUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.SpeedUpgradeIndex + 1, id: 2);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
                case 3:
                    TrySpendCoins(_upgradeLoader.RegenCurrentLevel);
                    if (_playerData.RegenerationUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.RegenerationUpgradeIndex + 1, id: 3);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
                case 4:
                    TrySpendCoins(_upgradeLoader.RangeCurrentLevel);
                    if (_playerData.ExpRangeUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.ExpRangeUpgradeIndex + 1, id: 4);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
            }
        }

        private void TrySpendCoins(ItemShop target)
        {
            _playerData.TrySpendCoins(target.Cost);
            _saveProgress.SaveData();
            _menuUIUpdater.UpdateUI();
        }
        [Inject]
        private void Construst(MenuUIUpdater menuUIUpdater, UpgradeLoader upgradeLoader, PlayerData playerData, SaveProgress saveProgress )
        {
            _menuUIUpdater = menuUIUpdater;
            _upgradeLoader = upgradeLoader;
            _playerData = playerData;
            _saveProgress = saveProgress;
        }
    }
}
