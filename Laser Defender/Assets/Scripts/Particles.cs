using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private Health health;

    private void Awake() => health = GetComponent<Health>();

    private void OnEnable() => health.OnDamaged += PlayHitParticles;

    private void OnDisable() => health.OnDamaged -= PlayHitParticles;

    public void PlayHitParticles()
    {
        ParticleSystem instance = Instantiate(particles, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
