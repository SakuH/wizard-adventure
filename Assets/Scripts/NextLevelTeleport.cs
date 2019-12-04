using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTeleport : MonoBehaviour
{
    public int nextFloorNumber;
    public void ActivateTeleporter()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SavePlayerData(nextFloorNumber);
            other.GetComponent<PlayerMovement>().currentTowerFloor = nextFloorNumber;
            GameObject.FindGameObjectWithTag("Player").GetComponent<GameStats>().SaveStats();
            GameObject.Find("LevelChanger").GetComponent<LevelChanger>().FadeToScene(nextFloorNumber);
            //SceneManager.LoadScene(nextFloorNumber);

        }
    }
}
