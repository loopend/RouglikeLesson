using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Player.Weapon.FrostBolt
{
    public class FrostBolt : Projectile
    {
        private const float StopDuration = 2f;
        private const float StopChance = 0.5f;
        private const int StopMinLevel = 5;

        private FrostBoltWeapon _frostBoltWeapon;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_frostBoltWeapon.Duration);
            Damage = _frostBoltWeapon.Damage;
        }

        private void Update()
        {
            transform.position += transform.right * (_frostBoltWeapon.Speed * Time.deltaTime);
        }
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);

                if (_frostBoltWeapon.CurrentLevel >= StopMinLevel &&
                    Random.value < StopChance &&
                    other.gameObject.TryGetComponent(out EnemyMove enemyMove))
                {
                    enemyMove.StopEnemy(StopDuration);
                }
            }
            if (_frostBoltWeapon.CurrentLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }

        [Inject] private void Construct(FrostBoltWeapon frostBoltWeapon) =>
            _frostBoltWeapon = frostBoltWeapon;

        
    }
}
