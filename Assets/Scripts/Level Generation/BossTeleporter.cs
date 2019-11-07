﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleporter : MonoBehaviour
{
    public void ActivateTeleporter()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Transform>().SetPositionAndRotation(new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
    }
}