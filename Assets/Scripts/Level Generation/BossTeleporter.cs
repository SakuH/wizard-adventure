using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleporter : MonoBehaviour
{

    public GameObject enemyBoss;
    public void ActivateTeleporter()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(enemyBoss, new Vector3(0, 2.0f, 1045), Quaternion.identity);
            other.GetComponent<Transform>().SetPositionAndRotation(new Vector3(0, 2.0f, 1025), Quaternion.identity);
            other.GetComponent<PlayerMovement>().SavePlayerData(2);
        }
    }
}
