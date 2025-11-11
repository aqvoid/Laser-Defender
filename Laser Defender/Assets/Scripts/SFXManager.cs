using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Header("=== SFX Config ===")]
    [SerializeField] private SFXConfigSO sFXConfig;

    [Header("=== Shooting ===")]
    [SerializeField, Range(0f, 1f)] private float shootingVolume;

    [Header("=== Someone Got Hit ===")]
    [SerializeField, Range(0f, 1f)] private float hitVolume;

    [Header("=== Player Crushed ===")]
    [SerializeField, Range(0f, 1f)] private float crushVolume;


    private void OnEnable()
    {
        Attack.OnShoot += PlayShootingClip;
        Health.OnAnybodyDamaged += PlayHitClip;
        Health.OnPlayerDeath += PlayCrushClip;
    }

    private void OnDisable()
    {
        Attack.OnShoot -= PlayShootingClip;
        Health.OnAnybodyDamaged -= PlayHitClip;
        Health.OnPlayerDeath -= PlayCrushClip;
    }

    private AudioClip RandomAudioClip(AudioClip[] audioClips)
    {
        if (sFXConfig == null || audioClips.Length == 0) return null;
        return audioClips[Random.Range(0, audioClips.Length)];
    }

    private void PlayClip(AudioClip[] audioClips, float volume)
    {
        if (audioClips != null)
        {
            GameObject audio = new GameObject($"{RandomAudioClip(audioClips).name} Sound");
            AudioSource source = audio.AddComponent<AudioSource>();

            source.loop = false;
            source.playOnAwake = false;
            source.clip = RandomAudioClip(audioClips);
            source.transform.position = Camera.main.transform.position;
            source.volume = volume;

            source.Play();
            Destroy(audio, RandomAudioClip(audioClips).length);
        }
    }

    public void PlayShootingClip() => PlayClip(sFXConfig.shootingClips, shootingVolume);
    public void PlayHitClip() => PlayClip(sFXConfig.hitClips, hitVolume);
    public void PlayCrushClip() => PlayClip(sFXConfig.crushClips, crushVolume);
}
