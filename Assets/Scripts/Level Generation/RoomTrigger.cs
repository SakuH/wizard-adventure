using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public GameObject[] spawns;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            foreach (GameObject spawn in spawns)
            {
                spawn.GetComponent<ObjectGenerator>().SpawnObject();
            }
            Destroy(gameObject);
        }
    }
}
