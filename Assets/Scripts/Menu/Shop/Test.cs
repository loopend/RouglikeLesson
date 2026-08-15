using Assets.Scripts.Menu;
using Assets.Scripts.Player;
using Assets.Scripts.Save3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Menu.Shop
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private PlayerData _playerData;
        private MenuUIUpdater _menuUIUpdater;
        private SaveProgress _saveProgress;

        public void AddCoins()
        {
            _playerData.AddRewardCoins(1000);
            _saveProgress.SaveData();
            _menuUIUpdater.UpdateUI();
        }

        [Inject]
        private void Construct(PlayerData playerData, MenuUIUpdater menuUIUpdater, SaveProgress saveProgress)
        {
            _playerData = playerData;
            _menuUIUpdater = menuUIUpdater;
            _saveProgress = saveProgress;
        }
    }
}
