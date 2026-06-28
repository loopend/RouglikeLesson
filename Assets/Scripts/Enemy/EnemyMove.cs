using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _freezeTimer;
    [SerializeField] private Animator _animator;
    private Vector3 _direction;
    private PlayerMovement _playerMovement;
    private WaitForSeconds _checkTime = new WaitForSeconds(3f);
    private WaitForSeconds _freeze = new WaitForSeconds(3f);
    private Coroutine _distanceToHide;
    private float _initialSpeed;

    private float _movementSpeedMultiplier = 1f;
    private bool _isStopped;

    private void Start()
    {
        _initialSpeed = _moveSpeed;
        _freeze = new WaitForSeconds(_freezeTimer);
    }
    private void OnEnable()
    {
        _movementSpeedMultiplier = 1f;
        _isStopped = false;
        _distanceToHide = StartCoroutine(CheckDistanceToHide());
    }

    public void SetMovementSpeedMultiplier(float multiplier) =>
        _movementSpeedMultiplier = Mathf.Clamp(multiplier, 0f, 1f);

    private void OnDisable()
    {
        if (_distanceToHide == null)
            return;
        StopCoroutine(_distanceToHide);
        _distanceToHide = null;
    }

    private void Update() => Move();

    public void FreezeEnemy()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(routine:StartFreeze()); 
        }
;    }

    public void StopEnemy(float duration)
    {
        if (!gameObject.activeSelf || _isStopped)
            return;

        StartCoroutine(StartStop(duration));
    }

    private void Move()
    {
        if (_isStopped)
            return;

        float distance = Vector3.Distance(transform.position, _playerMovement.transform.position);
        if (distance < 0.1f)
            return;
        _direction = (_playerMovement.transform.position - transform.position).normalized;
        transform.position += _direction * (_moveSpeed * _movementSpeedMultiplier * Time.deltaTime);
        _animator.SetFloat(name:"Horizontal", _direction.x);
        _animator.SetFloat(name:"Vertical", _direction.y);
    }

    private IEnumerator CheckDistanceToHide()
    {
        while (true)
        {
            float distance = Vector3.Distance(transform.position, _playerMovement.transform.position);
            if (distance > 20f)
                gameObject.SetActive(false);
            yield return _checkTime;
        }

    }

    private IEnumerator StartFreeze()
    {
        _moveSpeed /= 2f;
        yield return _freeze;
        _moveSpeed = _initialSpeed;
    }

    private IEnumerator StartStop(float duration)
    {
        _isStopped = true;
        yield return new WaitForSeconds(duration);
        _isStopped = false;
    }

    [Inject] private void Construct(PlayerMovement playerMovement) => _playerMovement = playerMovement;
}
