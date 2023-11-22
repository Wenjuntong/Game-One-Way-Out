using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiddleBossController : MonoBehaviour
{
    
    // Start is called before the first frame update
    private float detectionRange;
    private float attackCooldown;
    private float attackTimer;

    [SerializeField] private GameObject gate;

    public float speed = 6f;
    private GameObject targetPlayer;

    private Animator anim;
    private Rigidbody2D rb;
    private bool isPlayerOnRight;

    private Vector2 direction;
    enum EnemyState
    {
        Run,
        DetectedPlayer,
        death
    }

    EnemyState state = EnemyState.Run;

    IEnumerator waitforDeath(float waitTime)
    {
        if(gameObject.name == "Suski")
        {
            gate.SetActive(true);
        }

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
        PlayerAttribute playerAttribute = targetPlayer.GetComponent<PlayerAttribute>();
        EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();

        //increase player's exp
        playerAttribute.IncreaseExp(enemyAttribute.GetExperience());
    }

    IEnumerator waitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<MiddleBossController>().enabled = true;
    }

    IEnumerator AttackEffect(float waitTime)
    {
        anim.Play("MiddleBoss1_attack1");
        yield return new WaitForSeconds(waitTime);
        targetPlayer.GetComponent<PlayerAttribute>().TakeDamage(gameObject.GetComponent<EnemyAttribute>().GetDamage());
    }

    private void Awake()
    {
        detectionRange = gameObject.GetComponent<EnemyAttribute>().GetDetectionRange();
        attackCooldown = gameObject.GetComponent<EnemyAttribute>().GetattackCooldown();
    }

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        attackTimer = attackCooldown;
    }

    void Update()
    {
        if (targetPlayer != null)
        {
            isPlayerOnRight = (targetPlayer.transform.position.x > transform.position.x);
            anim.SetBool("isPlayerOnRight", isPlayerOnRight);
            if (state == EnemyState.Run) {
                direction = targetPlayer.transform.position - transform.position;
                rb.velocity = direction.normalized * speed;
                
            }
        }

        if (state == EnemyState.DetectedPlayer)
        {
            rb.velocity = Vector2.zero;
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                attackTimer = 0.0f;
                if (isPlayerOnRight)
                {
                    StartCoroutine(AttackEffect(1f));
                }
                else
                {
                    StartCoroutine(AttackEffect(1f));
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.DetectedPlayer;
            anim.SetBool("isLeaving", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.Run;
            anim.SetBool("isLeaving", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExplosionArea"))
        {
            EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
            EnemyController enemycontroller = gameObject.GetComponent<EnemyController>();
            MonsterController monstercontroller = gameObject.GetComponent<MonsterController>();
            WalkmanController walkmancontroller = gameObject.GetComponent<WalkmanController>();
            MiddleBossController middleBossController = gameObject.GetComponent<MiddleBossController>();
            FinalBossController finalBossController = gameObject.GetComponent<FinalBossController>();

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
                if (monstercontroller != null)
                {
                    enemyAttribute.TakeDamage(20);

                    if (enemyAttribute.GetHealth() > 0)
                    {
                        monstercontroller.HurtAnimation();
                    }
                    else
                    {
                        monstercontroller.DeathAnimation();
                    }
                }
                if (walkmancontroller != null)
                {
                    enemyAttribute.TakeDamage(20);

                    if (enemyAttribute.GetHealth() > 0)
                    {
                        walkmancontroller.HurtAnimation();
                    }
                    else
                    {
                        walkmancontroller.DeathAnimation();
                    }
                }

                if (middleBossController != null)
                {
                    enemyAttribute.TakeDamage(20);

                    if (enemyAttribute.GetHealth() > 0)
                    {
                        middleBossController.HurtAnimation();
                    }
                    else
                    {
                        middleBossController.DeathAnimation();
                    }
                }

                if (finalBossController != null)
                {
                    if (finalBossController.GetState() == 0)
                    {
                        enemyAttribute.TakeDamage(0);
                    }
                    else
                    {
                        enemyAttribute.TakeDamage(20);
                    }

                    if (enemyAttribute.GetHealth() <= 0)
                    {
                        finalBossController.DeathAnimation();
                    }
                }
            }
        }
    }

    public void HurtAnimation()
    {
        if (isPlayerOnRight)
        {
            anim.Play("MiddleBoss1_hit1");
            GetComponent<MiddleBossController>().enabled = false;
            StartCoroutine(waitTime(0.5f));
        }
        else
        {
            anim.Play("MiddleBoss1_hit0");
            GetComponent<MiddleBossController>().enabled = false;
            StartCoroutine(waitTime(0.5f));
        }
    }

    public void DeathAnimation()
    {
        state = EnemyState.death;

        Boss2Manager boss2Manager = gameObject.GetComponent<Boss2Manager>();
        if (boss2Manager != null)
        {
            boss2Manager.Boss2();
        }

        if (isPlayerOnRight)
        {
            rb.velocity = Vector2.zero;
            GetComponent<MiddleBossController>().enabled = false;
            anim.Play("MiddleBoss1_death1");
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(waitforDeath(1.5f));
        }
        else
        {
            rb.velocity = Vector2.zero;
            GetComponent<MiddleBossController>().enabled = false;
            anim.Play("MiddleBoss1_death0");
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            StartCoroutine(waitforDeath(1.5f));
        }
    }
}
