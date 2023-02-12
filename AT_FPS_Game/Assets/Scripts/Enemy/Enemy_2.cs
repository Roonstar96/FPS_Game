using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_2 : MonoBehaviour
{
    [Header("AI Variables")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;

    [Header("Patrol settings")]
    public Vector3 patrolPoint;
    public float patrolPointRange;
    public float sightRange, attackRange;
    bool newPatrolPoint;
    bool inSightRange;

    [Header("Attack Settings")]
    public float attackTime;
    bool InAttackRange;
    bool isAttacking;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        inSightRange = Physics.CheckSphere(gameObject.transform.position, sightRange, thePlayer);
        inSightRange = Physics.CheckSphere(gameObject.transform.position, attackRange, thePlayer);

        if (inSightRange && !InAttackRange)
        {
            Chasing();
        }
        if (inSightRange && InAttackRange)
        {
            Attacking();
        }
        else
        {
            Patrolling();
        }
    }
    private void Patrolling()
    {
        if (!newPatrolPoint)
        {
            float pointX = Random.Range(-patrolPointRange, patrolPointRange);
            float pointZ = Random.Range(-patrolPointRange, patrolPointRange);

            patrolPoint = new Vector3(transform.position.x + pointX, transform.position.y, transform.position.z + pointZ);

            if (Physics.Raycast(patrolPoint, -transform.up, 2f, isGround))
            {
                newPatrolPoint = true;
            }
        }

        else
        {
            agent.SetDestination(patrolPoint);
            Vector3 distanceToPatrolPont = transform.position = patrolPoint;

            if (distanceToPatrolPont.magnitude < 1f)
            {
                newPatrolPoint = false;
            }
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);

        if (!isAttacking)
        {
            isAttacking = true;
            
        }
    }

    private IEnumerator ResetAttack()
    {
        isAttacking = false;
        yield retrun new WaitForSeconds(attackTime);
    }


}
