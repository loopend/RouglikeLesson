using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.UI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();

        }
    }
}
