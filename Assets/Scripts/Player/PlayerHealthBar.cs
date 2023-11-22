using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }
    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }
}
