using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Player.Weapon.Trap
{
    public class Trap : Projectile
    {
        [SerializeField] private CircleCollider2D _collider;
        private WaitForSeconds _checkInterval = new WaitForSeconds(3f);
        private PlayerHealth _playerHealth;
        private TrapWeapon _trapWeapon;

        protected override void OnEnable()
        {
            Damage = _trapWeapon.Damage;
            _collider.enabled = false;
            StartCoroutine(routine: CheckDistance());

        }


        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                if (enemy.gameObject.activeSelf)
                {
                    enemy.Burn(Damage);
                }
                gameObject.SetActive(false);

            }
        }

        public void ActivateTrap() => _collider.enabled = true;


        private IEnumerator CheckDistance()
        {
            while (true)
            {
                if (Vector3.Distance(transform.position, _playerHealth.gameObject.transform.position) > 15)
                {
                    gameObject.SetActive(false);
                }
                yield return _checkInterval;
            }
        }




        [Inject]
        private void Construct(PlayerHealth playerHealth, TrapWeapon trapWeapon)
        {
            _playerHealth = playerHealth;
            _trapWeapon = trapWeapon;   
        }
    }
}
