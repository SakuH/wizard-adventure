using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    Camera cam;

    
    Vector3 target, mousePos, refVel;
    public float cameraDistance = 3.5f;
    public float smoothTime = 0.2f, yStart;
    public float cameraZOffset;
    public float cameraXOffset;
    public float cameraHeight;
    public float max;

  
    void Start()
    {
        target = player.transform.position;
        yStart = player.transform.position.z;
        player = GameObject.Find("Player");

       // offset = transform.position - player.transform.position;

    }
    void Update()
    {
        mousePos = captureMousePos();
        target = updateTargetPos();
       
        
    }

 
    void LateUpdate()
    {
       //  transform.position = player.transform.position + offset;
      updateCameraPosition();
    }
    /*
    Vector3 captureMousePos()
    {
        Vector3 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log("x; " + ret.x + "y; " + ret.y + "z; " + ret.z);
        ret *= 2;  
        ret -= Vector3.one;

        if(Mathf.Abs(ret.x) > max || Mathf.Abs(ret.z) > max) {
            ret = ret.normalized;

        }
        
        return ret;
    }

    Vector3 updateTargetPos() 
    {
        Vector3 mouseOFfset = mousePos * cameraDistance;
        Vector3 ret = player.transform.position + mouseOFfset;
        ret.y = cameraHeight;
        ret.x += cameraXOffset;
        ret.z += cameraZOffset;

        return ret;
    }
    void updateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }
    */
    Vector3 captureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
       
        ret *= 2;
        ret -= Vector2.one;

        //if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        //{
         //   ret = ret.normalized;

       // }

        Vector3 ret2;
        ret2.x = ret.x;
        ret2.y = 0;
        ret2.z = ret.y;

        return ret2;
    }

    Vector3 updateTargetPos()
    {
        Vector3 mouseOFfset = mousePos * cameraDistance;
        Vector3 ret = player.transform.position + mouseOFfset;
        ret.y = cameraHeight;
        ret.x += cameraXOffset;
        ret.z += cameraZOffset;

        return ret;
    }

    void updateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }
}
