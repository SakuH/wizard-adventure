using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;

    public GameObject[] bottomRoomsAfterMin;
    public GameObject[] topRoomsAfterMin;
    public GameObject[] leftRoomsAfterMin;
    public GameObject[] rightRoomsAfterMin;

    public GameObject bottomRoomDeadEnd;
    public GameObject topRoomDeadEnd;
    public GameObject leftRoomDeadEnd;
    public GameObject rightRoomDeadEnd;

    public GameObject closedRoom;

    public List<GameObject> rooms;

    public int minRoomAmount;
    public int maxRoomAmount;
    public float waitTime;
    public bool minRoomAmountReached = false;
    public bool maxRoomAmountReached = false;
    private bool spawnedTeleporter;
    public GameObject bossTeleporter;

    private void Update()
    {
        if(waitTime <= 0 && spawnedTeleporter == false)
        {
            rooms[rooms.Count - 1].GetComponent<RoomInternalBehaviour>().SetAsTeleporterRoom();
            rooms[Random.Range(1, rooms.Count - 2)].GetComponent<RoomInternalBehaviour>().SetAsBonusLevelRoom();
            spawnedTeleporter = true;
        }
        else if(waitTime >= 0)
        {
            waitTime -= Time.deltaTime;
        }
        if(!minRoomAmountReached && rooms.Count > minRoomAmount)
        {

            minRoomAmountReached = true;
        }
        if(!maxRoomAmountReached && rooms.Count > maxRoomAmount)
        {

            maxRoomAmountReached = true;
        }
    }
}
