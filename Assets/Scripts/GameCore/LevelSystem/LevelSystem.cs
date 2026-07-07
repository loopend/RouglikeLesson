using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.LevelSystem
{
    public class LevelSystem : MonoBehaviour, IActivate
    {
        public Action OnLevelChange;
        [SerializeField] private List<Level> _levels = new List<Level>();
        private GameTimer _gameTimer;
        private DiContainer _diContainer;

        private void Awake()
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                _diContainer.Inject(_levels[i]);
            }
        }

        private void Start()
        {
            Activate();
        }

        private void OnEnable()
        {
            OnLevelChange += LevelUp;
        }
        private void OnDisable()
        {
            OnLevelChange -= LevelUp;
        }


        public void Activate()
        {
            _levels[_gameTimer.Minutes].Activate();  
        }

        public void Deactivate()
        {
            _levels[_gameTimer.Minutes].Deactivate();
        }
        
        private void LevelUp()
        {
            _levels[_gameTimer.Minutes - 1].Deactivate();
            Activate();
        }



        [Inject]
        private void Construct(GameTimer gameTimer, DiContainer diContainer)
        {
            _gameTimer = gameTimer;
            _diContainer = diContainer;
        }
    }
}
