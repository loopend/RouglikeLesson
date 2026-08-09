using Assets.Scripts.GameCore.Loot;
using Assets.Scripts.GameCore.Pause;
using Assets.Scripts.GameCore.UI;
using Assets.Scripts.Player;
using Assets.Scripts.Save3;
using Assets.Scripts.ScenesLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.GameCore.EndGame
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private Button _endButton;
        [SerializeField] private TMP_Text _coinsText;
        private WaitForSeconds _interval;
        private int _coins;

        private RewardCoinsAnimation _rewardCoinsAnimation;
        private CoinsKeeper _coinsKeeper;
        private PlayerData _playerData;
        private SaveProgress _saveProgress;
        private SceneLoader _sceneLoader;
        private GamePause _gamePause;

        private void OnEnable()
        {
            _gamePause.SetPause(true);
            _endButton.gameObject.SetActive(false);
            _coins = _coinsKeeper.Coins;
            _coinsText.text = "0";
            _interval = new WaitForSeconds(2.5f);
            StartCoroutine(routine: CalculateCoins());
        }
        public void ExitGame()
        {
            _playerData.AddRewardCoins(_coins);
            _saveProgress.SaveData();
            _sceneLoader.MainMenu();
        }

        private IEnumerator CalculateCoins()
        {
            if (_coins > 10)
            {
                _rewardCoinsAnimation.ActivateAnimation(targetValue: _coins, currentValue: 0, _coinsText);
            }
            else
            {
                _coinsText.text = _coins.ToString();
                _endButton.gameObject.SetActive(true);
            }
            yield return _interval;
        }





        [Inject]
        private void Construst(RewardCoinsAnimation coinsAnimation, CoinsKeeper coinsKeeper, 
            PlayerData playerData, SaveProgress saveProgress, GamePause gamePause, SceneLoader sceneLoader)
        {
            _rewardCoinsAnimation = coinsAnimation;
            _coinsKeeper = coinsKeeper;
            _playerData = playerData;
            _saveProgress = saveProgress;
            _gamePause = gamePause;
            _sceneLoader = sceneLoader;
        }
    }
}
