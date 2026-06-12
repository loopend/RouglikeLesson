using Assets.Scripts.Player.Weapon.FrostBolt;
using Assets.Scripts.Player.Weapon.Suriken;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private SurikenWeapon _surikenWeapon;
        [SerializeField] private FrostBoltWeapon _frostBoltWeapon;
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
            Container.Bind<SurikenWeapon>().FromInstance(_surikenWeapon).AsSingle().NonLazy();
            Container.Bind<FrostBoltWeapon>().FromInstance(_frostBoltWeapon).AsSingle().NonLazy();
        }
    }
}
