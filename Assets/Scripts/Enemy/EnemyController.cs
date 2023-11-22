using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //public float speed = 6f;
    private float detectionRange;
    private float attackCooldown;
    private float attackTimer;

    private float characterVelocity;
    private readonly float directionChangeTime = 3f;
    private float latestDirectionChangeTime;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    private float iceTimer;
    private GameObject targetPlayer;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isPlayerOnRight;
    private GameObject player;
    NavMeshAgent agent;
    private Vector2 direction;

    enum EnemyState
    {
        Idle,
        DetectedPlayer
    }

    EnemyState state = EnemyState.Idle;

    IEnumerator waitforDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
        //increase player's exp
        playerAttribute.IncreaseExp(enemyAttribute.GetExperience());
    }

    private void Awake()
    {
        detectionRange = gameObject.GetComponent<EnemyAttribute>().GetDetectionRange();
        attackCooldown = gameObject.GetComponent<EnemyAttribute>().GetattackCooldown();
        characterVelocity = gameObject.GetComponent<EnemyAttribute>().GetRandomVelocity();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        latestDirectionChangeTime = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        agent.speed = GetComponent<EnemyAttribute>().GetSpeed();
        attackTimer = attackCooldown;
    }

    void Update()
    {
        iceTimer -= Time.deltaTime;
        if (iceTimer < 0f)
        {
            agent.speed = GetComponent<EnemyAttribute>().GetSpeed();
        }

        FindClosestPlayer();
        if (targetPlayer != null)
        {
            SetAgentPosition();
            /*direction = targetPlayer.transform.position - transform.position;
            rb.velocity = direction.normalized * 3;*/
            isPlayerOnRight = (targetPlayer.transform.position.x > transform.position.x);
            anim.SetBool("isPlayerOnRight", isPlayerOnRight);
        }
        else
        {
            if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }
            //move enemy:
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
        if (state == EnemyState.DetectedPlayer)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0.0f;
                if (isPlayerOnRight)
                {
                    if (player.GetComponent<PlayerAttribute>().GetCurrentHealth() > 0)
                    {
                        anim.Play("monster06_attack");
                        EnemyAttribute enemyAttribute = GetComponent<EnemyAttribute>();
                        PlayerAttribute playerAttribute = targetPlayer.GetComponent<PlayerAttribute>();
                        //playerAttribute.TakeDamage(enemyAttribute.GetDamage());
                        playerAttribute.TakeDamage(10);
                        //enemyAttribute.GetDamage()
                    }
                }
                else
                {
                    if (player.GetComponent<PlayerAttribute>().GetCurrentHealth() > 0)
                    {
                        anim.Play("monster05_attack");
                        EnemyAttribute enemyAttribute = GetComponent<EnemyAttribute>();
                        PlayerAttribute playerAttribute = targetPlayer.GetComponent<PlayerAttribute>();
                        //playerAttribute.TakeDamage(enemyAttribute.GetDamage());
                        playerAttribute.TakeDamage(10);
                        //enemyAttribute.GetDamage()
                    }
                }
            }
        }
    }

    void FindClosestPlayer()
    {
        targetPlayer = null;

        if(player != null && player.GetComponent<PlayerAttribute>().GetCurrentHealth()>0)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < detectionRange)
            {
                targetPlayer = player;
            }
        }
        else
        {
            targetPlayer = null;
        }
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.DetectedPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SnowballArea"))
        {
            iceTimer = 2f;
            if(iceTimer > 0)
            {
                agent.speed = 0f;
            }
        }

        if (collision.CompareTag("ExplosionArea"))
        {
            EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
            EnemyController enemycontroller = gameObject.GetComponent<EnemyController>();

            if (enemyAttribute != null)
            {
                if (enemycontroller != null)
                {
                    enemyAttribute.TakeDamage(20);

                    if (enemyAttribute.GetHealth() > 0)
                    {
                        enemycontroller.HurtAnimation();
                    }
                    else
                    {
                        enemycontroller.DeathAnimation();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.Idle;
        }
    }

    public void HurtAnimation()
    {
        if (isPlayerOnRight)
        {
            anim.Play("monster08_hit");
        }
        else
        {
            anim.Play("monster07_hit");
        }
    }

    public void DeathAnimation()
    {
        if (isPlayerOnRight)
        {
            GetComponent<EnemyController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            anim.Play("monster04_death");
            rb.velocity = Vector2.zero;
            StartCoroutine(waitforDeath(anim.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            GetComponent<EnemyController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            anim.Play("monster03_death");
            rb.velocity = Vector2.zero;
            StartCoroutine(waitforDeath(anim.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z));
    }
}
