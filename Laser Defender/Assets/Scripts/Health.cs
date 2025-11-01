using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    public event Action OnDamaged;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer == null) return;

        int damage = damageDealer.GetDamage();
        TakeDamage(damage);
    }

    private void TakeDamage(int damage)
    {
        OnDamaged?.Invoke();
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
