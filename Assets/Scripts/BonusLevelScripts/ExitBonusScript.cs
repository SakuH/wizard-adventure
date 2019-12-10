using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitBonusScript : MonoBehaviour
{
    public string nextScene;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {

            other.GetComponent<Transform>().SetPositionAndRotation(GameObject.Find("BonusLevelTeleporter").transform.position, Quaternion.identity);

        }
    }
}
