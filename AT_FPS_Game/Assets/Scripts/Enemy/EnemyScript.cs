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
public class EnemyScript : MonoBehaviour
{
    [Header("Basic settings")]
    [SerializeField] private TypeOfEnemy _enemyType;
    [SerializeField] private int _attack;
    [SerializeField] private int _health;

    [Header("AI Variables")]
    public NavMeshAgent agent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _isGround, _isPlayer;

    [Header("Patrol settings")]
    [SerializeField] private Vector3 _patrolPoint;
    [SerializeField] private float _patrolPointRange;
    [SerializeField] private float _sightRadius, _attackRadius;
    [SerializeField] private bool _isPatroling;
    [SerializeField] private bool _newPatrolPoint;
    [SerializeField] private bool _inSightRange;

    [Header("Attack Settings")]
    [SerializeField] private int _attackRange;
    [SerializeField] private float _attackTime;
    [SerializeField] private bool _inAttackRange;
    [SerializeField] private bool _isAttacking;
    [SerializeField] private PlayerStatus _playerStat;

    public int EnemyHealth { get => _health; set => _health = value; }

    private void Awake()
    {
        switch (_enemyType)
        {
            case (TypeOfEnemy.regular):
                {
                    _attack = 3;
                    _health = 10;
                    _sightRadius = 30;
                    _attackRadius = 20;
                    _attackRange = 22;
                    _attackTime = 2;
                    break;
                }
            case (TypeOfEnemy.medium):
                {
                    _attack = 4;
                    _health = 15;
                    _sightRadius = 25;
                    _attackRadius = 15;
                    _attackRange = 17;
                    _attackTime = 2.5f;
                    break;
                }
            case (TypeOfEnemy.heavy):
                {
                    _attack = 5;
                    _health = 20;
                    _sightRadius = 20;
                    _attackRadius = 15;
                    _attackRange = 17;
                    _attackTime = 3;
                    break;
                }
            case (TypeOfEnemy.miniBoss):
                {
                    _attack = 10;
                    _health = 50;
                    _sightRadius = 35;
                    _attackRadius = 25;
                    _attackRange = 27;
                    _attackTime = 5;
                    break;
                }
        }

        agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _newPatrolPoint = false;
    }

    private void Update()
    {
        _inSightRange = Physics.CheckSphere(gameObject.transform.position, _sightRadius, _isPlayer);
        _inAttackRange = Physics.CheckSphere(gameObject.transform.position, _attackRadius, _isPlayer);

        Patrolling();

        if (_health <= 0)
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
            StopAllCoroutines();
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

        else if (_newPatrolPoint)
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

    private void Attacking()
    {
        agent.SetDestination(transform.position);


        _isAttacking = true;
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.forward, out hit, _attackRange);
        Debug.DrawRay(transform.position, -transform.forward * _attackRange);

        if (hit.collider.tag == "Player")
        {
            Debug.Log("An enemy has hit you!");
            DamagePlayer();
            //ResetAttack();
        }
    
        //ResetAttack();
    }

    private void ResetAttack()
    {
        _isAttacking = false;
        new WaitForSeconds(_attackTime);

        Attacking();
    }

    private void DamagePlayer()
    {
        if(PlayerStatus.hasArmor)
        {
            _playerStat.Armor -= _attack;
        }
        else
        {
            _playerStat.Health -= _attack;
        }
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
