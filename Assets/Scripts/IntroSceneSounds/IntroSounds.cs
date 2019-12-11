using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSounds : MonoBehaviour
{
    public AudioSource introSoundMusic;
    public AudioSource introPlayerSfx;
    public AudioSource introPlayerSfx2;
    public AudioClip doorClosed;
     
   public void changeSfx()
    {
        introPlayerSfx.clip = doorClosed;
    }
}
