using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    private float attackCooldown;
    private float bulletSpeed;

    private bool isFacingRight;
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    private bool isAttacking = false;
    private float attackTimer = 0.0f;
    private Animator anim;
    private GameObject targetPlayer;

    IEnumerator waitforDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
        PlayerAttribute playerAttribute = targetPlayer.GetComponent<PlayerAttribute>();
        EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
        //increase player's exp
        playerAttribute.IncreaseExp(enemyAttribute.GetExperience());
    }

    enum EnemyState
    {
        Idle,
        DetectedPlayer
    }

    EnemyState state = EnemyState.Idle;

    private void Awake()
    {
        characterVelocity = gameObject.GetComponent<EnemyAttribute>().GetRandomVelocity();
        bulletSpeed = gameObject.GetComponent<EnemyAttribute>().GetbulletSpeed();
        attackCooldown = gameObject.GetComponent<EnemyAttribute>().GetattackCooldown();
    }

    void Start()
    {
        latestDirectionChangeTime = 0f;
        anim = GetComponent<Animator>();
        calcuateNewMovementVector();
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void Update()
    {
        anim.SetBool("isAttacking", isAttacking);
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (targetPlayer != null)
            {
                isFacingRight = (targetPlayer.transform.position.x > transform.position.x);
                anim.SetBool("isFacingRight", isFacingRight);
            }

            if (attackTimer >= attackCooldown && state == EnemyState.DetectedPlayer)
            {
                attackTimer = 0.0f;
                FireProjectile();
            }
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
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = EnemyState.DetectedPlayer;
            isAttacking = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExplosionArea"))
        {
            EnemyAttribute enemyAttribute = gameObject.GetComponent<EnemyAttribute>();
            EnemyController enemycontroller = gameObject.GetComponent<EnemyController>();
            MonsterController monstercontroller = gameObject.GetComponent<MonsterController>();

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
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = EnemyState.Idle;
            isAttacking = false;
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        Vector3 difference = targetPlayer.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 180f);
        projectileRigidbody.velocity = difference.normalized * bulletSpeed;
        Destroy(projectile, 8f);
    }

    public void HurtAnimation()
    {
        if (isFacingRight)
        {
            anim.Play("Level2_monster_hit 02");
        }
        else
        {
            anim.Play("Level2_monster_hit");
        }
    }

    public void DeathAnimation()
    {
        if (isFacingRight)
        {
            anim.Play("Level2_monster_death 02");
            StartCoroutine(waitforDeath(anim.GetCurrentAnimatorStateInfo(0).length));
        }
        else
        {
            anim.Play("Level2_monster_death");
            StartCoroutine(waitforDeath(anim.GetCurrentAnimatorStateInfo(0).length));
        }
    }
}
