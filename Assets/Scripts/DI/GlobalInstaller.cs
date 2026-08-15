using Assets.Scripts.Menu.Shop;
using Assets.Scripts.Player;
using Assets.Scripts.Save3;
using Assets.Scripts.ScenesLoader;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeLoader _upgradeLoader;
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<SaveProgress>().FromNew().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();
            Container.Bind<UpgradeLoader>().FromInstance(_upgradeLoader);
        }
    }
}