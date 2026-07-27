using Assets.Scripts.GameCore.UI;
using Zenject;

namespace Assets.Scripts.GameCore.Loot
{
    public class Treasure : Loot
    {
        private TreasureWindow _treasureWindow;
        protected override void Pickup()
        {
            base.Pickup();
            _treasureWindow.Activate();
        }

        [Inject]
        private void Construct(TreasureWindow treasureWindow)
        {
            _treasureWindow = treasureWindow;
        }
    }
}