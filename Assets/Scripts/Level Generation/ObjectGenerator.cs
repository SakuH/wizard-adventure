﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject parentRoom;
    public GameObject[] objects;

    private RoomInternalBehaviour roomBehaviour;

    void Start()
    {
        roomBehaviour = GetComponentInParent<RoomInternalBehaviour>();
    }

    public void SpawnObject()
    {
        int rand = Random.Range(0, objects.Length);
        if (objects[rand].CompareTag("Enemy"))
        {
            roomBehaviour.AddEnemyToRoom(Instantiate(objects[rand], transform.position, Quaternion.identity));
        }
        else if (objects[rand].CompareTag("Door"))
        {
            roomBehaviour.AddDoorToRoom(Instantiate(objects[rand], transform.position, Quaternion.identity));
        }
        else
        {
            Instantiate(objects[rand], transform.position, Quaternion.identity);
        }
        Destroy(gameObject, 4f);
    }
}
