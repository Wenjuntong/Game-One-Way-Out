using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinalBossController : MonoBehaviour
{
    [SerializeField] private GameObject gate;

    private GameObject targetPlayer;
    private float MeleeCooldown;
    private float rangeCooldown;
    private float finalCooldown;
    private float immuneCooldown;
    private float maxHealth;
    private float Health;
    private float MeleeTimer;
    private float rangeTimer;
    private float finalTimer;
    private float immuneTimer;
    private float bulletSpeed;
    private bool isFacingRight;
    private float healthPercentage;
    private GameObject meleeDetection;
    private GameObject laser_left;
    private GameObject laser_right;
    private GameObject laser_top;
    private GameObject laser_bottom;
    private GameObject laser_topleft;
    private GameObject laser_topright;
    private GameObject laser_bottomleft;
    private GameObject laser_bottomright;
    private float random;
    public GameObject projectilePrefabLeft;
    public GameObject projectilePrefabRight;
    public Transform projectileSpawnPointLeft;
    public Transform projectileSpawnPointRight;
    private Animator anim;
    private float canAttack;
    NavMeshAgent agent;
    private Rigidbody2D rb;
    private Vector2 direction;
    public float speed = 4f;

    enum EnemyState
    {
        Idle,
        Melee,
        Immune,
        recover,
        final,
        death
    }

    EnemyState state = EnemyState.Idle;

    IEnumerator waitforDeath(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }

        IEnumerator waitRangeTimeRight(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FireProjectileRight();
    }

    IEnumerator waitRangeTimeLeft(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        FireProjectileLeft();
    }

    IEnumerator Skill(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canAttack++;
        state = EnemyState.Idle;
    }

    IEnumerator waitFinalTimeRight(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.Play("FinalBoss_lasercast_right");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Laser(1.5f));
    }

    IEnumerator waitFinalTimeLeft(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        anim.Play("FinalBoss_lasercast_left");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Laser(1.5f));
    }

    IEnumerator Laser(float waitTime)
    {
        laser_right.SetActive(true);
        laser_left.SetActive(true);
        laser_top.SetActive(true);
        laser_bottom.SetActive(true);
        laser_topleft.SetActive(true);
        laser_topright.SetActive(true);
        laser_bottomleft.SetActive(true);
        laser_bottomright.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        laser_left.SetActive(false);
        laser_right.SetActive(false);
        laser_top.SetActive(false);
        laser_bottom.SetActive(false);
        laser_topleft.SetActive(false);
        laser_topright.SetActive(false);
        laser_bottomleft.SetActive(false);
        laser_bottomright.SetActive(false);
        StartCoroutine(Skill(1f));
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
        MeleeCooldown = gameObject.GetComponent<EnemyAttribute>().GetattackCooldown();
        rangeCooldown = gameObject.GetComponent<EnemyAttribute>().GetBossRange();
        finalCooldown = gameObject.GetComponent<EnemyAttribute>().GetFinalAttack();
        immuneCooldown = gameObject.GetComponent<EnemyAttribute>().GetImmune();

        bulletSpeed = gameObject.GetComponent<EnemyAttribute>().GetbulletSpeed();
        maxHealth = gameObject.GetComponent<EnemyAttribute>().GetMaxHealth();

        MeleeTimer = MeleeCooldown;
        rangeTimer = 0.0f;
        finalTimer = finalCooldown;
        immuneTimer = immuneCooldown;

        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        anim = GetComponent<Animator>();

        meleeDetection = transform.Find("melee_detection").gameObject;
        laser_left = transform.Find("laser_left").gameObject;
        laser_right = transform.Find("laser_right").gameObject;
        laser_top = transform.Find("laser_top").gameObject;
        laser_bottom = transform.Find("laser_bottom").gameObject;
        laser_topleft = transform.Find("laser_topleft").gameObject;
        laser_topright = transform.Find("laser_topright").gameObject;
        laser_bottomleft = transform.Find("laser_bottomleft").gameObject;
        laser_bottomright = transform.Find("laser_bottomright").gameObject;

        canAttack = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Health = gameObject.GetComponent<EnemyAttribute>().GetHealth();
        healthPercentage = Health / maxHealth;
        if (targetPlayer != null)
        {
            isFacingRight = (targetPlayer.transform.position.x > transform.position.x);
            anim.SetBool("isFacingRight", isFacingRight);
            if (healthPercentage > 0.7f)
            {
                meleeDetection.SetActive(true);
                //SetAgentPosition();
                direction = targetPlayer.transform.position - transform.position;
                rb.velocity = direction.normalized * speed;
                if (state == EnemyState.Melee)
                {
                    MeleeTimer += Time.deltaTime;
                    if (MeleeTimer >= MeleeCooldown)
                    {
                        MeleeTimer = 0.0f;
                        if (isFacingRight)
                        {
                            anim.Play("FinalBoss_melee_right");
                            targetPlayer.GetComponent<PlayerAttribute>().TakeDamage(20);
                        }
                        else
                        {
                            anim.Play("FinalBoss_melee_left");
                            targetPlayer.GetComponent<PlayerAttribute>().TakeDamage(20);
                        }
                    }
                }
            }
            if (healthPercentage <= 0.7f && state == EnemyState.Idle)
            {
                rb.velocity = Vector2.zero;
                meleeDetection.SetActive(false);
                rangeTimer += Time.deltaTime;
                if (rangeTimer >= rangeCooldown)
                {
                    rangeTimer = 0.0f;
                    if (isFacingRight)
                    {
                        anim.Play("FinalBoss_rangeattack_right");
                        targetPlayer.GetComponent<PlayerAttribute>().TakeDamage(5);
                        StartCoroutine(waitRangeTimeRight(1f));

                    }
                    else
                    {
                        anim.Play("FinalBoss_rangeattack_left");
                        targetPlayer.GetComponent<PlayerAttribute>().TakeDamage(5);
                        StartCoroutine(waitRangeTimeLeft(1f));
                    }
                }
            }
            if (healthPercentage < 0.5 && healthPercentage > 0.35)
            {
                float probability = 1f;
                random = Random.Range(0f, 1f);
                finalTimer += Time.deltaTime;
                if (finalTimer >= finalCooldown)
                {
                    finalTimer = 0.0f;
                    if (random < probability)
                    {
                        state = EnemyState.final;
                        if (isFacingRight)
                        {
                            anim.Play("FinalBoss_glowing_right");
                            StartCoroutine(waitFinalTimeRight(1f));
                        }
                        else
                        {
                            anim.Play("FinalBoss_glowing_left");
                            StartCoroutine(waitFinalTimeLeft(1f));
                            
                        }
                    }
                }
            }
            if (healthPercentage == 0.5 && canAttack == 0)
            {
                state = EnemyState.recover;
                bulletSpeed += 10;
                if (isFacingRight)
                {
                    anim.Play("FinalBoss_recover_right");
                    gameObject.GetComponent<EnemyAttribute>().IncreaseHealth(200);
                    StartCoroutine(Skill(2f));
                }
                else
                {
                    anim.Play("FinalBoss_recover_left");
                    gameObject.GetComponent<EnemyAttribute>().IncreaseHealth(200);
                    StartCoroutine(Skill(2f));
                }
            }
            if (healthPercentage <= 0.35 && healthPercentage > 0.3)
            {
                float probability = 1f;
                random = Random.Range(0f, 1f);
                immuneTimer += Time.deltaTime;
                if (immuneTimer >= immuneCooldown)
                {
                    immuneTimer = 0.0f;
                    if (random < probability)
                    {
                        state = EnemyState.Immune;
                        if (isFacingRight)
                        {
                            anim.Play("FinalBoss_immune_right");
                            StartCoroutine(Skill(4.5f));
                        }
                        else
                        {
                            anim.Play("FinalBoss_immune_left");
                            StartCoroutine(Skill(4.5f));
                        }
                    }
                }
            }
            if (healthPercentage <= 0.3)
            {
                float probability = 0.7f;
                random = Random.Range(0f, 1f);
                finalCooldown = 10f;
                finalTimer += Time.deltaTime;
                if (finalTimer >= finalCooldown)
                {
                    finalTimer = 0.0f;
                    if (random < probability)
                    {
                        state = EnemyState.final;
                        if (isFacingRight)
                        {
                            anim.Play("FinalBoss_glowing_right");
                            StartCoroutine(waitFinalTimeRight(1.5f));
                        }
                        else
                        {
                            anim.Play("FinalBoss_glowing_left");
                            StartCoroutine(waitFinalTimeLeft(1.5f));
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.Melee;
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            state = EnemyState.Idle;
        }
    }
    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(targetPlayer.transform.position.x, targetPlayer.transform.position.y, targetPlayer.transform.position.z));
    }

    void FireProjectileLeft()
    {
        Transform childposition = transform.GetChild(1);
        GameObject projectile = Instantiate(projectilePrefabLeft, projectileSpawnPointLeft.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        Vector3 difference = targetPlayer.transform.position - childposition.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 180f);
        projectileRigidbody.velocity = difference.normalized * bulletSpeed;
        Destroy(projectile, 8f);
    }

    void FireProjectileRight()
    {
        Transform childposition = transform.GetChild(2);
        GameObject projectile = Instantiate(projectilePrefabRight, projectileSpawnPointRight.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        Vector3 difference = targetPlayer.transform.position - childposition.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        projectileRigidbody.velocity = difference.normalized * bulletSpeed;
        Destroy(projectile, 8f);
    }

    public void DeathAnimation()
    {
        anim.enabled = false;
        state = EnemyState.death;
        anim.enabled = true;

        if (isFacingRight)
        {
            anim.Play("FinalBoss_death_right");
            StartCoroutine(waitforDeath(2f));
        }
        else
        {
            anim.Play("FinalBoss_death_left");
            StartCoroutine(waitforDeath(2f));
        }

        gate.SetActive(true);
    }

    public int GetState()
    {
        if(state == EnemyState.Immune)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
