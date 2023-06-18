using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class enemy : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Transform player;
        public LayerMask whatIsGround, whatIsPlayer;
        public GameObject projectile;
        public float health;

        //Patrolling - Devriye variables
        public Vector3 walkPoint;
        bool walkPointSet;
        public float walkPointRange;

        //Attacking - Saldýrý variables
        public float timeBetweenAttacks;
        bool alreadyAttacked;

        //States
        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        private void Awake()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            //Check for sight and attack range - görüþ alaný ve saldýrý menzili algýlama
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInAttackRange && !playerInSightRange)
                Patroling();
            if (!playerInAttackRange && playerInSightRange)
                ChasePlayer();
            if (playerInAttackRange && playerInSightRange)
                AttackPlayer();
        }

        private void Patroling()
        {
            if (!walkPointSet)
                SearchWalkPoint();

            if(walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkpoint = transform.position - walkPoint;

            //Walkpoint reached - ulaþýldý
            if(distanceToWalkpoint.magnitude < 1f)
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            //Calculate random point in range - menzilde rastgele bir nokta hesapla
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            //make sure agent dont move - hareket etmediðinden emin ol
            agent.SetDestination(transform.position);
            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                //Attack code here
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        public void TakeDamage(int Damage)
        {
            health -= Damage;
            if (health <= 0)
                Invoke("DestroyEnemy", 0.5f);
        }

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }

    }
}
