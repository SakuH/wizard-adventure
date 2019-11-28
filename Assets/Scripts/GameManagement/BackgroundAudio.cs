using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    public float sfxVolume = 1.0f;
    public float bgmVolume = 1.0f;
    public float bgNoiseRadius = 50.0f;
    public float meanTimeInterval = 10.0f;
    public float minPitch = 0.7f;
    public float maxPitch = 1.3f;
    public AudioClip mainBgm;
    public AudioClip bossBgm;
    public AudioClip[] bgNoiseClips;

    private AudioSource audioSource;
    private float currentInterval;

    // Start is called before the first frame update
    void Start()
    {
        sfxVolume = GetComponent<GameAudioSettings>().sfxVolume;
        bgmVolume = GetComponent<GameAudioSettings>().bgmVolume;

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = bgmVolume;
        audioSource.clip = mainBgm;
        audioSource.loop = true;
        audioSource.Play();

        StartCoroutine("PlaySound");
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            int rand = 0;
            rand = Random.Range(0, bgNoiseClips.Length);
            // Sets the position to be somewhere inside a sphere
            // with a given radius and the center at this game object.
            Vector3 position = transform.position + Random.insideUnitSphere * bgNoiseRadius;
            PlayAudioClip(bgNoiseClips[rand], position, sfxVolume);
            // The timeInterval to the next sound event is choosen from an exponential
            // distribution with a mean value meanTimeInterval.
            currentInterval = -meanTimeInterval * Mathf.Log(Random.value);
            yield return new WaitForSeconds(currentInterval);
        }
    }

    void PlayAudioClip(AudioClip clip, Vector3 position, float volume)
    {
        GameObject clipGameObject = new GameObject("One shot audio");
        AudioSource source = clipGameObject.AddComponent<AudioSource>();
        clipGameObject.transform.position = position;
        source.clip = clip;
        source.volume = volume;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        Destroy(clipGameObject, clip.length / source.pitch);
        
    }

    public void PlayBossMusic()
    {
        audioSource.Stop();
        audioSource.clip = bossBgm;
        audioSource.PlayDelayed(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
