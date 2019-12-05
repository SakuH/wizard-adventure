using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevelDoor : MonoBehaviour
{
    public GameObject enemies;
    public PlayerCamera playerCameraScript;
    public GameObject teleportLocation;
    // Start is called before the first frame update
    void Start()
    {
        playerCameraScript = GameObject.FindGameObjectWithTag("PlayerCamera").GetComponent<PlayerCamera>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = teleportLocation.transform.position;
            Destroy(enemies, 2);
            playerCameraScript.cameraDistance = 4;
            playerCameraScript.cameraHeight = 35;
            GameObject playerCam = GameObject.FindGameObjectWithTag("PlayerCamera");
            playerCam.transform.rotation =  Quaternion.Euler(70, 0, 0);

        }
    }
}
