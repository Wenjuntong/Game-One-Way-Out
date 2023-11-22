using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttribute : MonoBehaviour
{
    [SerializeField] private float enemyDamage = 10f;
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float Health;
    [SerializeField] private int enemyExperience = 0;
    [SerializeField] private float MeleedetectionRange = 10f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float randomVelocity = 3f;
    [SerializeField] private float bulletSpeed = 8f;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float Bossrange = 1.3f;
    [SerializeField] private float Finalattack = 20f;
    [SerializeField] private float immune = 30f;

    private Slider healthBar;


    private void Awake()
    {
        Health = maxHealth;
        healthBar = GetComponentInChildren<Slider>();
        healthBar.maxValue = maxHealth;
        healthBar.value = Health;
    }

    public void IncreaseHealth(float amount)
    {
        Health += amount;
    }

    //increase enemy damage set
    public void IncreaseDamage(float damage)
    {
        enemyDamage += damage;
    }

    //get enemy damage
    public float GetDamage()
    {
        return enemyDamage;
    }

    public void IncreaseExperience(int exp)
    {
        enemyExperience += exp;
    }

    public int GetExperience()
    {
        return enemyExperience;
    }
    //get setection range
    public float GetDetectionRange()
    {
        return MeleedetectionRange;
    }

    public float GetattackCooldown()
    {
        return attackCooldown;
    }

    public float GetRandomVelocity()
    {
        return randomVelocity;
    }

    public float GetbulletSpeed()
    {
        return bulletSpeed;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealth()
    {
        return Health;
    }

    public void TakeDamage(float damageAmount)
    {
        Health -= damageAmount;
        healthBar.value = Health;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetBossRange()
    {
        return Bossrange;
    }

    public float GetFinalAttack()
    {
        return Finalattack;
    }

    public float GetImmune()
    {
        return immune;
    }
}
