using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemy
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1.2f;
        [SerializeField] private float _attackRange = 3f;
        [SerializeField] private float _attackDamage = 15f;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private float _attackHitDelay = 0.4f;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Death = Animator.StringToHash("Death");

        private PlayerMovement _playerMovement;
        private PlayerHealth _playerHealth;
        private bool _isAttacking;
        private bool _isDead;
        private float _attackTimer;

        private void OnEnable()
        {
            _isAttacking = false;
            _isDead = false;
            _attackTimer = 0f;
            _animator.SetBool(IsMoving, false);
        }

        private void Update()
        {
            if (_isDead)
                return;

            _attackTimer -= Time.deltaTime;

            if (_isAttacking)
                return;

            float distance = Vector3.Distance(transform.position, _playerMovement.transform.position);

            if (distance <= _attackRange && _attackTimer <= 0f)
            {
                StartCoroutine(PerformAttack());
                return;
            }

            if (distance > _attackRange)
            {
                Vector3 direction = (_playerMovement.transform.position - transform.position).normalized;
                transform.position += direction * (_moveSpeed * Time.deltaTime);
                _spriteRenderer.flipX = direction.x < 0f;
                _animator.SetBool(IsMoving, true);
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }
        }

        public void PlayDeath()
        {
            _isDead = true;
            _animator.SetBool(IsMoving, false);
            _animator.SetTrigger(Death);
        }

        private IEnumerator PerformAttack()
        {
            _isAttacking = true;
            _animator.SetBool(IsMoving, false);

            Vector3 direction = _playerMovement.transform.position - transform.position;
            if (direction.x != 0f)
                _spriteRenderer.flipX = direction.x < 0f;

            _animator.SetTrigger(Attack);
            yield return new WaitForSeconds(_attackHitDelay);

            if (!_isDead && Vector3.Distance(transform.position, _playerMovement.transform.position) <= _attackRange)
                _playerHealth.TakeDamage(_attackDamage);

            yield return new WaitForSeconds(_attackCooldown - _attackHitDelay);
            _attackTimer = 0f;
            _isAttacking = false;
        }

        [Inject]
        private void Construct(PlayerMovement playerMovement, PlayerHealth playerHealth)
        {
            _playerMovement = playerMovement;
            _playerHealth = playerHealth;
        }
    }
}
