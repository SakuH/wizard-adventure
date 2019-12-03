using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    public float sfxVolume;

    public float minPitch = 0.7f;
    public float maxPitch = 1.2f;

    public AudioClip explosionAudio;

    void Start()
    {
        sfxVolume = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GameAudioSettings>().sfxVolume;
        ExplosionBlastSound(explosionAudio);
    }

    void ExplosionBlastSound(AudioClip clip)
    {
        GameObject clipGameObject = new GameObject("Explosion Sound");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = transform.position;
        source.clip = clip;
        source.volume = sfxVolume * 0.7f;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
    }
}
