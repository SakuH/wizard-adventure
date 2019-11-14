using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float movementSpeed = 4;
    private Rigidbody rb;
    private Vector3 mousePos;
    public Camera mainCamera;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    public GunController weapon;

    public float slopeForce;
    public float slopeForceRayLength;

    public LayerMask raycastWallLayer;
    public LayerMask raycastGroundLayer;
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

    private float baseMoveSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseMoveSpeed = movementSpeed;
       // mainCamera = Camera.main;
    }

    void Update()
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
       /* if(Input.GetKeyDown("e") && weapon.canPickup)
        {
           // isRotating = false;
            
           
           //weapon.isRotating = false;
           //weapon.weaponEquiped = true;
           
           // weapon.weaponEquiped = true;
        }*/
        
        speedBoost();
        dash();
 
    }

    void FixedUpdate()
    {
        groundCheck();
        wallCheck();
        if (!isDashing)
        {
            rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
        }
        wallCheck();
        playerRaycastPointer();

        if (!grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, -downForce, rb.velocity.z);
        }
        // transform.Translate(movementSpeed*Input.GetAxis("Horizontal")*Time.deltaTime,0f, movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        /*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        moveVertical = movementSpeed * moveVertical;
        moveHorizontal = movementSpeed * moveHorizontal;
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement);
        */
    }

    public void wallCheck()
    {
        RaycastHit hit;


        //if (Physics.Raycast(transform.position, Vector3.forward, out hit, raycastToWallLength, raycastWallLayer))
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.forward, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
           // Debug.Log("Front");
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

           // Debug.Log("Right");
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.left, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
            if (moveVelocity.x <= 0)
            {
                moveVelocity.x = 0;
            }
           // Debug.Log("Left");
        }
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - raycastOriginPoint, transform.position.z), Vector3.back, out hit, raycastToWallLength, raycastWallLayer))
        {
            stopDash();
            if (moveVelocity.z <= 0)
            {
                moveVelocity.z = 0;
            }
           // Debug.Log("Back");
        }
    }
    public void groundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, raycastToGroundLength, raycastGroundLayer))
        {
            // Debug.Log("Grounded");
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


           // if (Input.GetMouseButtonDown(1))
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
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

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

            
                //if (Input.GetKeyDown("space"))
                if (Input.GetMouseButtonDown(1)&& (Input.GetAxisRaw("Horizontal")!=0)|| Input.GetMouseButtonDown(1) && (Input.GetAxisRaw("Vertical") != 0))
            {


                

                    if (!isDashing)
                    {
                        Vector3 dashSpeedVelocity = rb.velocity;
                        dashSpeedVelocity.y = 0;
                        dashSpeedVelocity.x = rb.velocity.x;
                        dashSpeedVelocity.z = rb.velocity.z;


                        if (isSpeedBoosting)
                        {
                            dashSpeedVelocity.x *= speedBoostAmount / baseMoveSpeed;
                            dashSpeedVelocity.z *= speedBoostAmount / baseMoveSpeed;

                        }
                        /*
                        if(dashSpeedVelocity.x> 0)
                        {
                            dashSpeedVelocity.x = baseMoveSpeed;
                        }else if (dashSpeedVelocity.x < 0)
                        {
                            dashSpeedVelocity.x = -baseMoveSpeed;
                        }

                        if (dashSpeedVelocity.z > 0)
                        {
                            dashSpeedVelocity.z = baseMoveSpeed;           
                        }else if(dashSpeedVelocity.z < 0)
                        {
                            dashSpeedVelocity.z = -baseMoveSpeed;
                        }
                        */

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
}
