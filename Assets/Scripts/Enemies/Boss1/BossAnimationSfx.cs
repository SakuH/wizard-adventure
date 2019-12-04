﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationSfx : MonoBehaviour
{

    public float sfxVolume;

    public float minPitch = 0.7f;
    public float maxPitch = 1.2f;

    public AudioClip roarClip;
    public AudioClip berserkRoarClip;
    public AudioClip hit1Clip;
    public AudioClip hit2Clip;
    public AudioClip hit3Clip;
    public AudioClip footStepClip;
    public AudioClip swooshClip;

    private AudioSource audioSource;

    void Start()
    {
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = swooshClip;
        audioSource.volume = sfxVolume;
    }

    private void Update()
    {
        if (gameObject.GetComponentInParent<BossScript>().spinning && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!gameObject.GetComponentInParent<BossScript>().spinning && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void BossRoar()
    {
        BossSound(roarClip);
    }
    public void BossBerserkRoar()
    {
        BossSound(berserkRoarClip);
    }

    public void BossHit1()
    {
        BossSound(hit1Clip);
    }

    public void BossHit2()
    {
        BossSound(hit2Clip);
    }

    public void BossHit3()
    {
        BossSound(hit3Clip);
    }
    public void BossFootStep()
    {
        BossSound(footStepClip);
    }




    void BossSound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Boss Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume * 0.7f;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }
}