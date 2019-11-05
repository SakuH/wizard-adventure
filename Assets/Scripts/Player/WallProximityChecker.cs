using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallProximityChecker : MonoBehaviour
{
    
    private bool touchingWall;
    public LayerMask raycastWallLayer;
    public PlayerMovement player;
    public float raycastToWallLength;
    void Update()
    {
        raycastToWallLength = player.raycastToWallLength;

    }
    void FixedUpdate()
    {
        
        wallCheck();
    }
    public void wallCheck()
    {
        RaycastHit hit;


        if (Physics.Raycast(transform.position, Vector3.forward, out hit, raycastToWallLength, raycastWallLayer))
        {
            Debug.Log("1");
        }
        if (Physics.Raycast(transform.position, Vector3.right, out hit, raycastToWallLength, raycastWallLayer))
        {
            Debug.Log("2");
        }
        if (Physics.Raycast(transform.position, Vector3.left, out hit, raycastToWallLength, raycastWallLayer))
        {
            Debug.Log("3");
        }
        if (Physics.Raycast(transform.position, Vector3.back, out hit, raycastToWallLength, raycastWallLayer))
        {
            Debug.Log("4");
        }
    }


}
