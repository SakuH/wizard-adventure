using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip playerHitSound, enemyHitSound;
    public AudioClip playerHit,enemyHit;
    static AudioSource audioSrc;
    void Start()
    {
        playerHitSound = playerHit;
        enemyHitSound = enemyHit;

        audioSrc = GetComponent<AudioSource>();
    }

   
    void Update()
    {
        
    }
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                    break;
            case "enemyHit":
                audioSrc.PlayOneShot(enemyHitSound);
                break;
        }
    }
}
