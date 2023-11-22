using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour,IInventory
{
    private int money = 0;
    private int snowBallNum = 0;
    private int bombNum = 0;


    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] private GameObject bombPrefab;

    [SerializeField] private GameObject PistolPrefab;
    [SerializeField] private GameObject ShotgunPrefab;
    [SerializeField] private GameObject RiflePrefab;


    public int Money { get => money; set => money = value; }
    public int SnowBallNum { get => snowBallNum; set => snowBallNum = value; }
    public int BombNum { get => bombNum; set => bombNum = value; }


    //talent point with getter and setter
    public int talentPoint = 0;
    public int TalentPoint { get => talentPoint; set => talentPoint = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update(){}

    public void EquipSnowBall()
    {
        //add sand ball
        var newSnowball = Instantiate(snowballPrefab, gameObject.transform.GetChild(2));
        newSnowball.transform.parent = gameObject.transform.GetChild(2);
        newSnowball.transform.localScale = new Vector3(10,10,0);
    }

    public void EquipBomb()
    {
        var newBomb = Instantiate(bombPrefab, gameObject.transform.GetChild(3));
        newBomb.transform.parent = gameObject.transform.GetChild(3);
        newBomb.transform.localScale = new Vector3(10, 10, 0);
    }

    public void UpdateWeapon(GameObject newWeapon)
    {
        if (newWeapon.CompareTag("Pistol"))
        {
            foreach (Transform child in gameObject.transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }

            var newPistol = Instantiate(PistolPrefab, gameObject.transform.GetChild(0));
            newPistol.transform.parent = gameObject.transform.GetChild(0);
            newPistol.transform.localScale= new Vector3(1,1,0);
            newPistol.GetComponent<PistolMovement>().enabled = true;
        }

        if (newWeapon.CompareTag("Shotgun"))
        {
            foreach (Transform child in gameObject.transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }

            var newShotgun = Instantiate(ShotgunPrefab, gameObject.transform.GetChild(0));
            newShotgun.transform.parent = gameObject.transform.GetChild(0);
            newShotgun.transform.localScale = new Vector3(1, 1, 0);
            newShotgun.GetComponent<ShotgunMovement>().enabled = true;
        }
        if (newWeapon.CompareTag("Rifle"))
        {
            foreach (Transform child in gameObject.transform.GetChild(0))
            {
                Destroy(child.gameObject);
            }

            var newRifle = Instantiate(RiflePrefab, gameObject.transform.GetChild(0));
            newRifle.transform.parent = gameObject.transform.GetChild(0);
            newRifle.transform.localScale = new Vector3(1, 1, 0);
            newRifle.GetComponent<RifleMovement>().enabled = true;
        }
    }
}
