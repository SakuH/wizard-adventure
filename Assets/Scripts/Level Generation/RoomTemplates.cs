﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedTeleporter;
    public GameObject bossTeleporter;

    private void Update()
    {
        if(waitTime <= 0 && spawnedTeleporter == false)
        {

            //Instantiate(bossTeleporter, rooms[rooms.Count - 1].transform.position + new Vector3(0,2), Quaternion.identity);
            rooms[rooms.Count - 1].GetComponent<RoomInternalBehaviour>().SetAsTeleporterRoom();
            spawnedTeleporter = true;
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
