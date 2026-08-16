using Assets.Scripts.Menu.Shop;
using Assets.Scripts.Save3;
using Assets.Scripts.ScenesLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private GameObject _shopWindow;
        [SerializeField] private Button _resetGameButton;
        [SerializeField] private Button _yesButton;
        [SerializeField] private Button _noButton;
        [SerializeField] private GameObject _resetGameWindow;

        private SceneLoader _sceneLoader;
        private MenuUIUpdater _menuUIUpdater;
        private GameShop _gameShop;
        private SaveProgress _saveProgress;
        private UpgradeLoader _upgradeLoader;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(_sceneLoader.Game);
            _shopButton.onClick.AddListener(OpenShopWindow);
            _closeButton.onClick.AddListener(Application.Quit);
            _resetGameButton.onClick.AddListener(OpenResetWindow);
            _yesButton.onClick.AddListener(ConfirmReset);
            _noButton.onClick.AddListener(CloseResetWindow);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(_sceneLoader.Game);
            _shopButton.onClick.RemoveListener(OpenShopWindow);
            _closeButton.onClick.RemoveListener(Application.Quit);
            _resetGameButton.onClick.RemoveListener(OpenResetWindow);
            _yesButton.onClick.RemoveListener(ConfirmReset);
            _noButton.onClick.RemoveListener(CloseResetWindow);
        }

        private void OpenShopWindow()
        {
            _shopWindow.SetActive(true);
            _menuUIUpdater.UpdateUI();
            _gameShop.ShowPrice();
            _gameShop.CheckButtons();
        }

        private void OpenResetWindow()
        {
            _resetGameWindow.SetActive(true);
        }

        private void CloseResetWindow()
        {
            _resetGameWindow.SetActive(false);
        }

        private void ConfirmReset()
        {
            _saveProgress.ResetData();
            _upgradeLoader.LoadCurrentLevels();
            _menuUIUpdater.UpdateUI();
            _gameShop.CheckButtons();
            CloseResetWindow();
        }

        [Inject]
        private void Construct(
            SceneLoader sceneLoader,
            MenuUIUpdater menuUIUpdater,
            GameShop gameShop,
            SaveProgress saveProgress,
            UpgradeLoader upgradeLoader)
        {
            _sceneLoader = sceneLoader;
            _menuUIUpdater = menuUIUpdater;
            _gameShop = gameShop;
            _saveProgress = saveProgress;
            _upgradeLoader = upgradeLoader;
        }
    }
}
