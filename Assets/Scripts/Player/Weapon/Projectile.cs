using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Assets.Scripts.Player.Weapon
{
    public class Projectile : MonoBehaviour
    {
        protected WaitForSeconds Timer;
        protected float Damage;

        protected virtual void OnEnable()
        {
            StartCoroutine(routine: TimerToHide());
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(Damage / 1.5f, Damage * 1.5f);
                if(damage < 1)
                    damage = 1;
                enemy.TakeDamage(damage);
            }
        }

        private IEnumerator TimerToHide()
        {
            yield return Timer;
            gameObject.SetActive(false);
        }

    }

}
