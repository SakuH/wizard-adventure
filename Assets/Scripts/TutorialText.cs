using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialText : MonoBehaviour
{

private PlayerMovement playerMovement;

private GameObject player;

public string text;



    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }



    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            playerMovement.setWeaponPickUpText(text);
        }

    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            playerMovement.setWeaponPickUpText("");
        }

    }
}
