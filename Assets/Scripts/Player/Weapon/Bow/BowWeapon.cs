using Assets.Scripts.GameCore;
using Assets.Scripts.GameCore.Pool;
using System.Collections;
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
        [SerializeField] private Joystick _aimJoystick;

        [Header("Charge")]
        [SerializeField] private float _minChargeTime = 0.2f;
        [SerializeField] private float _maxChargeTime = 2f;
        [SerializeField] private float _minChargeMultiplier = 0.5f;
        [SerializeField] private float _maxChargeMultiplier = 2f;

        [Header("Aim")]
        [SerializeField] private float _aimDeadZone = 0.2f;

        private WaitForSeconds _timeBetweenAttack;
        private PlayerMovement _playerMovement;
        private Coroutine _cooldownCoroutine;

        private Vector3 _lastAimDirection = Vector3.right;
        private float _speed, _range, _duration;
        private float _chargeTime;
        private bool _isCharging;
        private bool _canShoot = true;

        private float _shotSpeed;
        private float _shotDamage;
        private float _shotRange;
        private float _shotDuration;
        private Vector3 _shotDirection = Vector3.right;

        public float Speed => _speed;
        public float Range => _range;
        public float Duration => _duration;
        public float ChargeProgress => Mathf.Clamp01((_chargeTime - _minChargeTime) / (_maxChargeTime - _minChargeTime));
        public bool IsCharging => _isCharging;
        public float ShotSpeed => _shotSpeed;
        public float ShotDamage => _shotDamage;
        public float ShotRange => _shotRange;
        public float ShotDuration => _shotDuration;
        public Vector3 ShotDirection => _shotDirection;

        protected override void Start()
        {
            SetStats(CurrentLevel - 1);
        }

        private void OnEnable()
        {
            Activate();
        }

        private void Update()
        {
            UpdateAim();
            HandleChargeInput();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            Assets.Scripts.Player.WeaponStats stats = WeaponStats[value];
            _timeBetweenAttack = new WaitForSeconds(stats.TimeBetweenAttack);
            _speed = stats.Speed;
            _range = stats.Range;
            _duration = stats.Range > 0f
                ? stats.Range / Mathf.Max(stats.Speed, 0.01f)
                : stats.Duration;
        }

        public void Activate()
        {
            SetStats(CurrentLevel - 1);
            _canShoot = true;
            _isCharging = false;
            _chargeTime = 0f;
        }

        public void Deactivate()
        {
            _isCharging = false;
            _chargeTime = 0f;

            if (_cooldownCoroutine != null)
            {
                StopCoroutine(_cooldownCoroutine);
                _cooldownCoroutine = null;
            }
        }

        public void ThrowArrow()
        {
            _shotDirection = _lastAimDirection.normalized;

            float angle = Mathf.Atan2(_shotDirection.y, _shotDirection.x) * Mathf.Rad2Deg;

            GameObject arrow = _objectPool.GetFromPool();
            arrow.transform.SetParent(_container, worldPositionStays: true);
            arrow.transform.position = _shootPoint.position;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            _animator.SetTrigger("Idle");
        }

        private void UpdateAim()
        {
            Vector3 aimDirection = GetAimDirection();
            if (aimDirection.sqrMagnitude <= 0.0001f)
                return;

            _lastAimDirection = aimDirection.normalized;
            float angle = Mathf.Atan2(_lastAimDirection.y, _lastAimDirection.x) * Mathf.Rad2Deg;
            _weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private Vector3 GetAimDirection()
        {
            if (_aimJoystick != null && _aimJoystick.gameObject.activeInHierarchy)
            {
                Vector3 joystickAim = new Vector3(_aimJoystick.Horizontal, _aimJoystick.Vertical, 0f);
                if (joystickAim.magnitude > _aimDeadZone)
                    return joystickAim;
            }

            float stickX = Input.GetAxisRaw("AimHorizontal");
            float stickY = Input.GetAxisRaw("AimVertical");
            Vector3 stickAim = new Vector3(stickX, stickY, 0f);
            if (stickAim.magnitude > _aimDeadZone)
                return stickAim;

            if (!_playerMovement.IsUsingJoystick)
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                mouseScreenPosition.z = Mathf.Abs(_camera.transform.position.z - _shootPoint.position.z);
                Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreenPosition);
                return mouseWorldPosition - _shootPoint.position;
            }

            if (_playerMovement.Movement.sqrMagnitude > 0.01f)
                return _playerMovement.Movement;

            return _lastAimDirection;
        }

        private void HandleChargeInput()
        {
            if (!_canShoot)
                return;

            if (Input.GetButtonDown("Fire1"))
            {
                _isCharging = true;
                _chargeTime = 0f;
            }

            if (!_isCharging)
                return;

            if (Input.GetButton("Fire1"))
            {
                _chargeTime = Mathf.Min(_chargeTime + Time.deltaTime, _maxChargeTime);
                return;
            }

            if (!Input.GetButtonUp("Fire1"))
                return;

            _isCharging = false;

            if (_chargeTime < _minChargeTime)
                _chargeTime = _minChargeTime;

            PrepareShotStats();
            _animator.SetTrigger("Attack");
            StartCooldown();
            _chargeTime = 0f;
        }

        private void PrepareShotStats()
        {
            float chargeProgress = Mathf.Clamp01((_chargeTime - _minChargeTime) / (_maxChargeTime - _minChargeTime));
            float chargeMultiplier = Mathf.Lerp(_minChargeMultiplier, _maxChargeMultiplier, chargeProgress);
            _shotSpeed = _speed * chargeMultiplier;
            _shotDamage = _damage * chargeMultiplier;
            _shotRange = _range * chargeMultiplier;
            _shotDuration = _shotRange / Mathf.Max(_shotSpeed, 0.01f);
        }

        private void StartCooldown()
        {
            _canShoot = false;

            if (_cooldownCoroutine != null)
                StopCoroutine(_cooldownCoroutine);

            _cooldownCoroutine = StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            yield return _timeBetweenAttack;
            _canShoot = true;
            _cooldownCoroutine = null;
        }

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerMovement = playerMovement;
        }
    }
}
