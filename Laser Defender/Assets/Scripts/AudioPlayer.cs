using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("=== Shooting ===")]
    [SerializeField] private AudioClip[] shootingClips;
    [SerializeField, Range(0f, 1f)] private float shootingVolume;

    private void OnEnable()
    {
        Attack.OnShoot += PlayShootingClip;
    }

    private void OnDisable()
    {
        Attack.OnShoot -= PlayShootingClip;
    }

    private AudioClip randomAudioClip() => shootingClips[Random.Range(0, shootingClips.Length)];

    public void PlayShootingClip()
    {
        if (shootingClips != null)
        {
            GameObject audio = new GameObject("LaserSound");
            AudioSource source = audio.AddComponent<AudioSource>();

            source.loop = false;
            source.playOnAwake = false;
            source.clip = randomAudioClip();
            source.transform.position = Camera.main.transform.position;
            source.volume = shootingVolume;

            source.Play();
            Destroy(audio, randomAudioClip().length);
        }
    }
}
