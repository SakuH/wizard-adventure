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
            GetComponentInParent<RoomInternalBehaviour>().TurnOnRoomLights();
            objectGenerators = gameObject.GetComponentsInChildren<ObjectGenerator>();

            foreach (ObjectGenerator spawnPoint in objectGenerators)
            {
                spawnPoint.SpawnObject();
            }
            Destroy(gameObject);
        }
    }
}
