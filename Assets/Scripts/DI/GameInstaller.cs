using Assets.Scripts.Enemy;
using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.ExperienceSystem;
using Assets.Scripts.GameCore.LevelSystem;
using Assets.Scripts.GameCore.Pause;
using Assets.Scripts.GameCore.UI;
using Assets.Scripts.GameCore.UpgradeSystem;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private ExperienceSystem _experienceSystem;
        [SerializeField] private PlayerUpgrade _playerUpgrade;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private GamePause _gamePause;
        [SerializeField] private BossSpawner _bossSpawner;
        [SerializeField] private ParticleDamageSpawner _particleDamageSpawner;
        [SerializeField] private ParticleEXPSpawner _particleEXPSpawner;
        public override void InstallBindings()
        {
            LevelSystem();
            Experince();
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<PlayerUpgrade>().FromInstance(_playerUpgrade).AsSingle().NonLazy();
            Container.Bind<BossSpawner>().FromInstance(_bossSpawner).AsSingle().NonLazy();
            Container.Bind<ParticleDamageSpawner>().FromInstance(_particleDamageSpawner).AsSingle().NonLazy();
            Container.Bind<UpgradeWindow>().FromInstance(_upgradeWindow).AsSingle().NonLazy();
            Container.Bind<GamePause>().FromInstance(_gamePause).AsSingle().NonLazy();
        }
        private void Experince()
        {
            Container.Bind<ExperienceSpawner>().FromInstance(_experienceSpawner).AsSingle().NonLazy();
            Container.Bind<ExperienceSystem>().FromInstance(_experienceSystem).AsSingle().NonLazy();
            Container.Bind<ParticleEXPSpawner>().FromInstance(_particleEXPSpawner).AsSingle().NonLazy();
        }
        private void LevelSystem()
        {
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy();
        }
    }
}
