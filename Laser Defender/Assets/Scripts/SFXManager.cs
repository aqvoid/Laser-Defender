using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("=== Shooting ===")]
    [SerializeField] private AudioClip[] shootingClips;
    [SerializeField, Range(0f, 1f)] private float shootingVolume;

    [Header("=== Someone Got Hit ===")]
    [SerializeField] private AudioClip[] hitClips;
    [SerializeField, Range(0f, 1f)] private float hitVolume;

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
}
