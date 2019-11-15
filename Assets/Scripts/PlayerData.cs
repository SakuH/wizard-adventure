using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float maxHealth;
    public bool weaponIsEquiped;
    public int equipedWeapon;
    public int currentFloor;
    public PlayerData(GameObject player)
    {
        maxHealth = player.GetComponent<PlayerHealth>().maxHealth;

        if(player.GetComponent<PlayerMovement>().weapon != null)
        {
            weaponIsEquiped = player.GetComponent<PlayerMovement>().weapon.weaponIsEquiped;
            if (weaponIsEquiped)
            {
                equipedWeapon = player.GetComponent<PlayerMovement>().weapon.equipedWeapon;
            }
            else
            {
                equipedWeapon = 0;
            }
        }
        else
        {
            weaponIsEquiped = false;
            equipedWeapon = 0;
        }

        currentFloor = player.GetComponent<PlayerMovement>().currentTowerFloor;
    }
}
