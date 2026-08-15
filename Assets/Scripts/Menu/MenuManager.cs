using Assets.Scripts.Menu.Shop;
using Assets.Scripts.ScenesLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private SceneLoader _sceneLoader;
        private MenuUIUpdater _menuUIUpdater;
        private GameShop _gameShop;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(_sceneLoader.Game);
            _shopButton.onClick.AddListener(OpenShopWindow);
            _closeButton.onClick.AddListener(Application.Quit);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(_sceneLoader.Game);
            _shopButton.onClick.RemoveListener(OpenShopWindow);
            _closeButton.onClick.RemoveListener(Application.Quit);
        }
         private void OpenShopWindow()
        {
            _shopWindow.SetActive(true);
            _menuUIUpdater.UpdateUI();
            _gameShop.ShowPrice();
            _gameShop.CheckButtons();
        }
         private void StartGame()
        {
            _sceneLoader.Game();
        }

        [Inject]
        private void Construct(SceneLoader sceneLoader, MenuUIUpdater menuUIUpdater, GameShop gameShop)
        {
            _sceneLoader = sceneLoader;
            _menuUIUpdater = menuUIUpdater;
            _gameShop = gameShop; 
        }
    }
}
