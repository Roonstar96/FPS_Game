using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum TypeOfEnemy
{
    regular,
    medium,
    heavy,
    miniBoss
}
public class Enemy_2 : MonoBehaviour
{
    [Header("Basic settings")]
    [SerializeField] private TypeOfEnemy _enemyType;
    [SerializeField] private int _attack;
    public int _health;

    [Header("AI Variables")]
    public NavMeshAgent agent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _isGround, _isPlayer;

    [Header("Patrol settings")]
    [SerializeField] private Vector3 _patrolPoint;
    [SerializeField] private float _patrolPointRange;
    [SerializeField] private float _sightRadius, _attackRadius;
    [SerializeField] private bool _newPatrolPoint;
    [SerializeField] private bool _inSightRange;

    [Header("Attack Settings")]
    [SerializeField] private int _attackRange; 
    [SerializeField] private float _attackTime;
    [SerializeField] private bool _inAttackRange;
    [SerializeField] private bool _isAttacking;


    private void Awake()
    {
        switch (_enemyType)
        {
            case (TypeOfEnemy.regular):
                {
                    _attack = 1;
                    _health = 3;
                    _sightRadius = 20;
                    _attackRadius = 10;
                    _attackRange = 12;
                    _attackTime = 2;
                    break;
                }
            case (TypeOfEnemy.medium):
                {
                    _attack = 2;
                    _health = 4;
                    _sightRadius = 15;
                    _attackRadius = 10;
                    _attackRange = 15;
                    _attackTime = 2;
                    break;
                }
            case (TypeOfEnemy.heavy):
                {
                    _attack = 3;
                    _health = 5;
                    _sightRadius = 10;
                    _attackRadius = 10;
                    _attackRange = 10;
                    _attackTime = 2;
                    break;
                }
            case (TypeOfEnemy.miniBoss):
                {
                    _attack = 5;
                    _health = 10;
                    _sightRadius = 30;
                    _attackRadius = 20;
                    _attackRange = 15;
                    _attackTime = 2;
                    break;
                }
        }

        agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;

        //Gizmos.color
    }

    private void Update()
    {
        _inSightRange = Physics.CheckSphere(gameObject.transform.position, _sightRadius, _isPlayer);
        _inAttackRange = Physics.CheckSphere(gameObject.transform.position, _attackRadius, _isPlayer);

        if(_health <= 0 )
        {
            Dying();
        }

        if (_inSightRange && _inAttackRange)
        {
            Attacking();
        }
        if (_inSightRange && !_inAttackRange)
        {
            Chasing();
        }
        else
        {
            _isAttacking = false;
            Patrolling();
        }

    }
    private void Patrolling()
    {
        if (!_newPatrolPoint)
        {
            float pointX = Random.Range(-_patrolPointRange, _patrolPointRange);
            float pointZ = Random.Range(-_patrolPointRange, _patrolPointRange);

            _patrolPoint = new Vector3(transform.position.x + pointX, transform.position.y, transform.position.z + pointZ);

            if (Physics.Raycast(_patrolPoint, -transform.up, 2f, _isGround))
            {
                _newPatrolPoint = true;
            }
        }

        else
        {
            agent.SetDestination(_patrolPoint);
            Vector3 distanceToPatrolPont = transform.position - _patrolPoint;

            if (distanceToPatrolPont.magnitude < 1f)
            {
                _newPatrolPoint = false;
            }
        }
    }

    private void Chasing()
    {
        agent.SetDestination(_player.position);
    }

    private IEnumerator Attacking()
    {
        agent.SetDestination(transform.position);

        if(!_isAttacking)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, transform.forward, out hit, _attackRange);
            Debug.DrawRay(transform.position, transform.forward * _attackRange);
            _isAttacking = true;
        }

        yield return new WaitForSeconds(_attackTime);
        _isAttacking = false;
    }
    private void Dying()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _sightRadius);
    }
}
