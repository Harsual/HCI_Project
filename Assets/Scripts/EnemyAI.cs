using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    [SerializeField] private Animator animator = null;
    [SerializeField] private GameController gameController;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    Collider m_Collider;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        health = 100;
        m_Collider = GetComponent<Collider>();
        //agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();

    }

    private void Patroling()
    {
        //animator.Play("Z_Walk1");

        animator.SetBool("isAttacking", false);
        animator.SetBool("isChasing", false);

        animator.SetBool("isPatroling", true);
        //Debug.Log("Patroling");
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            //Debug.Log(walkPoint);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint,-transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        animator.SetBool("isChasing", true);
        //Debug.Log("Chasing Player");
        agent.SetDestination(player.position);

    }

    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);


        animator.SetBool("isAttacking", true);
        //Debug.Log("Attacking Player");


        /*Vector3 targetPostition = new Vector3(this.transform.position.x,
                                       player.position.y,
                                       player.position.z);

        transform.LookAt(targetPostition);*/

        if (!alreadyAttacked)
        {
            //Attack code here

            //
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            m_Collider.enabled = false;
            animator.SetBool("isDead", true);
            UpdateScore();
            Invoke(nameof(DestroyEnemy),3f);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Debug.Log("ouch");
            TakeDamage(20);


        }
    }

    private void UpdateScore()
    {
        gameController.UpdateScore();
    }


}
