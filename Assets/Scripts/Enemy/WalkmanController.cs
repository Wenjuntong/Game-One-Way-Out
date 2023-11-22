using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkmanController : MonoBehaviour
{

    private float detectionRange;
    private float attackCooldown;
    private float attackTimer;

    private readonly float directionChangeTime = 3f;
    private GameObject targetPlayer;
    
    private Animator anim;
    private bool isPlayerOnRight;
    private GameObject player;
    NavMeshAgent agent;

    private float timeUntilDirectionChange;
    private bool isMovingRight;

    private float iceTimer;
    private bool isFreeze = false;

    /**public float speed = 6f;
    private float latestDirectionChangeTime;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float characterVelocity;
    private Rigidbody2D rb;**/
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
        //characterVelocity = gameObject.GetComponent<EnemyAttribute>().GetRandomVelocity();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //latestDirectionChangeTime = 0f;
        //rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;

        attackTimer = attackCooldown;

        timeUntilDirectionChange = directionChangeTime;
    }

    void Update()
    {
        iceTimer -= Time.deltaTime;
        if (iceTimer < 0f)
        {
            agent.speed = GetComponent<EnemyAttribute>().GetSpeed();
            isFreeze = false;
        }

        FindClosestPlayer();
        if (targetPlayer != null && !isFreeze)
        {
            anim.SetBool("isWalking", true);
            SetAgentPosition();
            isPlayerOnRight = (targetPlayer.transform.position.x > transform.position.x);
            anim.SetBool("isPlayerOnRight", isPlayerOnRight);
        }
        else
        {
            /**if (Time.time - latestDirectionChangeTime > directionChangeTime)
            {
                latestDirectionChangeTime = Time.time;
                calcuateNewMovementVector();
            }
            //move enemy:
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
            transform.position.y + (movementPerSecond.y * Time.deltaTime));**/
            anim.SetBool("isWalking", false);
            timeUntilDirectionChange -= Time.deltaTime;

            if (timeUntilDirectionChange <= 0)
            {
                // Randomly change direction
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    isMovingRight = true;
                    anim.SetBool("isPlayerOnRight", isMovingRight); // Flip sprite
                }
                else
                {
                    isMovingRight = false;
                    anim.SetBool("isPlayerOnRight", isMovingRight); // Flip sprite
                }

                timeUntilDirectionChange = directionChangeTime;
            }
        }

        if (state == EnemyState.DetectedPlayer)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0.0f;
                //atack
            }
        }
    }

    void FindClosestPlayer()
    {
        targetPlayer = null;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < detectionRange)
        {
            targetPlayer = player;
        }
    }

    /**void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }**/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.DetectedPlayer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.DetectedPlayer;
            player.GetComponent<PlayerAttribute>().TakeDamage(gameObject.GetComponent<EnemyAttribute>().GetDamage());
        }

        if (collision.CompareTag("SnowballArea"))
        {
            iceTimer = 2f;
            if (iceTimer > 0)
            {
                agent.speed = 0f;
                anim.SetBool("isWalking", false);
                isFreeze = true;
            }
        }

        if (collision.CompareTag("ExplosionArea"))
        {
            EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
            EnemyController enemycontroller = gameObject.GetComponent<EnemyController>();
            MonsterController monstercontroller = gameObject.GetComponent<MonsterController>();
            WalkmanController walkmanController = gameObject.GetComponent<WalkmanController>();

            if (enemyAttribute != null)
            {
                enemyAttribute.TakeDamage(20);
                if (enemycontroller != null)
                {
                    if (enemyAttribute.GetHealth() > 0)
                    {
                        enemycontroller.HurtAnimation();
                    }
                    else
                    {
                        enemycontroller.DeathAnimation();
                    }
                }
                if (monstercontroller != null)
                {
                    if (enemyAttribute.GetHealth() > 0)
                    {
                        monstercontroller.HurtAnimation();
                    }
                    else
                    {
                        monstercontroller.DeathAnimation();
                    }
                }
                if (walkmanController != null)
                {
                    if (enemyAttribute.GetHealth() > 0)
                    {
                        walkmanController.HurtAnimation();
                    }
                    else
                    {
                        walkmanController.DeathAnimation();
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
            anim.Play("Walkman1_hit1");
        }
        else
        {
            anim.Play("Walkman1_hit0");
        }
    }

    public void DeathAnimation()
    {
        if (isPlayerOnRight)
        {
            GetComponent<WalkmanController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            anim.Play("Walkman1_death1");
            StartCoroutine(waitforDeath(anim.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            GetComponent<WalkmanController>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            anim.Play("Walkman1_death0");
            StartCoroutine(waitforDeath(anim.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z));
    }
}
