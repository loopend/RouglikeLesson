using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.GameCore.Pause;
using TMPro;
using Assets.Scripts.GameCore.Loot;
using Zenject;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

namespace Assets.Scripts.GameCore.UI
{
    public class TreasureWindow : MonoBehaviour, IActivate
    {
        [SerializeField] private GameObject _treasureWindow;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private Button _button;

        private GamePause _gamePause;
        private CoinsKeeper _coinsKeeper;
        private CoinsUIUpdater _coinsUIUpdater;

        private int _randomCoinsToAdd;
        private WaitForSeconds _interval;
        private RewardCoinsAnimation _rewardCoinsAnimation;

        private void Start()
        {
            _interval = new WaitForSeconds(2.5f);
        }
        public void Activate()
        {
            _treasureWindow.SetActive(true);
            _gamePause.SetPause(true);
            _button.gameObject.SetActive(false);
            _randomCoinsToAdd = (int)Random.Range(10f, 100f);
            StartCoroutine(routine: StarCalculate());
        }

        public void Deactivate()
        {
            _treasureWindow.SetActive(false);
            _gamePause.SetPause(false);
        }
        public void GetReward()
        {
            _coinsKeeper.AddCoins(_randomCoinsToAdd);
            _coinsUIUpdater.OnCountChanged?.Invoke();
        }

        private IEnumerator StarCalculate()
        {
            _rewardCoinsAnimation.ActivateAnimation(targetValue: _randomCoinsToAdd, currentValue: 0, _coinsText);
            yield return _interval;
            GetReward();
            _button.gameObject.SetActive(true); 
        }


        [Inject]
        private void Construct(GamePause gamePause, CoinsKeeper coinsKeeper, CoinsUIUpdater coinsUIUpdater, 
            RewardCoinsAnimation rewardCoinsAnimation)

        {
            _gamePause = gamePause;
            _coinsKeeper = coinsKeeper; 
            _coinsUIUpdater = coinsUIUpdater;
            _rewardCoinsAnimation = rewardCoinsAnimation;
        }
    }
}