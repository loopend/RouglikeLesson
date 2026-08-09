using Assets.Scripts.Menu;
using UnityEngine;
using Zenject;
using Assets.Scripts.Enemy;
using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.ExperienceSystem;
using Assets.Scripts.GameCore.LevelSystem;
using Assets.Scripts.GameCore.Loot;
using Assets.Scripts.GameCore.Pause;
using Assets.Scripts.GameCore.UI;
using Assets.Scripts.GameCore.UpgradeSystem;

namespace Assets.Scripts.DI
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private MenuUIUpdater _menuUIUpdater;
        public override void InstallBindings()
        {
            Container.Bind<MenuUIUpdater>().FromInstance(_menuUIUpdater);
        }
    }
}
