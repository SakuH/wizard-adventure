using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    private ObjectGenerator[] objectGenerators;
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            objectGenerators = gameObject.GetComponentsInChildren<ObjectGenerator>();

            foreach (ObjectGenerator spawnObject in objectGenerators)
            {
                spawnObject.SpawnObject();
            }
            Destroy(gameObject);
        }
    }
}
