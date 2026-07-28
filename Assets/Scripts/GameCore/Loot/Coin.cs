using Assets.Scripts.GameCore.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Coin : Loot
    {
        [SerializeField] private AudioClip _pickupSound;
        [SerializeField] [Range(0f, 1f)] private float _pickupSoundVolume = 1f;

        private CoinsUIUpdater _coinsUpdater;
        private CoinsKeeper _coinsKeeper;

        protected override void Pickup()
        {
            PlayPickupSound();
            base.Pickup();
            _coinsKeeper.AddCoin();
            _coinsUpdater.OnCountChanged?.Invoke();
        }

        private void PlayPickupSound()
        {
            if (_pickupSound != null)
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position, _pickupSoundVolume);
        }

        [Inject]
        private void Construct(CoinsUIUpdater coinsUIUpdater, CoinsKeeper coinsKeeper)
        {
            _coinsKeeper = coinsKeeper;
            _coinsUpdater = coinsUIUpdater; 
        }
    }
}
