using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.Pool;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Weapon.Trap
{

  public class TrapWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _trapCoroutine;


        private void OnEnable()
        {
            Activate();
        }
        public void Activate()
        {
            SetStats(0);
            _trapCoroutine = StartCoroutine(routine: SpawnTrap());
        }

        public void Deactivate()
        {
            if (_trapCoroutine != null)
            {
                StopCoroutine(_trapCoroutine);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel -1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel -1].TimeBetweenAttack);

        }
        private IEnumerator SpawnTrap()
        {
            while (true)
            {
                GameObject trap = _objectPool.GetFromPool();
                trap.transform.SetParent(_container);
                trap.transform.position = transform.position;
                yield return _timeBetweenAttack;


            }
        }


    }
}
