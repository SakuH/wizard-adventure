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

    public LayerMask raycastLayerToHit;
    public float raycastToWallLength;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    void Update()
    {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f , Input.GetAxisRaw("Vertical"));
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

       




    }

    void FixedUpdate()
    {

        wallCheck();
        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y ,moveVelocity.z);
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);


            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
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
       

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, raycastToWallLength, raycastLayerToHit))
        {
            Debug.Log("Front");
            if (moveVelocity.z >= 0)
            {
                moveVelocity.z = 0;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, raycastToWallLength, raycastLayerToHit))
        {
            if (moveVelocity.x >= 0)
            {
                moveVelocity.x = 0;
            }
            Debug.Log("Right");
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, raycastToWallLength, raycastLayerToHit))
        {
            if (moveVelocity.x <= 0)
            {
                moveVelocity.x = 0;
            }
            Debug.Log("Left");
        }
        if (Physics.Raycast(transform.position, Vector3.back, out hit, raycastToWallLength, raycastLayerToHit))
        {
            if (moveVelocity.z <= 0)
            {
                moveVelocity.z = 0;
            }
            Debug.Log("Back");
        }
    }
  
}
