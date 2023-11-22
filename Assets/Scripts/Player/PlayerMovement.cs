using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float dashCoolDown = 1f;
    [SerializeField] private GameObject iceZoneEffect;
    [SerializeField] private GameObject ExplosionEffect;

    [HideInInspector] public Vector3 movement;
    [HideInInspector] public Vector2 movementInput;
    [HideInInspector] public GameObject equipedObject;

    private ParticleSystem skillEffect;

    private Camera mainCamera;
    private Vector3 mousePosition;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private PlayerAttribute playerAttribute;

    private float speed;
    private bool canDash = true;
    private bool isDashing = false;
    private float dashTime = 0.2f;
    private float dashingPower = 15f;
    private bool inLight;

    // Start is called before the first frame update
    void Start()
    {
        playerAttribute = gameObject.GetComponent<PlayerAttribute>();
        speed = playerAttribute.GetSpeed();
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        equipedObject = transform.GetChild(1).GetChild(0).gameObject;

        EquipTorch();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash && playerAttribute.GetCurrentEndurance()>0)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown("1"))
        {
            EquipGun();
        }
        if (Input.GetKeyDown("2"))
        {
            EquipTorch();
        }
        if (Input.GetKeyDown("3") && transform.GetChild(2).childCount > 0)
        {
            EquipSnowBall();
        }
        if(Input.GetKeyDown("4") && transform.GetChild(3).childCount > 0)
        {
            //Equip bombs
            EquipBomb();
        }

        CheckEquipped();

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movementInput = new Vector2(inputX, inputY);

        movement = new Vector3(inputX * speed, inputY * speed, 0f);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        //rigidbody2d.velocity = movementInput.normalized * speed;
        rigidbody2d.MovePosition(transform.position + movement * Time.deltaTime);

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (movementInput.x != 0 || movementInput.y != 0)
        {
            animator.SetBool("isWalking", true);
            //walk right or left
            if (mousePosition.x >= transform.position.x)
            {
                //animator.SetFloat("LookingDirection", 1);
                animator.SetFloat("MoveX", 0);
            }
            else
            {
                //animator.SetFloat("LookingDirection", 0);
                animator.SetFloat("MoveX", 1);
            }
        }
        else
        {
            animator.SetBool("isWalking", false);
            //idle right or left
            if (mousePosition.x >= transform.position.x)
            {
                animator.SetFloat("LookDirection", 1);
            }
            else
            {
                animator.SetFloat("LookDirection", 0);
            }
        }
    }
    private IEnumerator Dash()
    {
        // start the coroutine then dash the player, use the field named dashing power to determine the dash distance
        playerAttribute.ChangeEndurance(-1);
        canDash = false;
        isDashing = true;
        rigidbody2d.velocity = new Vector2(movementInput.normalized.x * dashingPower, movementInput.normalized.y * dashingPower);

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        canDash = true;
    }

    public void SetEquipped(GameObject equipped)
    {
        equipedObject.SetActive(false);
        equipedObject = equipped;
        equipedObject.SetActive(true);
    }

    public void ResetEquipped()
    {
        if(equipedObject != null)
        {
            equipedObject.SetActive(false);
        }
        equipedObject = transform.GetChild(0).gameObject;
        equipedObject.SetActive(true);
    }

    public void EquipGun()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        equipedObject = transform.GetChild(0).gameObject;
    }

    public void EquipTorch()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        equipedObject = transform.GetChild(1).gameObject;
    }

    public void EquipSnowBall()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(false);
        equipedObject = transform.GetChild(2).gameObject;
    }

    public void EquipBomb()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
        equipedObject = transform.GetChild(3).gameObject;
    }

    //get player's isInLight
    public bool GetInLight()
    {
        return inLight;
    }

    //set player's isInLight
    public void SetInLight(bool isInLight)
    {
        this.inLight = isInLight;
    }

    private void CheckEquipped()
    {
        if (equipedObject != null)
        {
            if (equipedObject.CompareTag("Torch"))
            {
                transform.GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(4).gameObject.SetActive(false);
            }

            if (equipedObject.CompareTag("Snowball"))
            {
                PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();

                if (Input.GetMouseButtonDown(0) && inventory.SnowBallNum > 0)
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 iceZonePosition = new Vector2(mousePosition.x, mousePosition.y);
                    GameObject iceZone = Instantiate(iceZoneEffect, iceZonePosition, Quaternion.identity);
                    skillEffect = iceZone.transform.GetChild(0).GetComponent<ParticleSystem>();
                    skillEffect.Play();

                    inventory.SnowBallNum -= 1;
                    
                    Destroy(transform.GetChild(2).GetChild(0).gameObject);
                    Destroy(iceZone, 1f);
                }

            }

            if (equipedObject.CompareTag("Bomb"))
            {
                PlayerInventory inventory = gameObject.GetComponent<PlayerInventory>();

                if (Input.GetMouseButtonDown(0) && inventory.BombNum > 0)
                {
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 bombPosition = new Vector2(mousePosition.x, mousePosition.y);
                    skillEffect = Instantiate(ExplosionEffect, bombPosition, Quaternion.identity).transform.GetChild(0).GetComponent<ParticleSystem>();
                    skillEffect.Play();

                    inventory.BombNum -= 1;

                    Destroy(transform.GetChild(3).GetChild(0).gameObject);
                    Destroy(skillEffect, 1f);
                }

            }

            if (equipedObject.CompareTag("Torch") || inLight)
            {
                GetComponent<PlayerAttribute>().SetIsInLight(true);
            }
            else
            {
                GetComponent<PlayerAttribute>().SetIsInLight(false);
            }
        }
    }
}
