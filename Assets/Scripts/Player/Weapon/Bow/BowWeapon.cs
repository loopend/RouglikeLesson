using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.Pool;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Weapon.Bow
{
    public class BowWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _container, _shootPoint, _weaponTransform;
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Animator _animator;
        private WaitForSeconds _timeBetweenAttack;
        private PlayerMovement _playerMovement;
        private Coroutine _bowCoroutine;
        private Vector3 _direction;
        private float _duration, _speed;
        public float Duration => _duration;
        public float Speed => _speed;

        private void OnEnable()
        {
            Activate();
        }

        private void Update()
        {
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                _camera.transform.position.z - transform.position.z
            ));

            _direction = (mouseWorldPosition - transform.position).normalized;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            _weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }
        public void Activate()
        {
            SetStats(0);
            _bowCoroutine = StartCoroutine(routine:StartThrowArrow());
        }

        public void Deactivate()
        {
            if (_bowCoroutine != null)
            {
                StopCoroutine(_bowCoroutine);
            }
        }

        public void ThrowArrow()
        {
            GameObject arrow = _objectPool.GetFromPool();
            arrow.transform.SetParent(_container);
            arrow.transform.position = _shootPoint.position;
            arrow.transform.rotation = transform.rotation;
            _animator.SetTrigger(name: "Idle");
        }
        private IEnumerator StartThrowArrow()
        {
            while (true)
            {
                if (_playerMovement.Movement != Vector3.zero)
                {
                    _animator.SetTrigger(name: "Attack");
                }
                yield return _timeBetweenAttack;
            }
        }
        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }

    }
}
