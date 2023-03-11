using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

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
    [SerializeField] private bool _isMiniBoss;
    [SerializeField] private Animator _animator;

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
    [SerializeField] private GameObject _projectile;
    [SerializeField] private PlayerStatus _playerStat;

    [Header("Item Drops")]
    [SerializeField] private int _itemdropChance;
    [SerializeField] private GameObject _itemDrop;
    [SerializeField] private GameObject _pistolAmmo;
    [SerializeField] private GameObject _shotgunAmmo;
    [SerializeField] private GameObject _biggunAmmo;
    [SerializeField] private GameObject _keyCard;

    //public delegate void AttackEvent();
    //public static event AttackEvent attackEvent;

    public int EnemyHealth { get => _health; set => _health = value; }

    private void Awake()
    {
        switch (_enemyType)
        {
            case (TypeOfEnemy.regular):
                {
                    _attack = 3;
                    _health = 10;
                    _isMiniBoss = false;

                    _sightRadius = 12;
                    _attackRadius = 10;
                    _attackRange = 11;
                    _attackTime = 2;

                    _itemdropChance = 5;
                    _itemDrop = _pistolAmmo;
                    break;
                }
            case (TypeOfEnemy.medium):
                {
                    _attack = 4;
                    _health = 15;
                    _isMiniBoss = false;

                    _sightRadius = 15;
                    _attackRadius = 13;
                    _attackRange = 14;
                    _attackTime = 2.5f;

                    _itemdropChance = 6;
                    _itemDrop = _shotgunAmmo;
                    break;
                }
            case (TypeOfEnemy.heavy):
                {
                    _attack = 5;
                    _health = 20;
                    _isMiniBoss = false;

                    _sightRadius = 18;
                    _attackRadius = 16;
                    _attackRange = 17;
                    _attackTime = 3;

                    _itemdropChance = 7;
                    _itemDrop = _biggunAmmo;
                    break;
                }
            case (TypeOfEnemy.miniBoss):
                {
                    _attack = 10;
                    _health = 50;
                    _isMiniBoss = true;

                    _sightRadius = 35;
                    _attackRadius = 25;
                    _attackRange = 27;
                    _attackTime = 4;

                    _itemDrop = _keyCard;
                    break;
                }
        }
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsShooting", false);
        _animator.SetBool("IsDying", false);

        agent = GetComponent<NavMeshAgent>();
        _newPatrolPoint = false;

        _player = GameObject.Find("Player").transform;
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _isAttacking = false;
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
        if (_inSightRange && !_inAttackRange)
        {
            Chasing();
        }
        else if(_inSightRange && _inAttackRange)
        {
            Attacking();  
        }

    }

    private void Patrolling()
    {
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsShooting", false);

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

        if(!_isAttacking)
        {
            _animator.SetBool("IsWalking", false);
            _animator.SetBool("IsShooting", true);

            if (_isMiniBoss)
            {
                GameObject gameObj1;
                GameObject gameObj2;
                gameObj1 = Instantiate(_projectile, (gameObject.transform.position), Quaternion.identity);
                gameObj1.GetComponent<Rigidbody>().AddForce(-_player.position * 50);
                gameObj2 = Instantiate(_projectile, gameObject.transform.position, Quaternion.identity);
                gameObj2.GetComponent<Rigidbody>().AddForce(-_player.position * 50);

                new WaitForSeconds(0.1f);
                gameObj1 = null;
                gameObj2 = null;
                StartCoroutine(ResetAttack());
            }
            else
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, -transform.forward, out hit, _attackRange);
                Debug.DrawRay(transform.position, -transform.forward * _attackRange);

                if (hit.collider.tag == "Player")
                {
                    Debug.Log("An enemy has hit you!");
                    DamagePlayer();
                    //StartCoroutine(ResetAttack());
                }
                StartCoroutine(ResetAttack());
            }
            _isAttacking = true;
        }
    }

    private IEnumerator ResetAttack()
    {
        _animator.SetBool("IsWalking", true);
        yield return new WaitForSeconds(_attackTime);
        _isAttacking = false;
    }

    private void DamagePlayer()
    {
        if(PlayerStatus._hasArmour)
        {
            PlayerStatus.Armour -= _attack;
        }
        else
        {
            PlayerStatus.Health -= _attack;
        }
    }

    private void Dying()
    {
        _animator.SetBool("IsDying", true);
        new WaitForSeconds(0.8f);

        if (_isMiniBoss)
        {
            Instantiate(_itemDrop, gameObject.transform.position, Quaternion.identity);
        }
        else
        {
            int i = Random.Range(0, 10);

            if (i >= _itemdropChance)
            {
                Instantiate(_itemDrop, gameObject.transform.position, Quaternion.identity);
            }
        }
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
