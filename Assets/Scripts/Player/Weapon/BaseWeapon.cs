using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Player.Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new List<WeaponStats>();
        [SerializeField] private float _damage;
        private DiContainer _diContainer;
        private int _currenHealth = 1;
        private int _maxHealth = 8;
        public List<WeaponStats> Weapons => _weaponStats;
        public float Damage => _damage;
        public int CurrentHealth => _currenHealth;
        public int MaxHealth => _maxHealth;

        private void Awake()
        {
            _diContainer.Inject(injectable:this);
        }
        private void Start()
        {
            SetStats(0);
        }

        public virtual void LevelUp()
        {
            if (_currenHealth < _maxHealth)
                _currenHealth++;
            SetStats(_currenHealth - 1);
        }


        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(_damage / 2f, _damage * 1.5f);
                enemy.TakeDamage(damage);
            }
        }


        protected virtual void SetStats(int value) => _damage = _weaponStats[value].Damage





        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

    }
}
