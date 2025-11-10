using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("=== Shooting ===")]
    [SerializeField] private AudioClip[] shootingClips;
    [SerializeField, Range(0f, 1f)] private float shootingVolume;

    [Header("=== Someone Got Hit ===")]
    [SerializeField] private AudioClip[] hitClips;
    [SerializeField, Range(0f, 1f)] private float hitVolume;

    [Header("=== Background Music ===")]
    [SerializeField, Range(0f, 1f)] private float musicVolume;


    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = musicVolume;
    }

    private void OnEnable()
    {
        Attack.OnShoot += PlayShootingClip;
        Health.OnAnybodyDamaged += PlayHitClip;
    }

    private void OnDisable()
    {
        Attack.OnShoot -= PlayShootingClip;
        Health.OnAnybodyDamaged -= PlayHitClip;
    }

    private AudioClip randomAudioClip(AudioClip[] audioClips) => audioClips[Random.Range(0, shootingClips.Length)];

    public void PlayShootingClip() => PlayClip(shootingClips, shootingVolume);

    public void PlayHitClip() => PlayClip(hitClips, hitVolume);

    private void PlayClip(AudioClip[] audioClips, float volume)
    {
        if (audioClips != null)
        {
            GameObject audio = new GameObject($"{randomAudioClip(audioClips).name} Sound");
            AudioSource source = audio.AddComponent<AudioSource>();

            source.loop = false;
            source.playOnAwake = false;
            source.clip = randomAudioClip(audioClips);
            source.transform.position = Camera.main.transform.position;
            source.volume = volume;

            source.Play();
            Destroy(audio, randomAudioClip(audioClips).length);
        }
    }

    public IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / duration);
            yield return null;
        }
        
        musicSource.volume = 0f;
        musicSource.Pause();
    }

    public IEnumerator FadeInMusic(float duration)
    {
        musicSource.UnPause();

        float startVolume = 0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, musicVolume, elapsed / duration);
            yield return null;
        }

        musicSource.volume = musicVolume;
    }
}
