using Assets.Scripts.Player;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
        }
    }
}