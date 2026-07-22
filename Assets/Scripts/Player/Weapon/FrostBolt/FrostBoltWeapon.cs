



using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon.FrostBolt
{
    [System.Serializable]
    public class FrostBoltWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private List<Transform> _shootPoints = new List<Transform>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _frostBoltCorutine;
        private float _duration, _speed;
        private Vector3 _direction;
        public float Speed => _speed;
        public Vector3 Direction => _direction;
        public float Duration => _duration; 
        private void OnEnable()
        {
            Activate();
        }
        public void Activate()
        {
            SetStats(0);
           _frostBoltCorutine = StartCoroutine(routine:StartThrowFrozenBolt());
        }

        public void Deactivate()
        {
            if (_frostBoltCorutine != null)
            {
                StopCoroutine(_frostBoltCorutine);
            }
        }
        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[value].TimeBetweenAttack);
            _speed = WeaponStats[value].Speed;
            _duration = WeaponStats[value].Duration;
        }

        private IEnumerator StartThrowFrozenBolt()
        {
            while (true)
            {
                for (int i = 0; i < _shootPoints.Count; i++)
                {
                    _direction = (_shootPoints[i].position - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject frozenBolt = _objectPool.GetFromPool();
                    frozenBolt.transform.SetParent(_container);
                    frozenBolt.transform.position = transform.position;
                    frozenBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);    
                }
                yield return _timeBetweenAttack;

            }
        }

    }
}

