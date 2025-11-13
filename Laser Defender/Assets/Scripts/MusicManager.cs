using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [Header("=== Background Music ===")]
    [SerializeField, Range(0f, 1f)] private float musicVolume;

    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = musicVolume;
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
