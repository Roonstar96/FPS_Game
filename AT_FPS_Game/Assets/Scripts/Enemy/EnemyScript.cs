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

    public delegate void AttackEvent();
    public static event AttackEvent attackEvent;

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

                    _sightRadius = 30;
                    _attackRadius = 20;
                    _attackRange = 22;
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

                    _sightRadius = 25;
                    _attackRadius = 15;
                    _attackRange = 17;
                    _attackTime = 2.5f;

                    _itemdropChance = 5;
                    _itemDrop = _shotgunAmmo;
                    break;
                }
            case (TypeOfEnemy.heavy):
                {
                    _attack = 5;
                    _health = 20;
                    _isMiniBoss = false;

                    _sightRadius = 20;
                    _attackRadius = 15;
                    _attackRange = 17;
                    _attackTime = 3;

                    _itemdropChance = 5;
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
                    _attackTime = 5;

                    _itemdropChance = 5;
                    _itemDrop = _keyCard;
                    break;
                }
        }

        agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _newPatrolPoint = false;
        _isAttacking = false;
    }

    private void OnEnable()
    {
        EnemyScript.attackEvent += Attacking;
    }

    private void OnDisable()
    {
        EnemyScript.attackEvent -= Attacking;
    }

    private void Update()
    {
        _inSightRange = Physics.CheckSphere(gameObject.transform.position, _sightRadius, _isPlayer);
        _inAttackRange = Physics.CheckSphere(gameObject.transform.position, _attackRadius, _isPlayer);

        Patrolling();
        //StartCoroutine(Attacking());

        if (_health <= 0)
        {
            Dying();
        }
        if (_inSightRange && !_inAttackRange)
        {
            //_isAttacking = false;
            Chasing();
        }
        else if (_inSightRange && _inAttackRange)
        {
            //StartCoroutine(ResetAttack());
            //Attacking();
            //_isAttacking = true;
            attackEvent();
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

        if (_isMiniBoss)
        {
            GameObject gameobj;
            gameobj = Instantiate(_projectile, gameObject.transform.position, Quaternion.identity);
            gameobj.GetComponent<Rigidbody>().AddForce(-_player.position * 50);

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
            }
        }
        StartCoroutine(ResetAttack());
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(_attackTime);
        attackEvent();
    }

    private void DamagePlayer()
    {
        if(PlayerStatus._hasArmour)
        {
            _playerStat.Armour -= _attack;
        }
        else
        {
            _playerStat.Health -= _attack;
        }
    }

    private void Dying()
    {
        //Add code for item dropping
        //Add a changce factor for regular enemies and the keycard a definite action for the mini boss
        if (_isMiniBoss)
        {
            //play animation, then drop
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
