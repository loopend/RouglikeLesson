using Assets.Scripts.GameCore.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Treasure : Loot
    {
        [SerializeField] private AudioClip _pickupSound;
        [SerializeField] [Range(0f, 1f)] private float _pickupSoundVolume = 1f;

        private TreasureWindow _treasureWindow;

        protected override void Pickup()
        {
            PlayPickupSound();
            base.Pickup();
            _treasureWindow.Activate();
        }

        private void PlayPickupSound()
        {
            if (_pickupSound != null)
                AudioSource.PlayClipAtPoint(_pickupSound, transform.position, _pickupSoundVolume);
        }

        [Inject]
        private void Construct(TreasureWindow treasureWindow)
        {
            _treasureWindow = treasureWindow;
        }
    }
}
