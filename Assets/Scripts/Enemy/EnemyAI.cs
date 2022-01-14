using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    private CharacterController _controller;
    private Vector3 _direction;
    private Vector3 _velocity;
    private Transform _player;
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private float _gravity = 20f;
    [SerializeField] private EnemyState _currentState = EnemyState.Chase;

    private Health _playerHealth;
    [SerializeField] private float _attackDelay = 1.5f;
    private float _nextAttack = -1;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
        {
            Debug.LogError("Enemy Controller Is NULL!!");
        }
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();
        if (_player == null || _playerHealth == null)
        {
            Debug.LogError("Player Components Are NULL!!");
        }
    }

    void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Chase:
                CalculateMovement();
                break;
        }
    }

    void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            _direction = _player.position - transform.position;
            _direction.y = 0;
            _direction.Normalize();

            //rotate towards the player
            transform.rotation = Quaternion.LookRotation(_direction);

            _velocity = _direction * _speed;
        }

        _velocity.y -= _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
            {
                _playerHealth.Damage(10);
            }
            _nextAttack = Time.time + _attackDelay;
        }
    }

    public void StartAttack()
    {
        _currentState = EnemyState.Attack;
    }
    public void StopAttack()
    {
        _currentState = EnemyState.Chase;
    }
}
