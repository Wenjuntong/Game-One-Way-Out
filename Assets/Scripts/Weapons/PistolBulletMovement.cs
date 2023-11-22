using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
public class PistolBulletMovement : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    //[SerializeField] float force = 10f;
    [SerializeField] Sprite FlashSprite;
    [Range(0, 5)] public int FramesToFlash = 1;
    [SerializeField] private ParticleSystem bulletEffectPrefab;

    private Rigidbody2D rb;
    private GameObject player1;
    private PlayerAttribute playerAttribute;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player");
        playerAttribute = player1.GetComponent<PlayerAttribute>(); 
        rb = GetComponent<Rigidbody2D>();

        //StartCoroutine(DoFlash());

        /*Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        rb.velocity = new Vector2(difference.x, difference.y).normalized * bulletSpeed;*/
        Vector3 newMovement = transform.rotation * new Vector3(1, 0, 1);
        rb.velocity = new Vector2(newMovement.x, newMovement.y).normalized * bulletSpeed;
    }

    private void Update()
    {
        float difference = Vector2.Distance(transform.position, player1.transform.position);
        if (difference > 10f)
        {
            ParticleSystem bulletEffect = Instantiate(bulletEffectPrefab, transform.position, Quaternion.identity);
            bulletEffect.Play();
            Destroy(gameObject);
            Destroy(bulletEffect.gameObject, 0.6f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Environment") || collision.transform.CompareTag("Enemy"))
        {
            //clone a bullet effect at the position of the bullet
            ParticleSystem bulletEffect = Instantiate(bulletEffectPrefab, transform.position, Quaternion.identity);
            bulletEffect.Play();
            Destroy(gameObject) ;
            //Destroy bulletEffect after 0.6 seconds
            Destroy(bulletEffect.gameObject, 0.6f);

            EnemyAttribute enemyAttribute = collision.GetComponent<EnemyAttribute>();
            EnemyController enemycontroller = collision.GetComponent<EnemyController>();
            MonsterController monstercontroller = collision.GetComponent<MonsterController>();
            WalkmanController walkmancontroller = collision.GetComponent<WalkmanController>();
            MiddleBossController middleBossController = collision.GetComponent<MiddleBossController>();
            FinalBossController finalBossController = collision.GetComponent<FinalBossController>();

            if (enemyAttribute != null)
            {
                if (enemycontroller != null)
                {
                    enemyAttribute.TakeDamage(playerAttribute.GetPistolDamage());

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
                    enemyAttribute.TakeDamage(playerAttribute.GetPistolDamage());

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
                    enemyAttribute.TakeDamage(playerAttribute.GetPistolDamage());

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
                    enemyAttribute.TakeDamage(playerAttribute.GetPistolDamage());

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
                        enemyAttribute.TakeDamage(playerAttribute.GetPistolDamage());
                    }

                    if (enemyAttribute.GetHealth() <= 0)
                    {
                        finalBossController.DeathAnimation();
                    }
                }
            }
        }
        else
        {
            return;
        }
    }

    /*IEnumerator DoFlash()
    {
        var renderer = GetComponent<SpriteRenderer>();
        var originalSprite = renderer.sprite;
        renderer.sprite = FlashSprite;

        var framesFlahsed = 0;
        while (framesFlahsed < FramesToFlash)
        {
            framesFlahsed++;
            yield return null;
        }

        renderer.sprite = originalSprite;
    }*/
}
