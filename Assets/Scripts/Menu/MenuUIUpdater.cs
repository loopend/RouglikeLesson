using Assets.Scripts.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Menu
{
    public class MenuUIUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        private PlayerData _playerData;
        public void UpdateUI()
        {
            _coinsText.text = _playerData.Coins.ToString(); 
        }

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
    }
}
