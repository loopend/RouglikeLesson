using Assets.Scripts.GameCore.Loot;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.UI
{
    public class CoinsUIUpdater : MonoBehaviour
    {
        public Action OnCountChanged;
        [SerializeField] private TMP_Text _coinsText;

        private CoinsKeeper _coinsKeeper;

        private void OnEnable()
        {
            OnCountChanged += UpdateCoinsText;
        }
        private void OnDisable()
        {
            OnCountChanged -= UpdateCoinsText;
        }
        private void UpdateCoinsText()
        {
            _coinsText.text = _coinsKeeper.Coins.ToString();
        }

        [Inject]
        private void Construct(CoinsKeeper coinsKeeper)
        {
            _coinsKeeper = coinsKeeper;
        }
    }
}