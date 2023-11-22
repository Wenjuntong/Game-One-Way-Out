using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PistolMovement : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float startTimeBtwShots;
    [SerializeField] private ParticleSystem muzzleFlash;

    private SpriteRenderer spriteRenderer;
    private float timeBtwShots;
    private GameObject newBullet;
    private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firePoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(1);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        if(rotationZ < 89 && rotationZ > -89)
        {
            spriteRenderer.flipY = false;
            //transform.localScale = new Vector3(1,1,0);
            //spriteRenderer.flipY = false;
            //firePoint.position = transform.position + new Vector3(5.4f, 1, 0);
        }
        else
        {
            spriteRenderer.flipY = true;
            //transform.localScale = new Vector3(1, -1, 0);
            //muzzleFlash.main.flip = new Vector3(0, 0, 0);
            //firePoint.position = transform.position + new Vector3(4.5f, -1, 0);
        }

        //shoot
        shoot();
    }

    void shoot() {
        if(timeBtwShots <= 0)
        {
            if (Input.GetButton("Fire1"))
            {
                muzzleFlash.Play();
                Instantiate(bulletPrefab, firePoint.position, transform.rotation);
                //newBullet.transform.parent = transform.GetChild(0);
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                return;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
