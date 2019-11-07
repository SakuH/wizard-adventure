using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAsPartOfRoom : MonoBehaviour
{

    bool isAlive = true;
    void Start()
    {
        Invoke("MarkAsDead", 3f);
    
    }


    void Update()
    {
        
    }

    void MarkAsDead()
    {
        isAlive = false;
    }

    public bool GetAliveStatus()
    {
        return isAlive;
    }
}
