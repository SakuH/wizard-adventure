using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelTeleporter : MonoBehaviour
{
    public GameObject bonusLevelPrefab;


    private bool teleporterActivated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !teleporterActivated)
        {
            GameObject bonusLevelInstant = bonusLevelPrefab;
            Instantiate(bonusLevelInstant, new Vector3(0, 202.0f, 20), Quaternion.identity);
            //other.GetComponent<Transform>().SetPositionAndRotation(new Vector3(0, 202.0f, 0), Quaternion.identity);
            other.GetComponent<Transform>().SetPositionAndRotation(bonusLevelInstant.transform.Find("SpawnPoint").transform.position, Quaternion.identity);
            bonusLevelInstant.transform.Find("BonusRoomEnemyHolder").gameObject.SetActive(true);
            //other.GetComponent<PlayerMovement>().SavePlayerData(2);
            teleporterActivated = true;
        }
    }
}
