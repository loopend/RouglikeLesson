using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Weapon.Bow
{
    public class Arrow : Projectile
    {
        private BowWeapon _bowWeapon;
        private float _speed;
        private float _maxRange;
        private float _traveledDistance;
        private Vector3 _direction;

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_bowWeapon.ShotDuration);
            Damage = _bowWeapon.ShotDamage;
            _speed = _bowWeapon.ShotSpeed;
            _maxRange = _bowWeapon.ShotRange;
            _direction = _bowWeapon.ShotDirection.normalized;
            _traveledDistance = 0f;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (_bowWeapon.CurrentLevel <= 4)
                gameObject.SetActive(false);
        }

        private void Update()
        {
            float step = _speed * Time.deltaTime;
            transform.position += _direction * step;
            _traveledDistance += step;

            if (_maxRange > 0f && _traveledDistance >= _maxRange)
                gameObject.SetActive(false);
        }

        [Inject]
        private void Construct(BowWeapon bowWeapon)
        {
            _bowWeapon = bowWeapon;
        }
    }
}
