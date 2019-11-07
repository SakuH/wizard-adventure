using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{

    public float speed = 5f;
    public float height = 0.5f;
    Vector3 pos;
    public bool isRotating;
    // Start is called before the first frame update
    void Start()
    {
         pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ( isRotating){

        float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
        transform.position = new Vector3(pos.x,newY,pos.z) * height;
        transform.Rotate(new Vector3(0,30,45)*Time.deltaTime);

        }
    }
}
