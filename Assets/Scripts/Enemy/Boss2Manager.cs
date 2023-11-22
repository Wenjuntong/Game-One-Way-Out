using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Manager : MonoBehaviour
{
    [SerializeField] private GameObject suski;

    private GameObject brazier;
    private FireLight fireLight;
    private bool hasSuski = false;

    private void Start()
    {
        brazier = GameObject.Find("PF Props Well");
        fireLight = brazier.GetComponent<FireLight>();
    }

    public void Boss2()
    {
        if(fireLight.GetIsOnFire() && !hasSuski)
        {
            suski.SetActive(true);
        }
    }
}
