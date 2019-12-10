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
    public AudioClip gameOverJingle;
    public AudioClip victoryJingle;
    public AudioClip[] bgNoiseClips;

    private AudioSource audioSource;
    private float currentInterval;

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

    public static IEnumerator FadeToNextBgm(AudioSource audioSource, AudioClip nextAudioClip, float duration, float targetVolume, float nextClipVolume, float delayForNextClip)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = nextClipVolume;
        audioSource.clip = nextAudioClip;
        audioSource.PlayDelayed(delayForNextClip);
        
        yield break;
    }

    public void PlayBossMusic()
    {
        StartCoroutine(FadeToNextBgm(audioSource, bossBgm, 2.0f, 0.0f, bgmVolume, 2.0f));

    }

    public void PlayVictoryMusic()
    {
        audioSource.loop = false;
        StartCoroutine(FadeToNextBgm(audioSource, victoryJingle, 2.0f, 0.0f, bgmVolume, 1.0f));
    }
    public void PlayGameOverMusic()
    {
        audioSource.loop = false;
        StartCoroutine(FadeToNextBgm(audioSource, gameOverJingle, 2.0f, 0.0f, bgmVolume, 1.0f));
    }

}
