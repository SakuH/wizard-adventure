using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelTeleporter : MonoBehaviour
{
    public GameObject bonusLevelPrefab;

    private GameObject bonusLevelInstant;

    private bool teleporterActivated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !teleporterActivated)
        {
            bonusLevelInstant = Instantiate(bonusLevelPrefab, new Vector3(0, 202.0f, 0), Quaternion.identity);
            other.GetComponent<Transform>().SetPositionAndRotation(bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("SpawnPoint").gameObject.transform.position, Quaternion.identity);
            bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("BonusRoomEnemyHolder").transform.gameObject.SetActive(true);
            teleporterActivated = true;
            GameObject playerCam = GameObject.FindGameObjectWithTag("MainCamera");
            playerCam.GetComponent<PlayerCamera>().cameraDistance = 1;
            playerCam.GetComponent<PlayerCamera>().cameraHeight = 10;
            
            playerCam.transform.rotation = Quaternion.Euler(20, 0, 0);
        }
        else if (other.CompareTag("Player") && teleporterActivated)
        {
            Destroy(bonusLevelInstant, 2.0f);
        }
    }

}
