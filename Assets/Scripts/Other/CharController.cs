using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject[] Characters;

    // Start is called before the first frame update
    void Start()
    {
        int num = SkinManager.selectedSkin;
        Instantiate(Characters[num],new Vector3(0,0,0),Quaternion.identity);
    }
}
