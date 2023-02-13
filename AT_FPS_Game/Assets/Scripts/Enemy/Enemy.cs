using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyType 
{
    regular,    
    medium,
    heavy,
    miniBoss
}
public class Enemy : MonoBehaviour
{   
    [Header("Basic settings")]
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private int _attack;
    public int _health;

    [Header("Patrol settings")]
    [SerializeField] private Transform _patrolPoint1;
    [SerializeField] private Transform _patrolPoint2;
    [SerializeField] private float _patrolSpeed;

    [Header("Attack settings")]
    [SerializeField] private Transform _origin;
    [SerializeField] private int _attackRange;
    [SerializeField] private float _attackRadius;
    [SerializeField] private LayerMask _isPlayer;
    //[SerializeField] private bool _inAttackRange;

    private float _timer;
    private bool _patrolling;
    private bool _attacking;
    private bool _dying;

    private bool atOne;
    private bool atTwo;

    private void Awake()
    {
        switch(_enemyType)
        {
            case (EnemyType.regular):
                {
                    _attack = 1;
                    _health = 3;
                    break;
                }
            case (EnemyType.medium):
                {
                    _attack = 2;
                    _health = 4;
                    break;
                }
            case (EnemyType.heavy):
                {
                    _attack = 3;
                    _health = 5;
                    break;
                }
            case (EnemyType.miniBoss):
                {
                    _attack = 5;
                    _health = 10;
                    break;
                }
        }

        gameObject.transform.position = _patrolPoint1.position;

        _patrolling = true;
        _attacking = Physics.CheckSphere(gameObject.transform.position, _attackRadius, _isPlayer);
        _dying = false;

        atOne = true;
        atTwo = false;

        StartCoroutine(PatrollingFuction());
    }

    void Update()
    {
        if (_patrolling)
        {
            StartCoroutine(PatrollingFuction());
        }
        if (_attacking)
        {
            _patrolling = false;
            StartCoroutine(AttackingFunction());
        }
        if (_health <= 0)
        {
            StartCoroutine(DyingFunction());
        }
    }

    private IEnumerator PatrollingFuction()
    {
        while(_patrolling)
        {
            if (gameObject.transform.position == _patrolPoint1.position)
            {
                _timer = float.Epsilon;
                while (_timer < _patrolSpeed)
                {
                    Vector3 newPosition = Vector3.Lerp(_patrolPoint1.position, _patrolPoint2.position, _timer / _patrolSpeed);
                    gameObject.transform.position = newPosition;
                    _timer += Time.deltaTime;

                    yield return new WaitForFixedUpdate();
                }
            }
            if (gameObject.transform.position == _patrolPoint2.position)
            {
                _timer = float.Epsilon;
                while (_timer < _patrolSpeed)
                {
                    Vector3 newPosition = Vector3.Lerp(_patrolPoint2.position, _patrolPoint1.position, _timer / _patrolSpeed);
                    gameObject.transform.position = newPosition;
                    _timer += Time.deltaTime;

                    yield return new WaitForFixedUpdate();
                }
            }
        }

        //Add stuff here for a collision sphere to start attacking
    }

    private IEnumerator AttackingFunction()
    {
        bool casting = false;

        if (!casting)    
        {
            casting = true;
            RaycastHit hit;
            Physics.Raycast(_origin.position, _origin.forward, out hit, _attackRange);
            Debug.DrawRay(_origin.position, _origin.forward * _attackRange);

            yield return new WaitForSeconds(2);
            casting = false;
        }

        //Add stuff here for patrolling again/follow player, if player is out of range
    }

    private IEnumerator DyingFunction()
    {
        //Add stuff for dyring animation
        yield return null;
        Destroy(gameObject);
    }
}

