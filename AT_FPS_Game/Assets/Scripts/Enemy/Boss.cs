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
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _dodgeRadius;

    [Header("Attack Settings")]
    [SerializeField] private int _attackRange;
    [SerializeField] private float _attackTime;
    [SerializeField] private bool _inAttackRange;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private GameObject _dropBomb;
    [SerializeField] private PlayerStatus _playerStat;

    public int EnemyHealth { get => _health; set => _health = value; }
    public int EnemyArmor { get => _armour; set => _armour = value; }


    void Update()
    {
        
    }
}
