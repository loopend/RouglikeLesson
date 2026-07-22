using Assets.Scripts.GameCore.UI;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Coin : Loot
    {
        private CoinsUIUpdater _coinsUpdater;
        private CoinsKeeper _coinsKeeper;

        protected override void Pickup()
        {
            base.Pickup();
            _coinsKeeper.AddCoin();
            _coinsUpdater.OnCountChanged?.Invoke();
        }

        [Inject]
        private void Construct(CoinsUIUpdater coinsUIUpdater, CoinsKeeper coinsKeeper)
        {
            _coinsKeeper = coinsKeeper;
            _coinsUpdater = coinsUIUpdater; 
        }
    }
}