using UnityEngine;
using System;
using Zenject;

namespace Assets.Scripts.DI
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerHealth _playerHealth;
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
            Container.Bind<PlayerHealth>().FromInstance(_playerHealth).AsSingle().NonLazy();
        }
    }
}
