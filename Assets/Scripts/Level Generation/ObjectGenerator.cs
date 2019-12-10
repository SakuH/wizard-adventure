using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject parentRoom;
    public GameObject[] objects;

    public bool isPartOfRoom = true;

    private RoomInternalBehaviour roomBehaviour;

    void Start()
    {
        if (isPartOfRoom)
        {
            roomBehaviour = GetComponentInParent<RoomInternalBehaviour>();
        }
    }

    public void SpawnObject()
    {
        int rand = Random.Range(0, objects.Length);
        
        if (isPartOfRoom)
        {
            if (objects[rand].CompareTag("Enemy"))
            {
                roomBehaviour.AddEnemyToRoom(Instantiate(objects[rand], transform.position, Quaternion.identity));
            }
            else if (objects[rand].CompareTag("Door"))
            {
                roomBehaviour.AddDoorToRoom(Instantiate(objects[rand], transform.position, objects[rand].transform.rotation));
            }
            else if(objects[rand].CompareTag("Weapon"))
            {   
                GameObject random = objects[rand];
                Instantiate(random, transform.position, Quaternion.identity);
               
            }
            else
            {
                Instantiate(objects[rand], transform.position, Quaternion.identity);
            }
        }
        else
        {
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 4f);
    }
}
