using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [Header("Basic Settings")]
    [SerializeField] private int _attack;
    [SerializeField] private int _health;
    [SerializeField] private int _armour;

    [Header("AI Variables")]
    public NavMeshAgent agent;
    [SerializeField] private Transform _player;
    [SerializeField] private LayerMask _isGround, _isPlayer;

    [Header("Moving Settings")]
    [SerializeField] private Vector3 _walkPoint;
    [SerializeField] private float _walkPointRange;
    [SerializeField] private float _dodgeRadius;
    [SerializeField] private bool _newWalkPoint;
    [SerializeField] private bool _inDodgeRadius;

    [Header("Attack Settings")]
    [SerializeField] private int _attackRange;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _attackTime;
    [SerializeField] private bool _inAttackRange;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _dropBomb;
    [SerializeField] private PlayerStatus _playerStat;

    public int EnemyHealth { get => _health; set => _health = value; }
    public int EnemyArmor { get => _armour; set => _armour = value; }

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player").transform;
        _playerStat = GameObject.Find("Player").GetComponent<PlayerStatus>();
        _newWalkPoint = false;
    }
    private void Update()
    {
        _inDodgeRadius = Physics.CheckSphere(gameObject.transform.position, _dodgeRadius, _isPlayer);

        MovingAround();

        if (_inDodgeRadius)
        {
            AvoidPlayer();
        }
    }

    private void MovingAround()
    {
        if (!_newWalkPoint)
        {
            float pointX = Random.Range(-_walkPointRange, _walkPointRange);
            float pointZ = Random.Range(-_walkPointRange, _walkPointRange);

            _walkPoint = new Vector3(transform.position.x + pointX, transform.position.y, transform.position.z + pointZ);

            if (Physics.Raycast(_walkPoint, -transform.up, 2f, _isGround))
            {
                _newWalkPoint = true;
            }
        }
        else if (_newWalkPoint)
        {
            agent.SetDestination(_walkPoint);
            Vector3 distanceToPatrolPont = transform.position - _walkPoint;

            if (distanceToPatrolPont.magnitude < 1f)
            {
                _newWalkPoint = false;
            }
        }
    }

    private void AvoidPlayer()
    {
        // set desitination to the oposite of the players transform (might not work quite like that)
    }

    private void AttackPhase1()
    {
        //regular raycast at set intervals
        //instantiate thrown bomb
    }

    private void AttackPhase2()
    {
        //Instantiate projectile at set interval
        //Tnstatiate dropped bomb while playing in dodge range 
    }

    private void DamagePlayer()
    {
        if (PlayerStatus._hasArmour)
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
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        
    }
}
