using Assets.Scripts.GameCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon
{
    public class FireBallWeapon : BaseWeapon, IActivate
    {
        [Header("Single")]
        [SerializeField] private SpriteRenderer _spriteRenderer1X;
        [SerializeField] private Collider2D _collider1X;
        [SerializeField] private Transform _transformSprite1X, _targetContainer1X;
        [Header("Double")]
        [SerializeField] private List<SpriteRenderer> _spriteRenderer2X = new List<SpriteRenderer>();
        [SerializeField] private List<Collider2D> _collider2X;
        [SerializeField] private List<Transform> _transformSprite2X;
        [SerializeField] private Transform _targetContainer2X;
        private WaitForSeconds _interval, _duration, _timeBetweenAttack;
        private float _rotationSpeed, _range;
        private Coroutine _attackCoroutine;


        protected override void Start()
        {
            SetStats(0);
            SetupWeapon();
            //LevelUp();
            //LevelUp();
            //LevelUp();
            //LevelUp();
            //LevelUp();
            //LevelUp();
            //LevelUp();
        }


        private void Update()
        {
            transform.Rotate(xAngle:0, yAngle:0, zAngle: _rotationSpeed * Time.deltaTime);
        }
        public void Activate()
        {
            _attackCoroutine = StartCoroutine(routine: WeaponLifeCycle());
        }

        public void Deactivate()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }
        public override void LevelUp()
        {
            base.LevelUp();
            SetupWeapon();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _rotationSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].Duration);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
        }


        private void OnConnectedToServer()
        {
            if (CurrentLevel < 4)
            {
                _targetContainer1X.gameObject.SetActive(true);
                _targetContainer2X.gameObject.SetActive(false);
                _transformSprite1X.localPosition = new Vector3(_range, 0, 0);
                _collider1X.offset = new Vector2(_range, 0);
            }
        }
        private void SetupWeapon()
        {
            if (CurrentLevel < 4)
            {
                _targetContainer1X.gameObject.SetActive(true);
                _targetContainer2X.gameObject.SetActive(false);
                _transformSprite1X.localPosition = new Vector3(_range, 0, 0);
                _collider1X.offset = new Vector2 (_range, 0);
            }
            else
            {
                _targetContainer1X.gameObject.SetActive(false);
                _targetContainer2X.gameObject.SetActive(true);
                for (int i = 0; i < _collider2X.Count; i++)
                {
                    _collider2X[i].gameObject.SetActive(true);
                }
                _transformSprite2X[0].localPosition = new Vector3(_range, 0, 0);
                _transformSprite2X[1].localPosition = new Vector3(-_range, 0, 0);
                _collider2X[0].offset = new Vector2(_range, 0);
                _collider2X[1].offset = new Vector2(-_range, 0);
            }
        }

        private IEnumerator WeaponLifeCycle()
        {
            while (true)
            {
                if (CurrentLevel < 4)
                {
                    _spriteRenderer1X.enabled = !_spriteRenderer1X.enabled;
                    _collider1X.enabled = !_collider1X.enabled; 
                }
                else
                {
                    for (int i = 0; i < _spriteRenderer2X.Count; i++)
                    {
                        _spriteRenderer2X[i].enabled = !_spriteRenderer2X[i].enabled;
                        _collider2X[i].enabled = !_collider2X[i].enabled;
                    }
                }
                _interval = _spriteRenderer1X.enabled || 
                    _spriteRenderer2X[0] ? _duration : _timeBetweenAttack;
                yield return _interval;
            }
        }


    }
}
