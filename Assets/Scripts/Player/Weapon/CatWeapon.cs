using Assets.Scripts.GameCore;
using UnityEngine;
using TMPro;
using Zenject;

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

        private Transform _playerTransform;
        private float _orbitAngle;
        private float _orbitSpeed;
        private float _range;

        [Inject]
        private void Construct(PlayerMovement playerMovement)
        {
            _playerTransform = playerMovement.transform;
        }

        protected override void Start()
        {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();

            SetStats(0);
            Activate();
        }

        private void LateUpdate()
        {
            if (_playerTransform != null)
                transform.position = _playerTransform.position;

            transform.rotation = Quaternion.identity;

            _orbitAngle += _orbitSpeed * Time.deltaTime;
            float angleRad = _orbitAngle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad)) * _range;

            if (_catSprite != null)
            {
                _catSprite.localPosition = new Vector3(offset.x, offset.y, 0f);
                _catSprite.localRotation = Quaternion.identity;
            }

            if (_collider != null)
                _collider.offset = offset;

            UpdateAnimatorDirection(angleRad);
        }

        public void Activate()
        {
            if (_spriteRenderer != null)
                _spriteRenderer.enabled = true;

            if (_collider != null)
                _collider.enabled = true;
        }

        public void Deactivate()
        {
            if (_spriteRenderer != null)
                _spriteRenderer.enabled = false;

            if (_collider != null)
                _collider.enabled = false;
        }

        public override void LevelUp()
        {
            base.LevelUp();
            UpdateUI();
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _orbitSpeed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            UpdateUI();
        }

        private void UpdateAnimatorDirection(float angleRad)
        {
            if (_animator == null)
                return;

            Vector2 direction = new Vector2(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
        }

        private void UpdateUI()
        {
            if (_weaponLevelText != null)
                _weaponLevelText.text = $"Уровень оружия: {CurrentLevel}";
        }
    }
}
