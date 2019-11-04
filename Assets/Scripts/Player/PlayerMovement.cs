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

}
