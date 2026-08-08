using Assets.Scripts.GameCore.Pause;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    private Vector3 _movement;
    private GamePause _gamePause;
    private float _initialSpeed;
    public Vector3 Movement => _movement;

    [SerializeField] private Joystick _joystick; 
    private bool _useJoystick = false;


    public bool IsUsingJoystick => _useJoystick;

    private void Start()
    {
        _initialSpeed = _moveSpeed;
    }



    private void Update() => Move();

    public void ToggleControl()
    {
        _useJoystick = !_useJoystick;
        Debug.Log(_useJoystick ? "Управление: Джойстик" : "Управление: Клавиатура");
    }

    private void Move()
    {
        _moveSpeed = _gamePause._isStopped ? 0 : _initialSpeed;
        if (_useJoystick)
        {
            _movement = new Vector3(_joystick.Horizontal, _joystick.Vertical, 0);
        }
        else
        {
            _movement = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"),
                0
            );
        }

        if (_movement.magnitude > 1f)
            _movement.Normalize();
            transform.position += _movement * (_moveSpeed * Time.deltaTime);
            _animator.SetFloat("Horizontal", _movement.x);
            _animator.SetFloat("Vertical", _movement.y);
            _animator.SetFloat("Speed", _movement.sqrMagnitude);
    }
    public void UpgradeSpeed()
    {
        _moveSpeed += 0.3f;
        _initialSpeed = _moveSpeed;
    }
    [Inject]
    private void Construct(GamePause gamePause)
    {
        _gamePause = gamePause;
    }

}