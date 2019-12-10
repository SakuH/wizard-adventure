using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private TrailRenderer trailEffect;
    private bool isQuitting;
    public bool hasWeaponEquiped;
    public GameObject player;
    public float movementSpeed = 4;
    private Rigidbody rb;
    private Vector3 mousePos;
    public Camera mainCamera;

    public AudioClip dashSound;

    public int currentTowerFloor = 1;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    public GunController weapon;

    public float slopeForce;
    public float slopeForceRayLength;

    public LayerMask raycastWallLayer;
    public LayerMask raycastGroundLayer;
    public TextMeshProUGUI weaponPickUpText;
    public float raycastToWallLength;
    public float raycastToGroundLength;
    public float raycastOriginPoint;
    public bool grounded;
    public float downForce;

    public float dashDuration;
    public float dashDurationTime;
    public float dashCooldown;
    public float dashCooldownTime;
    public bool isDashing;
    public float dashForce = 10;

    public float speedBoostAmount = 10;
    public float speedBoostDuration;
    public float speedBoostDurationTime = 10;
    public float speedBoostCooldown;
    public float speedBoostCooldownTime = 20;
    public bool  isSpeedBoosting;

    public GameObject[] weaponList;

    private float baseMoveSpeed;

    private AudioSource audioSource;
    private readonly float lowPitchRange = 1.4F;
    private readonly float highPitchRange = 1.8F;
    public float sfxVolume;

    private GameObject mainPlayerHand;
    Vector3 knockbackDirForce;

    public Animator playerAnimator;
    void Awake()
    {

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
        mainPlayerHand = GameObject.Find ("GameObjectHand");
        LoadPlayerData();
        rb = GetComponent<Rigidbody>();
        baseMoveSpeed = movementSpeed;
        setWeaponPickUpText("");
        trailEffect = gameObject.GetComponent<TrailRenderer>();
        trailEffect.enabled = false;
        
    }

    void Update()
    {
        if (player.GetComponent<PlayerHealth>().health > 0)
        {

            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            moveInput.Normalize();
            moveVelocity = moveInput * movementSpeed;


            if (Input.GetMouseButtonDown(0))
            {
                weapon.isFiring = true;

            }
            if (Input.GetMouseButtonUp(0))
            {
                weapon.isFiring = false;

            }
        
            speedBoost();
            dash();
            if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") < 0)
            {
                playerAnimator.SetBool("Walking", true);
            }
            else
            {
                playerAnimator.SetBool("Walking", false);
            }
            if (isDashing || isSpeedBoosting)
            {
                if (trailEffect.enabled == false)
                {
                    trailEffect.enabled = true;
                }
            }
            else
            {
                if (trailEffect.enabled == true)
                {
                    trailEffect.enabled = false;
                }
            }

          
        }
        if (isQuitting)
        {
            if (isSpeedBoosting)
            {
                movementSpeed = baseMoveSpeed;
                isSpeedBoosting = false;
                speedBoostCooldown = 0;
                speedBoostDuration = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (player.GetComponent<PlayerHealth>().health > 0)
        {
            groundCheck();
            wallCheck();
            if (!isDashing && player.GetComponent<PlayerHealth>().knockbackTime <= 0)
            {
                rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
            }

            if (player.GetComponent<PlayerHealth>().knockbackTime > 0)
            {
                rb.velocity = knockbackDirForce;
            }
            wallCheck();
            playerRaycastPointer();

            if (!grounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, -downForce, rb.velocity.z);
            }

        }
    }

    public void wallCheck()
    {
        RaycastHit hit;


      
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.forward, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
            if (moveVelocity.z >= 0)
            {
                moveVelocity.z = 0;
            }
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.right, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
            if (moveVelocity.x >= 0)
            {
                moveVelocity.x = 0;
            }

        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.left, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
            if (moveVelocity.x <= 0)
            {
                moveVelocity.x = 0;
            }
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.back, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
            if (moveVelocity.z <= 0)
            {
                moveVelocity.z = 0;
            }

        }
    }
    public void groundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastToGroundLength, raycastGroundLayer))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }


    public void speedBoost()
    {if (speedBoostCooldown <= 0)
        {
            if (Input.GetKeyDown("space"))
            {
                if (!isSpeedBoosting)
                {
                    movementSpeed += speedBoostAmount;
                    speedBoostDuration = speedBoostDurationTime;
                    isSpeedBoosting = true;
                }

            }
            if (speedBoostDuration > 0)
            {
                speedBoostDuration -= Time.deltaTime;
            }
            else
            {
                if (isSpeedBoosting)
                {
                    movementSpeed = baseMoveSpeed;
                    isSpeedBoosting = false;
                    speedBoostCooldown = speedBoostCooldownTime;
                }

            }
        }else
        {
            speedBoostCooldown -= Time.deltaTime;
        }
    }

   
    public void playerRaycastPointer()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, transform.position);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);


            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    public void dash()
    {
        if (dashCooldown <= 0)
        {
                       
            if (Input.GetMouseButtonDown(1)&& (Input.GetAxisRaw("Horizontal")!=0)|| Input.GetMouseButtonDown(1) && (Input.GetAxisRaw("Vertical") != 0))
            {

                    if (!isDashing)
                    {
 
                        PlayerSound(dashSound);

                        Vector3 dashSpeedVelocity = rb.velocity;
                        dashSpeedVelocity.y = 0;
                        dashSpeedVelocity.x = rb.velocity.x;
                        dashSpeedVelocity.z = rb.velocity.z;


                        if (isSpeedBoosting)
                        {
                            dashSpeedVelocity.x *= speedBoostAmount / baseMoveSpeed;
                            dashSpeedVelocity.z *= speedBoostAmount / baseMoveSpeed;

                        }


                    rb.velocity = dashSpeedVelocity * dashForce;
                        isDashing = true;
                        dashDuration = dashDurationTime;
                    }
                

            }

            if (dashDuration > 0)
            {
                dashDuration -= Time.deltaTime;

            }
            else
            {
                if (isDashing)
                {                                  
                    isDashing = false;
                dashCooldown = dashCooldownTime;

                }
                
            }
        }
        else
        {
            dashCooldown -= Time.deltaTime;
        }
    }
    public void stopDash()
    {
        dashDuration = 0;
    }

    public void setWeaponPickUpText(string text)
    {
        weaponPickUpText.text = text;
    }

    void PlayerSound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Player Movement Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume;
        source.pitch = Random.Range(lowPitchRange, highPitchRange);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }
    public void SavePlayerData(int nextFloorIndex)
    {
        currentTowerFloor = nextFloorIndex;
        SaveSystem.SavePlayer(player);
    }

    public void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            player.GetComponent<PlayerHealth>().maxHealth = data.maxHealth;
            player.GetComponent<PlayerHealth>().health = data.maxHealth;
            currentTowerFloor = data.currentFloor;
            if(data.weaponIsEquiped){
                GameObject spawnedWeapon = weaponList[data.equipedWeapon];

                GameObject weaponInstance = Instantiate(spawnedWeapon, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z + 1f), Quaternion.identity);
                weaponInstance.GetComponent<GunController>().equipedOnSpawn = true;

            }
            else
            {
                GameObject spawnedWeapon = weaponList[0];
                GameObject weaponInstance = Instantiate(spawnedWeapon, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z + 1f), Quaternion.identity);
                weaponInstance.GetComponent<GunController>().equipedOnSpawn = true;
            }
        }

    }

    public void knockback(Vector3 knockbackDir)
    {
        knockbackDirForce = knockbackDir;
      
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }
}
