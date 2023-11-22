using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnduranceBar : MonoBehaviour
{
    [SerializeField] private Slider EnduranceBar;

    public void SetEndurance(int endurance)
    {
        EnduranceBar.value = endurance;
    }
    public void SetMaxEndurance(int maxEndurance)
    {
        EnduranceBar.maxValue = maxEndurance;
    }
}
