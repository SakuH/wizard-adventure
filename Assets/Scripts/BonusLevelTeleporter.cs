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
            GameObject bonusLevelInstant = Instantiate(bonusLevelPrefab, new Vector3(0, 202.0f, 0), Quaternion.identity);
            //StartCoroutine(SomeCoroutine(other, bonusLevelInstant));
            //other.GetComponent<Transform>().SetPositionAndRotation(new Vector3(0.0f, 174.0f, -478.0f), Quaternion.identity);
            other.GetComponent<Transform>().SetPositionAndRotation(bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("SpawnPoint").gameObject.transform.position, Quaternion.identity);
            //other.GetComponent<Transform>().SetPositionAndRotation(bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("SpawnPoint").transform.TransformPoint(Vector3.zero), Quaternion.identity);
            bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("BonusRoomEnemyHolder").transform.gameObject.SetActive(true);
            //other.GetComponent<PlayerMovement>().SavePlayerData(2);
            teleporterActivated = true;
        }
    }


  /*  private IEnumerator SomeCoroutine(Collider other, GameObject bonusLevelInstant)
    {
       
        
        yield return new WaitForSeconds(3);
        other.GetComponent<Transform>().SetPositionAndRotation(bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("SpawnPoint").transform.TransformPoint(Vector3.zero), Quaternion.identity);
        yield return new WaitForSeconds(1);
        bonusLevelInstant.transform.Find("HallwayHolder").transform.Find("BonusRoomEnemyHolder").gameObject.SetActive(true);
    }
    */
}
