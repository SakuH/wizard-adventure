using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLight : MonoBehaviour
{


    public void LightOn()
    {
        //Debug.Log("In light on function");
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
