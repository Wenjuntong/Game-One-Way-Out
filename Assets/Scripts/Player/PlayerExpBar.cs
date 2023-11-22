using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExpBar : MonoBehaviour
{
    [SerializeField] private Slider ExpBar;

    public void SetExp(int exp)
    {
        ExpBar.value = exp;
    }
    public void SetMaxExp(int maxExp)
    {
        ExpBar.maxValue = maxExp;
    }
}
