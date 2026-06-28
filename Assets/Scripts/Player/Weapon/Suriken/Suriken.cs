using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Weapon.Suriken
{
    [System.Serializable]
    public class Suriken : Projectile
    {
        private const int ReturnMinLevel = 5;
        private const int ReturnMaxLevel = 8;
        private const float ReturnReachDistance = 0.3f;

        [SerializeField] private Transform _sprite;
        private SurikenWeapon _surikenWeapon;
        private Transform _playerTransform;
        private bool _isReturning;
        private float _flightTimer;

        protected override void OnEnable()
        {
            Damage = _surikenWeapon.Damage;
            _isReturning = false;
            _flightTimer = 0f;
            _playerTransform = _surikenWeapon.transform;

            if (!ShouldReturn())
            {
                Timer = new WaitForSeconds(_surikenWeapon.Duration);
                base.OnEnable();
            }
        }

        private void Update()
        {
            _sprite.transform.Rotate(xAngle: 0, yAngle: 0, zAngle: 500f * Time.deltaTime);

            if (ShouldReturn())
                UpdateReturningFlight();
            else
                transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
        }

        private void UpdateReturningFlight()
        {
            if (!_isReturning)
            {
                transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
                _flightTimer += Time.deltaTime;

                if (_flightTimer >= _surikenWeapon.Duration)
                    _isReturning = true;

                return;
            }

            Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
            transform.position += directionToPlayer * (_surikenWeapon.Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _playerTransform.position) <= ReturnReachDistance)
                gameObject.SetActive(false);
        }

        private bool ShouldReturn() =>
            _surikenWeapon.CurrentLevel >= ReturnMinLevel &&
            _surikenWeapon.CurrentLevel <= ReturnMaxLevel;

        [Inject]
        private void Construct(SurikenWeapon surikenWeapon) =>
            _surikenWeapon = surikenWeapon;
    }
}
