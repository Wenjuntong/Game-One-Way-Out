using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttribute : MonoBehaviour
{
    public int level = 0;
    public int exp = 0;

    [SerializeField] private Canvas PlayerCanvas;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private int maxEndurance = 5;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float critRate = 0.1f;
    [SerializeField] private float damage = 10f;

    public bool isInLight = false;
    private PlayerHealthBar healthBar;
    private RectTransform healthBarRectTransform;
    private PlayerExpBar expBar;
    private RectTransform expBarRectTransform;
    private PlayerEnduranceBar enduranceBar;
    private RectTransform enduranceBarRectTransform;

    private float enduranceTimer = 5f;
    private float notInLightTimer = 1f;
    private float inLightTimer = 1f;

    public float currentHealth;
    public int currentEndurance;
    private bool isDying = false;
    [HideInInspector] public bool isDead = false;
    private Animator animator;
    private Vector3 mousePosition;
    private Camera mainCamera;
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    private void Start()
    {
        Instantiate(PlayerCanvas);
        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<PlayerHealthBar>();
        healthBarRectTransform = healthBar.GetComponent<RectTransform>();
        expBar = GameObject.Find("PlayerExpBar").GetComponent<PlayerExpBar>();
        expBarRectTransform = expBar.GetComponent<RectTransform>();
        enduranceBar = GameObject.Find("PlayerEnduranceBar").GetComponent<PlayerEnduranceBar>();
        enduranceBarRectTransform = enduranceBar.GetComponent<RectTransform>();

        currentHealth = maxHealth;
        exp = 0;
        currentEndurance = maxEndurance;

        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        expBar.SetMaxExp(100);
        expBar.SetExp(this.exp);

        enduranceBar.SetMaxEndurance(maxEndurance);
        enduranceBar.SetEndurance(currentEndurance);

        healthBarRectTransform.sizeDelta = new Vector2(maxHealth,15f);
        expBarRectTransform.sizeDelta = new Vector2(100, 15f);
        enduranceBarRectTransform.sizeDelta = new Vector2(100, 15f);

        animator = GetComponent<Animator>();

        inLightTimer = 1f;
        notInLightTimer = 1f;
    }

    private void Update()
    {
        //if player is death
        if (gameObject.GetComponent<PlayerAttribute>().GetCurrentHealth() <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        if (isDead)
        {
            if (isDying)
            {
                return;
            }
            else
            {
                isDying = true;
                StartCoroutine(Death());
            }
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        //when player exp reaches max
        if (exp >= 40)
        {
            //level up
            LevelUp();
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }



    }

    private void FixedUpdate()
    {
        enduranceTimer -= Time.deltaTime;

        if (enduranceTimer <= 0)
        {
            enduranceTimer = 5f;
            if (currentEndurance < maxEndurance)
            {
                ChangeEndurance(1);
            }
        }

        if (isInLight)
        {
            inLightTimer -= Time.deltaTime;
            if(inLightTimer <= 0)
            {
                if (currentHealth < maxHealth)
                {
                    currentHealth += 2f;
                    healthBar.SetHealth(currentHealth);
                    inLightTimer = 1f;
                }
            }
        }
        else
        {
            notInLightTimer -= Time.deltaTime;
            if (notInLightTimer <= 0)
            {
                currentHealth -= 2f;
                healthBar.SetHealth(currentHealth);
                notInLightTimer = 1f;
            }
        }
    }

    //change player's speed
    public void ChangeSpeed(float speed)
    {
        this.speed = speed;
    }
    
    //get player's speed
    public float GetSpeed()
    {
        return speed;
    }

    public void SpeedUp()
    {
        this.speed += 1f;
    }

    //set player's health
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }

    //get player's health
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    //health up
    public void HealthUp()
    {
        maxHealth *= 1.1f;
        //change healthbar's width in rect transform
        healthBarRectTransform.sizeDelta = new Vector2(maxHealth, 12.7088f);
        currentHealth *= 1.1f;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);

    }

    //level up
    public void LevelUp()
    {
        level++;
        exp = 0;
        expBar.SetExp(this.exp);

        PlayerInventory playerInventory = GetComponent<PlayerInventory>();
        //increase talent points by 1
        playerInventory.TalentPoint += 1;
    }

    //increase player's exp
    public void IncreaseExp(int exp)
    {
        this.exp += exp;
        expBar.SetExp(this.exp);
    }

    //get player's level
    public int GetLevel()
    {
        return level;
    }

    //take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void TrapDamage(float damage)
    {
        if (damage > 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    //get player's isInLight
    public bool GetIsInLight()
    {
        return isInLight;
    }

    //set player's isInLight
    public void SetIsInLight(bool isInLight)
    {
        this.isInLight = isInLight;
    }

    public void DamageUp()
    {
        this.damage *= 1.3f;
    }

    //get pistol damage
    public float GetPistolDamage()
    {
        return this.damage * 3f;
    }

    //get shotgun damage
    public float GetShotgunDamage()
    {
        return damage;
    }

    //get rifle damage
    public float GetRifleDamage()
    {
        return damage/2;
    }

    //consume current strength
    public void ChangeEndurance(int strength)
    {
        currentEndurance += strength;
        enduranceBar.SetEndurance(currentEndurance);
    }

    public void MaxEnduranceUp()
    {
        maxEndurance += 1;
        //change healthbar's width in rect transform
        enduranceBarRectTransform.sizeDelta = new Vector2(maxEndurance * 20, 12.7088f);
        healthBar.SetMaxHealth(maxEndurance);
    }

    //get current strength
    public int GetCurrentEndurance()
    {
        return currentEndurance;
    }

    IEnumerator Death()
    {
        //if player is death, play death animation
        if (mousePosition.x >= transform.position.x)
        {
            animator.SetFloat("DeathDirection", 1);
        }
        else
        {
            animator.SetFloat("DeathDirection", 0);
        }
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.Play("Death");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        
        yield return new WaitForSeconds(3);

        //Destroy(gameObject);
        GameObject playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        GameController gameController = playerCanvas.GetComponent<GameController>();
        gameController.EnableRestartCanvas();
        //load a new scene
        //SceneManager.LoadScene("StartMenu");
    }
}
