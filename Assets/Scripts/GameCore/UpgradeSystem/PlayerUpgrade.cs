using Assets.Scripts.Player.Weapon;

using UnityEngine;
using Assets.Scripts.Player.Weapon.Bow;
using Assets.Scripts.Player.Weapon.FrostBolt;
using Assets.Scripts.Player.Weapon.Suriken;
using Assets.Scripts.Player.Weapon.Trap;
using System;
using Zenject;

namespace Assets.Scripts.GameCore.UpgradeSystem
{
    public class PlayerUpgrade : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private FireBallWeapon _fireBallWeapon;
        private AuraWeapon _auraWeapon;
        private SurikenWeapon _surikenWeapon;
        private TrapWeapon _trapWeapon;
        private FrostBoltWeapon _frostBoltWeapon;
        private CatWeapon _catWeapon;
        private BowWeapon _bowWeapon;

        public FireBallWeapon FireBallWeapon => _fireBallWeapon;
        public AuraWeapon AuraWeapon => _auraWeapon;
        public SurikenWeapon SurikenWeapon=> _surikenWeapon;
        public TrapWeapon TrapWeapon => _trapWeapon;
        public FrostBoltWeapon FrostBoltWeapon => _frostBoltWeapon;
        public CatWeapon CatWeapon => _catWeapon;
        public BowWeapon BowWeapon => _bowWeapon;

        public float RangeExp {get; private set;}

        private void Start() => RangeExp = 1.5f;

        public void UpgradeHealth() => _playerHealth.UpgradeHeath();

        public void UpgradeRegeneration() => _playerHealth.UpgradeRegeneration();

        public void UpgradeSpeed() => _playerMovement.UpgradeSpeed();
        public void UpgradeRangeExp() => RangeExp += 1f;

        public void UpgradeWeapon(BaseWeapon target)
        {
            if (target.gameObject.activeSelf)
            {
                target.LevelUp();
            }
            else
            {
                ActivateWeapon(target);
            }
        }
        private void ActivateWeapon(BaseWeapon target)
        {
            target.gameObject.SetActive(true);
        }
        [Inject]
        private void Construct(PlayerHealth health, PlayerMovement movement, FireBallWeapon fireBall, AuraWeapon aura,
            SurikenWeapon suriken, FrostBoltWeapon frostBolt, TrapWeapon trap, BowWeapon bow, CatWeapon cat)
        {
            _playerHealth = health;
            _playerMovement = movement; 
            _fireBallWeapon = fireBall;
            _auraWeapon = aura;
            _surikenWeapon = suriken;
            _frostBoltWeapon = frostBolt;
            _trapWeapon = trap;
            _bowWeapon = bow;
            _catWeapon = cat;
        }



    }
}
