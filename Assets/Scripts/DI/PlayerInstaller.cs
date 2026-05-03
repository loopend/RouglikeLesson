using UnityEngine;
using System;
using Zenject;

namespace Assets.Scripts.DI
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private PlayerMovement _playerMovement;
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle().NonLazy();
        }
    }
}
