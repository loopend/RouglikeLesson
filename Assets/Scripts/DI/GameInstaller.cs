using Assets.Scripts.GameCore;

using Zenject;

namespace Assets.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();

        }
    }
}
