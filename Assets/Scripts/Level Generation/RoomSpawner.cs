﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    //1 --> need bottom door
    //2 --> need top door
    //3 --> need left door
    //4 --> need right door

    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f;
    private float extraSpawnTime = 0.0f;
    void Start()
    {
        switch (openingDirection)
        {
            case 1:
                extraSpawnTime = 0.01f;
                break;
            case 2:
                extraSpawnTime = 0.06f;
                break;
            case 3:
                extraSpawnTime = 0.07f;
                break;
            case 4:
                extraSpawnTime = 0.03f;
                break;

            default:
                break;
        }
        Destroy(gameObject, waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.2f + extraSpawnTime);
    }

    void Spawn()
    {
        if (!spawned)
        {
            if (!templates.minRoomAmountReached)
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, templates.bottomRooms.Length);
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    rand = Random.Range(0, templates.topRooms.Length);
                    Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    rand = Random.Range(0, templates.leftRooms.Length);
                    Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                }
                else if (openingDirection == 4)
                {
                    rand = Random.Range(0, templates.rightRooms.Length);
                    Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                }
            }
            else if (templates.minRoomAmountReached && !templates.maxRoomAmountReached)
            {
                if (openingDirection == 1)
                {
                    rand = Random.Range(0, templates.bottomRoomsAfterMin.Length);
                    Instantiate(templates.bottomRoomsAfterMin[rand], transform.position, templates.bottomRoomsAfterMin[rand].transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    rand = Random.Range(0, templates.topRoomsAfterMin.Length);
                    Instantiate(templates.topRoomsAfterMin[rand], transform.position, templates.topRoomsAfterMin[rand].transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    rand = Random.Range(0, templates.leftRoomsAfterMin.Length);
                    Instantiate(templates.leftRoomsAfterMin[rand], transform.position, templates.leftRoomsAfterMin[rand].transform.rotation);
                }
                else if (openingDirection == 4)
                {
                    rand = Random.Range(0, templates.rightRoomsAfterMin.Length);
                    Instantiate(templates.rightRoomsAfterMin[rand], transform.position, templates.rightRoomsAfterMin[rand].transform.rotation);
                }
            }
            else if (templates.maxRoomAmountReached)
            {
                if (openingDirection == 1)
                {
                    Instantiate(templates.bottomRoomDeadEnd, transform.position, templates.bottomRoomDeadEnd.transform.rotation);
                }
                else if (openingDirection == 2)
                {
                    Instantiate(templates.topRoomDeadEnd, transform.position, templates.topRoomDeadEnd.transform.rotation);
                }
                else if (openingDirection == 3)
                {
                    Instantiate(templates.leftRoomDeadEnd, transform.position, templates.leftRoomDeadEnd.transform.rotation);
                }
                else if (openingDirection == 4)
                {
                    Instantiate(templates.rightRoomDeadEnd, transform.position, templates.rightRoomDeadEnd.transform.rotation);
                }
            }
            spawned = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
       if (other.CompareTag("RoomSpawnPoint")){

            //if (other.GetComponent<Destroyer>() != null)
            //{
            //    Debug.Log("spawn point met destroyer");
            //    Destroy(gameObject);
            //}
            //else
            //{

                try
                {
                    
                    if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
                    {
                        
                        if (gameObject.transform.position != new Vector3(0, 0, 0))
                        {
                            Debug.Log("instantiating a closed room");
                            Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                            Destroy(gameObject);
                            
                        }
                    }
                }
                catch
                {
                    //Destroy(gameObject);
                }


            }
            spawned = true;
        //}
    }


}
