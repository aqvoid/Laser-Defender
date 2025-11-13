using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private Health entityHealth;

    private void Awake() => entityHealth = GetComponent<Health>();

    private void OnEnable() => entityHealth.OnEntityDamaged += PlayHitParticles;
    private void OnDisable() => entityHealth.OnEntityDamaged -= PlayHitParticles;

    public void PlayHitParticles(Health health, int damage)
    {
        ParticleSystem instance = Instantiate(particles, health.transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
