using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletspeed;
    public float setX;
    public float setY;
    public float setZ;
    public GameObject bulletImpact;

    public int setDamage;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,5);
    }

    // Update is called once per frame
    void Update()
    {
       // transform.Translate(Vector3.forward * bulletspeed * Time.deltaTime);
        transform.Translate(new Vector3(setX,setY,setZ) * bulletspeed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("Bullet Destroyed");
            Instantiate(bulletImpact, transform.position, Quaternion.identity);        
            Destroy(gameObject);
        }
       
    }
}
