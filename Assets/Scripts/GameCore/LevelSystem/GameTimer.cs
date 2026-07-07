using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameCore.LevelSystem
{
    public class GameTimer : MonoBehaviour, IActivate
    {
        [SerializeField] private TMP_Text _gameTimerText;
        private LevelSystem _levelSystem;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerCorutine;
        private int _seconds, _minutes;
        public int Minutes => _minutes;

        public void Start()
        {
            Activate();
        }
        public void Activate()
        {
            _timerCorutine = StartCoroutine(routine:Timer());
        }

        public void Deactivate()
        {
            if (_timerCorutine != null)
            {
                StopCoroutine(_timerCorutine);
            }
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _seconds++;
                if(_seconds >= 60)
                {
                    _minutes++;
                    _seconds = 0;
                    _levelSystem.OnLevelChange?.Invoke();
                }
                TimerTextFormat();
                yield return _tick;
            }
        }

        private void TimerTextFormat()
        {
            _gameTimerText.text = $"{_minutes}:{_seconds}";
            if (_seconds < 10 && _minutes < 10)
            {
                _gameTimerText.text = $"0{_minutes}:0{_seconds}";
            }
            else if (_seconds < 10)
            {
                _gameTimerText.text = $"{_minutes}:0{_seconds}";
            }
            else if (_minutes < 10)
            {
                _gameTimerText.text = $"0{_minutes}:{_seconds}";
            }
        }


        [Inject]
        private void Construct(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }
    }
}
