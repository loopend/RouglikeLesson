using Assets.Scripts.Player;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Menu.Shop
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private PlayerData _playerData;
        public void AddCoins()
        {
            _playerData.AddRewardCoins(1000);
        }
        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }
        //public void ResetData()
        //{
        //    _playerData.SetUpgradeIndex();
        //}
    }
}
