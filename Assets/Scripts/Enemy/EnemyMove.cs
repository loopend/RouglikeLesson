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
    private Coroutine _distanceToHide;

    private float _movementSpeedMultiplier = 1f;

    private void OnEnable()
    {
        _movementSpeedMultiplier = 1f;
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

    private void Move()
    {
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



    [Inject] private void Construct(PlayerMovement playerMovement) => _playerMovement = playerMovement;
}
