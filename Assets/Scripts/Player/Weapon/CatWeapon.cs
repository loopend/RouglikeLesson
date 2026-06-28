using Assets.Scripts.GameCore;
using System.Collections;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Player.Weapon
{
    public class CatWeapon : BaseWeapon, IActivate
    {
        [Header("UI")]
        [SerializeField] private TMP_Text _weaponLevelText;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private Transform _catSprite;
        [SerializeField] private Animator _animator;

        private WaitForSeconds _interval;
        private WaitForSeconds _duration;
        private WaitForSeconds _timeBetweenAttack;
        private float _rotationSpeed;
        private float _range;
        private Coroutine _attackCoroutine;

        protected override void Start()
        {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();

            SetStats(0);
            SetupWeapon();
            Activate();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
            LevelUp();
        }

        private void Update()
        {
            if (_spriteRenderer == null || !_spriteRenderer.enabled)
                return;

            transform.Rotate(0f, 0f, _rotationSpeed * Time.deltaTime);
            UpdateAnimatorDirection();
        }

        public void Activate()
        {
            _attackCoroutine = StartCoroutine(WeaponLifeCycle());
        }

        public void Deactivate()
        {
            if (_attackCoroutine != null)
                StopCoroutine(_attackCoroutine);
        }

        public override void LevelUp()
        {
            base.LevelUp();
            SetupWeapon();
            UpdateUI();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _rotationSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].Duration);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            UpdateUI();
        }

        private void SetupWeapon()
        {
            if (_catSprite != null)
                _catSprite.localPosition = new Vector3(_range, 0f, 0f);

            if (_collider != null)
                _collider.offset = new Vector2(_range, 0f);
        }

        private void UpdateAnimatorDirection()
        {
            if (_animator == null)
                return;

            float angleRad = transform.eulerAngles.z * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
        }

        private void UpdateUI()
        {
            if (_weaponLevelText != null)
                _weaponLevelText.text = $"Уровень оружия: {CurrentLevel}";
        }

        private IEnumerator WeaponLifeCycle()
        {
            if (_spriteRenderer == null || _collider == null)
                yield break;

            while (true)
            {
                bool enable = !_spriteRenderer.enabled;
                _spriteRenderer.enabled = enable;
                _collider.enabled = enable;
                _interval = enable ? _duration : _timeBetweenAttack;
                yield return _interval;
            }
        }
    }
}
