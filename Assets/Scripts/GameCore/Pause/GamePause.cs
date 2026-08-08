using Assets.Scripts.GameCore.LevelSystem;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.Pause
{
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private GameObject _playerWeapons;
        private LevelSystem.LevelSystem _levelSystem;
        private GameTimer _gameTimer;
        public bool _isStopped { get; private set;}


        public void SetPause(bool value)
        {
            if (value)
            {
                PauseOn();
            }
            else
            {
                PauseOff();
            }
        }
        private void PauseOn()
        {
            _levelSystem.Deactivate();
            _gameTimer.Deactivate();
            _isStopped = true;
            _playerWeapons.SetActive(false);

        }
        private void PauseOff()
        {
            _levelSystem.Activate();
            _gameTimer.Activate();
            _isStopped = false;
            _playerWeapons.SetActive(true);
        }
        [Inject]
        private void Construct(LevelSystem.LevelSystem levelSystem,GameTimer gameTimer)
        {
            _levelSystem = levelSystem;
            _gameTimer = gameTimer;
        }


    }
}