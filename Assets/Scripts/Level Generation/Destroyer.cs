using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoomSpawnPoint"))
        {
            other.GetComponent<RoomSpawner>().SetAsSpawned();
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RoomSpawnPoint"))
        {
            other.GetComponent<RoomSpawner>().SetAsSpawned();
            Destroy(other.gameObject);
        }
    }
}
