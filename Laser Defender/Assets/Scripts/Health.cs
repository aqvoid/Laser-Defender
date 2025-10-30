using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer == null) return;

        GetComponent<Particles>().PlayHitParticles();
        TakeDamage(damageDealer.GetDamage());
    }

    private void TakeDamage(int damage)
    {
        healthPoints -= damage;
        if (healthPoints <= 0) Destroy(gameObject);
    }
}
